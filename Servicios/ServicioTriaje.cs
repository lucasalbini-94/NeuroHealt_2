namespace NeuroHealthDesktop.Servicios
{
    public class ServicioTriaje : IServicioTriaje
    {
        public NivelUrgencia Clasificar(SignosVitales signos)
        {
            // TODO: Implementar las reglas de triaje.
            int saturacion = signos.Saturacion;
            int pulso = signos.Pulso;
            double temperatura = signos.Temperatura;
            int dolor = signos.Dolor;

            if (saturacion < 90 || pulso > 120 || temperatura >= 39.0 || dolor >= 9)
            {
                return NivelUrgencia.Rojo;
            }
            if (saturacion < 94 || pulso > 100 || temperatura >= 38.0 || dolor >= 6)
            {
                return NivelUrgencia.Amarillo;
            }
            else
            {
                return NivelUrgencia.Verde;
            }
        }

        private bool EsRojo(SignosVitales signos)
        {
            // TODO: Implementar condición de nivel rojo.
            if (Clasificar(signos) == NivelUrgencia.Rojo)
                return true;
            return false;
        }

        private bool EsAmarillo(SignosVitales signos)
        {
            // TODO: Implementar condición de nivel amarillo.
            if (Clasificar(signos) == NivelUrgencia.Amarillo)
                return true;
            return false;
        }
    }
}
