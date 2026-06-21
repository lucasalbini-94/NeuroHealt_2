using NeuroHealthDesktop.Repositorios;
using NeuroHealthDesktop.Servicios;
using System;
using System.Security.Cryptography;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NeuroHealthDesktop.Forms
{
    public partial class FrmPrincipal : Form
    {
        private IRepositorioPacientes repositorioPacientes;
        private IRepositorioObservaciones repositorioObservaciones;
        private IServicioTriaje servicioTriaje;
        private ServicioPacientes servicioPacientes;
        private ServicioObservaciones servicioObservaciones;

        public FrmPrincipal()
        {
            InitializeComponent();

            InicializarDependencias();
            ConfigurarGrillas();
            ActualizarGrillas();

            lblEstado.Text = "Sistema iniciado correctamente.";
            progressBarEvaluacion.Visible = false;
        }

        private void InicializarDependencias()
        {
            // TODO: Crear repositorios, servicios y conectar dependencias.
            repositorioPacientes = new RepositorioPacientesArchivo();
            repositorioObservaciones = new RepositorioObservacionesArchivo();
            servicioTriaje = new ServicioTriaje();

            servicioPacientes = new ServicioPacientes(repositorioPacientes, servicioTriaje);
            servicioObservaciones = new ServicioObservaciones(repositorioObservaciones, repositorioPacientes);
        }

        private void ConfigurarGrillas()
        {
            // Pacientes en espera
            dgvColaEspera.Columns.Add("Dni", "DNI");
            dgvColaEspera.Columns.Add("Paciente", "Paciente");
            dgvColaEspera.Columns.Add("Edad", "Edad");
            dgvColaEspera.Columns.Add("Motivo", "Motivo de Consulta");
            dgvColaEspera.Columns.Add("Nivel", "Nivel");
            dgvColaEspera.Columns.Add("Tipo", "Tipo");

            // Pacientes admitidos
            dgvPacientesAdmitidos.Columns.Add("Dni", "DNI");
            dgvPacientesAdmitidos.Columns.Add("Paciente", "Paciente");
            dgvPacientesAdmitidos.Columns.Add("Edad", "Edad");
            dgvPacientesAdmitidos.Columns.Add("Motivo", "Motivo de Consulta");
            dgvPacientesAdmitidos.Columns.Add("Urgencia", "Urgencia");
            dgvPacientesAdmitidos.Columns.Add("Tipo", "Tipo");
        }

        private void ConfigurarGrillaPacientes(DataGridView grilla, bool mostrarNivelTexto)
        {
            // TODO: Configurar una grilla de pacientes.
        }

        private void ActualizarGrillas()
        {
            // Borra todo antes porque sino agrega y duplica lo que ya puso
            dgvColaEspera.Rows.Clear();
            // Agrega todos los pacientes
            foreach (var paciente in servicioPacientes.ObtenerPacientesAdmitidos())
            {
                dgvColaEspera.Rows.Add(
                    paciente.Dni,
                    paciente.NombreApellido,
                    paciente.Edad,
                    paciente.Motivo,
                    "",
                    paciente.Tipo
                );
            }
        }

        private async void btnEvaluarPaciente_Click(object sender, EventArgs e)
        {
            // Fragmento orientativo provisto para simular tarea en segundo plano.
            btnEvaluarPaciente.Enabled = false;
            progressBarEvaluacion.Visible = true;
            progressBarEvaluacion.Style = ProgressBarStyle.Marquee;
            lblEstado.Text = "Aquí se evaluará el siguiente paciente.";

            await Task.Run(() =>
            {
                Thread.Sleep(1000);
            });

            progressBarEvaluacion.Visible = false;
            btnEvaluarPaciente.Enabled = true;
            lblEstado.Text = "Evaluación pendiente de implementar.";
        }

        private void btnNuevoPaciente_Click(object sender, EventArgs e)
        {
            FrmRegistrarPaciente nuevoFormularioRegistro = new FrmRegistrarPaciente(servicioPacientes);
            nuevoFormularioRegistro.ShowDialog();
        }

        private void btnObservaciones_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Aquí se abrirá el formulario de observaciones.");
        }

        private void btnEstadisticas_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Aquí se abrirá el formulario de estadísticas.");
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            ActualizarGrillas();
            lblEstado.Text = "Aquí se actualizarán los datos mostrados.";
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void dgvPacientesAdmitidos_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            // TODO: Aplicar color según el nivel de urgencia.
        }
    }
}
