using System.Collections.Generic;

namespace NeuroHealthDesktop.Repositorios
{
    public class RepositorioPacientesArchivo : IRepositorioPacientes
    {
        private string rutaArchivo;

        public RepositorioPacientesArchivo()
        {
            // TODO: Crear carpeta Datos y definir ruta de pacientes.txt.
            rutaArchivo = "pacientes.txt";
        }

        public void Agregar(Paciente paciente)
        {
            // TODO: Agregar paciente al archivo.
        }

        public List<Paciente> ObtenerTodos()
        {
            // TODO: Leer pacientes desde archivo.
            return new List<Paciente>();
        }

        public Paciente? BuscarPorDni(long dni)
        {
            // TODO: Buscar paciente por DNI en archivo.
            return null;
        }

        public bool ExisteDni(long dni)
        {
            // TODO: Verificar si existe el DNI.
            return false;
        }

        public void Actualizar(Paciente paciente)
        {
            // TODO: Actualizar paciente en archivo.
        }

        public List<Paciente> FiltrarPorNivel(NivelUrgencia nivel)
        {
            // TODO: Filtrar pacientes por nivel.
            return new List<Paciente>();
        }

        private void GuardarTodos(List<Paciente> pacientes)
        {
            // TODO: Reescribir archivo completo.
        }

        private string ConvertirPacienteALinea(Paciente paciente)
        {
            // TODO: Convertir paciente a formato separado por |.
            return string.Empty;
        }

        private Paciente? ConvertirLineaAPaciente(string linea)
        {
            // TODO: Convertir línea del archivo en PacienteGuardia o PacientePediatrico.
            return null;
        }
    }
}
