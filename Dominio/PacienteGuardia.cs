namespace NeuroHealthDesktop
{
    public class PacienteGuardia : Paciente
    {
        public bool RequiereCamilla { get; set; }

        public override TipoPaciente Tipo
        {
            get { return TipoPaciente.Guardia; }
        }

        public PacienteGuardia(
            long dni,
            string nombreApellido,
            int edad,
            MotivoConsulta motivo,
            SignosVitales signos,
            bool requiereCamilla)
            : base(dni, nombreApellido, edad, motivo, signos)
        {
            // TODO: Inicializar dato específico del paciente de guardia.
            RequiereCamilla = requiereCamilla;
        }

        public override string ObtenerDescripcion()
        {
            // TODO: Completar descripción específica del paciente de guardia.
            return base.ObtenerDescripcion();
        }
    }
}
