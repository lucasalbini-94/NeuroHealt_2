using System.Collections.Generic;

namespace NeuroHealthDesktop.Servicios
{
    public class ServicioObservaciones
    {
        private IRepositorioObservaciones repositorioObservaciones;
        private IRepositorioPacientes repositorioPacientes;

        public ServicioObservaciones(IRepositorioObservaciones repositorioObservaciones, IRepositorioPacientes repositorioPacientes)
        {
            this.repositorioObservaciones = repositorioObservaciones;
            this.repositorioPacientes = repositorioPacientes;
        }

        public ResultadoOperacion<Observacion> AgregarObservacion(long dniPaciente, string texto)
        {
            // TODO: Validar paciente, validar texto y agregar observación.
            return ResultadoOperacion<Observacion>.Error("Método pendiente de implementación.");
        }

        public List<Observacion> ObtenerObservacionesPorPaciente(long dniPaciente)
        {
            // TODO: Obtener observaciones por DNI y ordenarlas.
            return new List<Observacion>();
        }

        public List<Observacion> ObtenerTodas()
        {
            // TODO: Obtener todas las observaciones.
            return new List<Observacion>();
        }
    }
}
