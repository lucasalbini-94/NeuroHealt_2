namespace NeuroHealthDesktop
{
    public interface IRepositorioObservaciones
    {
        void Agregar(Observacion observacion);
        List<Observacion> ObtenerTodas();
        List<Observacion> ObtenerPorDniPaciente(long dniPaciente);
        int ObtenerProximoId();
    }
}
