namespace NeuroHealthDesktop
{
    public interface IRepositorioPacientes
    {
        void Agregar(Paciente paciente);
        List<Paciente> ObtenerTodos();
        Paciente? BuscarPorDni(long dni);
        bool ExisteDni(long dni);
        void Actualizar(Paciente paciente);
        List<Paciente> FiltrarPorNivel(NivelUrgencia nivel);
    }
}
