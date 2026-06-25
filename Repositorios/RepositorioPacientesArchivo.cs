using System.Collections.Generic;

namespace NeuroHealthDesktop.Repositorios
{
    public class RepositorioPacientesArchivo : IRepositorioPacientes
    {
        private string rutaArchivo;

        public RepositorioPacientesArchivo()
        {
            // TODO: Crear carpeta Datos y definir ruta de pacientes.txt.
            string carpetaArchivo = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Datos");
            if (!Directory.Exists(carpetaArchivo))
            {
                Directory.CreateDirectory(carpetaArchivo);
            }
            rutaArchivo = "pacientes.txt";
        }

        public void Agregar(Paciente paciente)
        {
            // TODO: Agregar paciente al archivo.
            string? linea = ConvertirPacienteALinea(paciente);

            using (StreamWriter sw = new StreamWriter(rutaArchivo))
            {
                sw.WriteLine(linea);
            }
        }

        public List<Paciente> ObtenerTodos()
        {
            // TODO: Leer pacientes desde archivo.
            // Si el archivo no existe, retornar lista vacía.
            List<Paciente> listaPacientes = new List<Paciente>();

            if (!File.Exists(rutaArchivo))
            {
                return listaPacientes;
            }

            using StreamReader sr = new StreamReader(rutaArchivo);
            {
                string linea;
                // se lee línea por línea reconstruyendo el objeto, siempre que la línea no sea nula
                while ((linea = sr.ReadLine()) != null)
                {
                    if (!string.IsNullOrWhiteSpace(linea))
                    {
                        Paciente? paciente = ConvertirLineaAPaciente(linea);
                        if (paciente != null)
                        {
                            listaPacientes.Add(paciente);
                        }
                    }
                }
            }

            return listaPacientes;
        }

        public Paciente? BuscarPorDni(long dni)
        {
            // TODO: Buscar paciente por DNI en archivo.
            Paciente? buscado = null;
            List<Paciente> lista = ObtenerTodos();
            buscado = lista.Find(p => p.Dni == dni);
            return buscado;
        }

        public bool ExisteDni(long dni)
        {
            // TODO: Verificar si existe el DNI.
            return BuscarPorDni(dni) != null;
        }

        public void Actualizar(Paciente paciente)
        {
            // TODO: Actualizar paciente en archivo.
            List<Paciente> lista = ObtenerTodos();
            int index = lista.FindIndex(p => p.Dni == paciente.Dni);

            if (index != -1)
            {
                lista[index] = paciente;
                GuardarTodos(lista);
            }
        }

        public List<Paciente> FiltrarPorNivel(NivelUrgencia nivel)
        {
            // TODO: Filtrar pacientes por nivel.
            List<Paciente> lista = ObtenerTodos();
            List<Paciente> listaFiltrada = new List<Paciente>();

            listaFiltrada = lista.FindAll(p => p.Nivel == nivel);
            
            return listaFiltrada;
        }

        private void GuardarTodos(List<Paciente> pacientes)
        {
            // TODO: Reescribir archivo completo.
            using (StreamWriter sw = new StreamWriter(rutaArchivo))
            {
                foreach (Paciente paciente in pacientes)
                {
                    string linea = ConvertirPacienteALinea(paciente);
                    sw.WriteLine(linea);
                }
            }
        }

        private string ConvertirPacienteALinea(Paciente paciente)
        {
            // TODO: Convertir paciente a formato separado por |.
            string tipoPaciente = "";
            string requiereCamilla = "";
            string adultoResponsable = "";

            // Verificar tipo de paciente para asignar los valores correspondientes.
            if (paciente.Tipo == TipoPaciente.Guardia)
            {
                tipoPaciente = "Guardia";
                requiereCamilla = ((PacienteGuardia)paciente).RequiereCamilla ? "Sí" : "No";
            }
            else if (paciente.Tipo == TipoPaciente.Pediatrico)
            {
                tipoPaciente = "Pediatrico";
                adultoResponsable = ((PacientePediatrico)paciente).AdultoResponsable;
            }

            // Crear línea con los datos del paciente separados por "|".
            string linea = $"{tipoPaciente}|{paciente.Dni}|{paciente.NombreApellido}|{paciente.Edad}|{paciente.Motivo}|" +
                $"{paciente.Signos.Pulso}|{paciente.Signos.Temperatura}|{paciente.Signos.Presion}|{paciente.Signos.Saturacion}|{paciente.Signos.Dolor}|" +
                $"{paciente.FechaIngreso}|{paciente.Nivel}|{requiereCamilla}|{adultoResponsable}";

            return linea;
        }

        private Paciente? ConvertirLineaAPaciente(string linea)
        {
            Paciente paciente = null;
            // TODO: Convertir línea del archivo en PacienteGuardia o PacientePediatrico.
            try
            {
                string[] campos = linea.Split('|');

                string tipoPaciente = campos[0];
                long dni = long.Parse(campos[1]);
                string nombreApellido = campos[2];
                int edad = int.Parse(campos[3]);
                MotivoConsulta motivo = (MotivoConsulta)Enum.Parse(typeof(MotivoConsulta), campos[4]);
                int pulso = int.Parse(campos[5]);
                double temperatura = double.Parse(campos[6]);
                string presion = campos[7];
                int saturacion = int.Parse(campos[8]);
                int dolor = int.Parse(campos[9]);
                SignosVitales signos = new SignosVitales(pulso, temperatura, presion, saturacion, dolor);
                DateTime fechaIngreso = DateTime.Parse(campos[10]);
                NivelUrgencia nivel = (NivelUrgencia)Enum.Parse(typeof(NivelUrgencia), campos[11]);

                if (tipoPaciente == "Guardia")
                {
                    bool requiereCamilla = campos[12] == "Sí";
                    paciente = new PacienteGuardia(dni, nombreApellido, edad, motivo, signos, requiereCamilla);
                }
                else if (tipoPaciente == "Pediatrico")
                {
                    string adultoResponsable = campos[13];
                    paciente = new PacientePediatrico(dni, nombreApellido, edad, motivo, signos, adultoResponsable);
                }

                if (paciente != null)
                {
                    paciente.FechaIngreso = fechaIngreso;
                    paciente.Nivel = nivel;
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Hubo un error al cargar pacientes desde el archivo");
            }

            return paciente;
        }
    }
}
