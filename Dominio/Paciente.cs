namespace NeuroHealthDesktop
{
    public abstract class Paciente
    {
        public long Dni { get; set; }
        public string NombreApellido { get; set; }
        public int Edad { get; set; }
        public MotivoConsulta Motivo { get; set; }
        public SignosVitales Signos { get; set; }
        public DateTime FechaIngreso { get; set; }
        public NivelUrgencia Nivel { get; set; }

        public abstract TipoPaciente Tipo { get; }

        protected Paciente(long dni, string nombreApellido, int edad, MotivoConsulta motivo, SignosVitales signos)
        {
            // TODO: Revisar qué datos deben inicializarse al crear un paciente.
            Dni = dni;
            NombreApellido = nombreApellido;
            Edad = edad;
            Motivo = motivo;
            Signos = signos;
            FechaIngreso = DateTime.Now;
            Nivel = NivelUrgencia.SinEvaluar;
        }

        public virtual string ObtenerDescripcion()
        {
            // TODO: Completar una descripción general del paciente.
            return $"{Dni} - {NombreApellido}";
        }
    }
}
