namespace NeuroHealthDesktop
{
    public class SignosVitales
    {
        public int Pulso { get; set; }
        public double Temperatura { get; set; }
        public string Presion { get; set; }
        public int Saturacion { get; set; }
        public int Dolor { get; set; }

        public SignosVitales(int pulso, double temperatura, string presion, int saturacion, int dolor)
        {
            // TODO: Asignar los valores recibidos a las propiedades.
            Pulso = pulso;
            Temperatura = temperatura;
            Presion = presion;
            Saturacion = saturacion;
            Dolor = dolor;
        }
    }
}
