namespace NeuroHealthDesktop
{
    public interface IServicioTriaje
    {
        NivelUrgencia Clasificar(SignosVitales signos);
    }
}
