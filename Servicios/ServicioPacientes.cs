using NeuroHealthDesktop.Forms;
using System.Collections.Generic;

namespace NeuroHealthDesktop.Servicios
{
    public class ServicioPacientes
    {
        private Queue<Paciente> colaEspera;
        private List<Paciente> pacientesAdmitidos;

        private IRepositorioPacientes repositorioPacientes;
        private IServicioTriaje servicioTriaje;

        public ServicioPacientes(IRepositorioPacientes repositorioPacientes, IServicioTriaje servicioTriaje)
        {
            this.repositorioPacientes = repositorioPacientes;
            this.servicioTriaje = servicioTriaje;

            colaEspera = new Queue<Paciente>();
            pacientesAdmitidos = new List<Paciente>();

            CargarDatosInicialesDesdeRepositorio();
        }

        private void CargarDatosInicialesDesdeRepositorio()
        {
            // TODO: Cargar pacientes desde el repositorio.

            List<Paciente> listaTotal = repositorioPacientes.ObtenerTodos();

            foreach (Paciente p in listaTotal)
            {
                // Los pacientes SinEvaluar deben ir a colaEspera.
                // Los pacientes evaluados deben ir a pacientesAdmitidos.
                if (p.Nivel == NivelUrgencia.SinEvaluar)
                {
                    colaEspera.Enqueue(p);
                }
                else
                {
                    pacientesAdmitidos.Add(p);
                }
            }
        }

        public ResultadoOperacion<Paciente> RegistrarPaciente(Paciente paciente)
        {
                if (paciente.Dni <= 0)
                {
                    return ResultadoOperacion<Paciente>.Error("El DNI del paciente debe ser un número positivo.");
                }
                if (paciente.Edad <= 0)
                {
                    return ResultadoOperacion<Paciente>.Error("La edad debe ser mayor a 0.");
                }
                if (string.IsNullOrWhiteSpace(paciente.NombreApellido))
                {
                    return ResultadoOperacion<Paciente>.Error("El nombre y apellido no puede estar vacío.");
                }
                if (paciente is PacientePediatrico pp && string.IsNullOrWhiteSpace(pp.AdultoResponsable))
                {
                    return ResultadoOperacion<Paciente>.Error("El paciente pediátrico debe tener un adulto responsable.");
                }
                if (paciente.Signos.Pulso < 30 || paciente.Signos.Pulso > 200)
                {
                    return ResultadoOperacion<Paciente>.Error("El pulso debe estar entre 30 y 200.");
                }
                if (paciente.Signos.Temperatura < 34 || paciente.Signos.Temperatura > 42)
                {
                    return ResultadoOperacion<Paciente>.Error("La temperatura debe estar entre 34 y 42 grados Celsius.");
                }
                if (paciente.Signos.Saturacion < 70 || paciente.Signos.Saturacion > 100)
                {
                    return ResultadoOperacion<Paciente>.Error("La saturación debe estar entre 70 y 100.");
                }
                if (paciente.Signos.Dolor < 0 || paciente.Signos.Dolor > 10)
                {
                    return ResultadoOperacion<Paciente>.Error("El nivel de dolor debe estar entre 0 y 10.");
                }

                if (repositorioPacientes.ExisteDni(paciente.Dni))
                {
                    return ResultadoOperacion<Paciente>.Error("Ya existe un paciente con el mismo DNI.");
                }
                paciente.Nivel = NivelUrgencia.SinEvaluar;
            
                try
                {
                    repositorioPacientes.Agregar(paciente);
                    colaEspera.Enqueue(paciente);
                    return ResultadoOperacion<Paciente>.Correcto("Paciente registrado correctamente.", paciente);
                }
                catch (Exception ex)
                {
                    return ResultadoOperacion<Paciente>.Error($"Error al registrar paciente: {ex.Message}");
                }
            
        }

        public List<Paciente> ObtenerColaEspera()
        {
            // NO ME DEJA DEVOLVER LA COLA.
            return  new List<Paciente>(colaEspera);
        }

        public List<Paciente> ObtenerPacientesAdmitidos()
        {
            // TODO: Devolver pacientes admitidos.
            return pacientesAdmitidos;
        }

        public List<Paciente> ObtenerTodos()
        {
            // TODO: Obtener todos los pacientes desde el repositorio.
            return repositorioPacientes.ObtenerTodos();
        }

        public ResultadoOperacion<Paciente> EvaluarSiguientePaciente()
        {
            // TODO: Evaluar el siguiente paciente en espera.
            if (colaEspera.Count == 0)
            {
                return ResultadoOperacion<Paciente>.Error("No hay pacientes en espera para evaluar.");
            }

            // Examinar al siguiente paciente en la cola de espera.
            Paciente paciente = colaEspera.Dequeue();

            NivelUrgencia nivelAsignado = servicioTriaje.Clasificar(paciente.Signos);
            paciente.Nivel = nivelAsignado;

            try
            {
                repositorioPacientes.Actualizar(paciente);
                pacientesAdmitidos.Add(paciente);
                return ResultadoOperacion<Paciente>.Correcto("Paciente evaluado y admitido correctamente.", paciente);
            }
            catch (Exception ex)
            {
                return ResultadoOperacion<Paciente>.Error($"Error al evaluar paciente: {ex.Message}");
            }
        }

        public ResultadoOperacion<Paciente> BuscarPacientePorDni(long dni)
        {
            // TODO: Buscar paciente por DNI en ambas listas. Si la primera es nula, busca en la segunda.
            Paciente? paciente = pacientesAdmitidos.Find(p => p.Dni == dni) ?? colaEspera.FirstOrDefault(p => p.Dni == dni);

            if (paciente != null)
            {
                return ResultadoOperacion<Paciente>.Correcto("Paciente encontrado.", paciente);
            }

            return ResultadoOperacion<Paciente>.Error("No existe un paciente con ese DNI");
        }

        public List<Paciente> FiltrarPorNivel(NivelUrgencia nivel)
        {
            // TODO: Filtrar pacientes admitidos por nivel.
            return pacientesAdmitidos.FindAll(p => p.Nivel == nivel);
        }

        public int ContarEnEspera()
        {
            // TODO: Contar pacientes en espera.
            return colaEspera.Count;
        }

        public int ContarAdmitidos()
        {
            // TODO: Contar pacientes admitidos.
            return pacientesAdmitidos.Count;
        }

        public int ContarPorNivel(NivelUrgencia nivel)
        {
            // TODO: Contar pacientes por nivel de urgencia.
            return pacientesAdmitidos.Count(p => p.Nivel == nivel);
        }

        public double CalcularEdadPromedioAdmitidos()
        {
            // TODO: Calcular edad promedio de pacientes admitidos.
            if (pacientesAdmitidos.Count == 0)
            {
                return 0.0;
            }
            return pacientesAdmitidos.Average(p => p.Edad);
        }

        public double CalcularPorcentajeCriticos()
        {
            // TODO: Calcular porcentaje de pacientes con nivel Rojo.
            if (pacientesAdmitidos.Count == 0)
            {
                return 0.0;
            }
            double cantidadCriticos = ContarPorNivel(NivelUrgencia.Rojo);
            return (cantidadCriticos / pacientesAdmitidos.Count()) * 100.0;
        }
    }
}
