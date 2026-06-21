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
            // Los pacientes SinEvaluar deben ir a colaEspera.
            // Los pacientes evaluados deben ir a pacientesAdmitidos.
        }

        public ResultadoOperacion<Paciente> RegistrarPaciente(Paciente paciente)
        {
            colaEspera.Enqueue(paciente);
            pacientesAdmitidos.Add(paciente);
            return ResultadoOperacion<Paciente>.Error("Método pendiente de implementación.");
        }

        public List<Paciente> ObtenerColaEspera()
        {
            // NO ME DEJA DEVOLVER LA COLA.
            return pacientesAdmitidos;
        }

        public List<Paciente> ObtenerPacientesAdmitidos()
        {
            // TODO: Devolver pacientes admitidos.
            return pacientesAdmitidos;
        }

        public List<Paciente> ObtenerTodos()
        {
            // TODO: Obtener todos los pacientes desde el repositorio.
            return new List<Paciente>();
        }

        public ResultadoOperacion<Paciente> EvaluarSiguientePaciente()
        {
            // TODO: Evaluar el siguiente paciente en espera.
            return ResultadoOperacion<Paciente>.Error("Método pendiente de implementación.");
        }

        public ResultadoOperacion<Paciente> BuscarPacientePorDni(long dni)
        {
            // TODO: Buscar paciente por DNI.
            return ResultadoOperacion<Paciente>.Error("Método pendiente de implementación.");
        }

        public List<Paciente> FiltrarPorNivel(NivelUrgencia nivel)
        {
            // TODO: Filtrar pacientes admitidos por nivel.
            return new List<Paciente>();
        }

        public int ContarEnEspera()
        {
            // TODO: Contar pacientes en espera.
            return 0;
        }

        public int ContarAdmitidos()
        {
            // TODO: Contar pacientes admitidos.
            return 0;
        }

        public int ContarPorNivel(NivelUrgencia nivel)
        {
            // TODO: Contar pacientes por nivel de urgencia.
            return 0;
        }

        public double CalcularEdadPromedioAdmitidos()
        {
            // TODO: Calcular edad promedio de pacientes admitidos.
            return 0;
        }

        public double CalcularPorcentajeCriticos()
        {
            // TODO: Calcular porcentaje de pacientes con nivel Rojo.
            return 0;
        }
    }
}
