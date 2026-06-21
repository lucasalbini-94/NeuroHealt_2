using System.Collections.Generic;

namespace NeuroHealthDesktop.Repositorios
{
    public class RepositorioObservacionesArchivo : IRepositorioObservaciones
    {
        private string rutaArchivo;

        public RepositorioObservacionesArchivo()
        {
            // TODO: Crear carpeta Datos y definir ruta de observaciones.txt.
            rutaArchivo = "observaciones.txt";
        }

        public void Agregar(Observacion observacion)
        {
            // TODO: Agregar observación al archivo.
        }

        public List<Observacion> ObtenerTodas()
        {
            // TODO: Leer observaciones desde archivo.
            return new List<Observacion>();
        }

        public List<Observacion> ObtenerPorDniPaciente(long dniPaciente)
        {
            // TODO: Filtrar observaciones por DNI del paciente.
            return new List<Observacion>();
        }

        public int ObtenerProximoId()
        {
            // TODO: Calcular próximo ID disponible.
            return 1;
        }

        private string ConvertirObservacionALinea(Observacion observacion)
        {
            // TODO: Convertir observación a formato separado por |.
            return string.Empty;
        }

        private Observacion? ConvertirLineaAObservacion(string linea)
        {
            // TODO: Convertir línea del archivo en observación.
            return null;
        }
    }
}
