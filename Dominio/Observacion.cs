namespace NeuroHealthDesktop
{
    public class Observacion
    {
        public int Id { get; set; }
        public long DniPaciente { get; set; }
        public DateTime Fecha { get; set; }
        public string Texto { get; set; }

        public Observacion(int id, long dniPaciente, string texto)
        {
            Id = id;
            DniPaciente = dniPaciente;
            Texto = texto;
            Fecha = DateTime.Now;
        }

        public Observacion(int id, long dniPaciente, DateTime fecha, string texto)
        {
            Id = id;
            DniPaciente = dniPaciente;
            Fecha = fecha;
            Texto = texto;
        }

        public override string ToString()
        {
            // TODO: Definir cómo se visualizará una observación en pantalla.
            return $"{Fecha:dd/MM/yyyy HH:mm} - {Texto}";
        }
    }
}
