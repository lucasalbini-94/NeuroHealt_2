using NeuroHealthDesktop.Repositorios;
using NeuroHealthDesktop.Servicios;
using System;
using System.Diagnostics.Eventing.Reader;
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
            dgvPacientesAdmitidos.Rows.Clear();
            // Agrega todos los pacientes
            foreach (var paciente in servicioPacientes.ObtenerPacientesAdmitidos())
            if (paciente.Nivel == NivelUrgencia.SinEvaluar)
            {
                dgvColaEspera.Rows.Add(
                    paciente.Dni,
                    paciente.NombreApellido,
                    paciente.Edad,
                    paciente.Motivo,
                    paciente.Signos.Dolor,
                    paciente.Tipo
                );
            }
                else
                {
                    dgvPacientesAdmitidos.Rows.Add(
                    paciente.Dni,
                    paciente.NombreApellido,
                    paciente.Edad,
                    paciente.Motivo,
                    paciente.Nivel,
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

            foreach (var paciente in servicioPacientes.ObtenerPacientesAdmitidos())
            {
                if (paciente.Signos.Saturacion < 90 &&
                    paciente.Signos.Pulso > 120 &&
                    paciente.Signos.Temperatura >= 39.0 &&
                    paciente.Signos.Dolor >= 9)
                {
                    paciente.Nivel = NivelUrgencia.Rojo;
                }
                else if (paciente.Signos.Saturacion >= 90 && paciente.Signos.Saturacion <= 94 &&
                    paciente.Signos.Pulso >= 100 && paciente.Signos.Pulso <= 120 &&
                    paciente.Signos.Temperatura >= 38.0 && paciente.Signos.Temperatura <= 38.9 &&
                    paciente.Signos.Dolor >= 6 && paciente.Signos.Dolor <= 8)
                {
                    paciente.Nivel = NivelUrgencia.Amarillo;
                }
                else
                {
                    paciente.Nivel = NivelUrgencia.Verde;
                }
                

            }
            ActualizarGrillas();

        }

        private void btnNuevoPaciente_Click(object sender, EventArgs e)
        {
            FrmRegistrarPaciente nuevoFormularioRegistro = new FrmRegistrarPaciente(servicioPacientes);
            nuevoFormularioRegistro.ShowDialog();
            ActualizarGrillas();
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
