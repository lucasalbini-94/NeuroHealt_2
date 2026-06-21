namespace NeuroHealthDesktop
{
    public class PacientePediatrico : Paciente
    {
        public string AdultoResponsable { get; set; }

        public override TipoPaciente Tipo
        {
            get { return TipoPaciente.Pediatrico; }
        }

        public PacientePediatrico(
            long dni,
            string nombreApellido,
            int edad,
            MotivoConsulta motivo,
            SignosVitales signos,
            string adultoResponsable)
            : base(dni, nombreApellido, edad, motivo, signos)
        {
            // TODO: Inicializar dato específico del paciente pediátrico.
            AdultoResponsable = adultoResponsable;
        }

        public override string ObtenerDescripcion()
        {
            // TODO: Completar descripción específica del paciente pediátrico.
            return base.ObtenerDescripcion();
        }
    }
}
