using NeuroHealthDesktop.Repositorios;
using NeuroHealthDesktop.Servicios;
using System;
using System.Diagnostics.Eventing.Reader;
using System.Security.Cryptography;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.WebRequestMethods;

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
            foreach (var paciente in servicioPacientes.ObtenerTodos())
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

            // CAMBIA DEL NOMBRE AL COLOR EN NIVEL DE URGENCIA
            if (dgvPacientesAdmitidos != null && dgvPacientesAdmitidos.Rows.Count > 0)
            {
                foreach (DataGridViewRow fila in dgvPacientesAdmitidos.Rows)
                {
                    if (fila.Cells["Urgencia"].Value != null)
                    {
                        string nivel = fila.Cells["Urgencia"].Value.ToString();

                        if (nivel == "Rojo")
                        {
                            fila.Cells["Urgencia"].Style.BackColor = Color.Red;
                            fila.Cells["Urgencia"].Value = "";
                        }
                        else if (nivel == "Amarillo")
                        {
                            fila.Cells["Urgencia"].Style.BackColor = Color.Yellow;
                            fila.Cells["Urgencia"].Value = "";
                        }
                        else if (nivel == "Verde")
                        {
                            fila.Cells["Urgencia"].Style.BackColor = Color.Green;
                            fila.Cells["Urgencia"].Value = "";
                        }
                    }
                }

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

            var resultado = servicioPacientes.EvaluarSiguientePaciente();
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
            if (dgvPacientesAdmitidos.SelectedRows.Count == 0)
            {
                MessageBox.Show("Por favor, seleccione un paciente de la lista de admitidos primero.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var fila = dgvPacientesAdmitidos.SelectedRows[0];
            long dniPaciente = Convert.ToInt64(fila.Cells["DNI"].Value);

            // Pasa la instancia de servicioObservaciones al constructor
            FrmObservaciones ventanaObs = new FrmObservaciones(servicioObservaciones);
            ventanaObs.ShowDialog();
        }

        private void btnEstadisticas_Click(object sender, EventArgs e)
        {
            FrmEstadisticas nuevoFormularioEstadistica = new FrmEstadisticas(servicioPacientes);
            nuevoFormularioEstadistica.ShowDialog();
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

        private void lblTitulo_Click(object sender, EventArgs e)
        {

        }
    }
}
