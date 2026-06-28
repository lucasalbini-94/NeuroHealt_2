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
            List<Paciente> pacientesEnEspera = servicioPacientes.ObtenerColaEspera();

            foreach (Paciente p in pacientesEnEspera)
            {
                dgvColaEspera.Rows.Add(
                    p.Dni,
                    p.NombreApellido,
                    p.Edad,
                    p.Motivo,
                    "Sin Evaluar",
                    p is PacienteGuardia ? "Guardia" : "Pediátrico"
                );
            }

            List<Paciente> pacientesAdmitidos = servicioPacientes.ObtenerPacientesAdmitidos();

            foreach (Paciente p in pacientesAdmitidos)
            {
                dgvPacientesAdmitidos.Rows.Add(
                    p.Dni,
                    p.NombreApellido,
                    p.Edad,
                    p.Motivo,
                    p.Nivel.ToString(),
                    p is PacienteGuardia ? "Guardia" : "Pediátrico");
            }
        }


        private async void btnEvaluarPaciente_Click(object sender, EventArgs e)
        {
            if ( servicioPacientes.ContarEnEspera() == 0)
            {
                MessageBox.Show("No hay pacientes en la cola de espera.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // Fragmento orientativo provisto para simular tarea en segundo plano.
            btnEvaluarPaciente.Enabled = false;
            progressBarEvaluacion.Visible = true;
            progressBarEvaluacion.Style = ProgressBarStyle.Marquee;
            lblEstado.Text = "Evaluando";

            await Task.Run(() =>
            {
                Thread.Sleep(1000);
            });

            progressBarEvaluacion.Visible = false;
            btnEvaluarPaciente.Enabled = true;

            var resultado = servicioPacientes.EvaluarSiguientePaciente();
            lblEstado.Text = resultado.Mensaje;
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
            long dniPaciente = Convert.ToInt64(fila.Cells["Dni"].Value);

            // Pasa la instancia de servicioObservaciones al constructor
            FrmObservaciones ventanaObs = new FrmObservaciones(servicioObservaciones, dniPaciente);
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
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void dgvPacientesAdmitidos_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            // TODO: Aplicar color según el nivel de urgencia.

            if (dgvPacientesAdmitidos.Columns[e.ColumnIndex].Name == "Urgencia" && e.Value != null)
            {
                string? nivel = e.Value.ToString();

                if (nivel == "Rojo")
                {
                    e.CellStyle.BackColor = Color.Red;
                    e.Value = "";
                }
                else if (nivel == "Amarillo")
                {
                    e.CellStyle.BackColor = Color.Yellow;
                    e.Value = "";
                }
                else if (nivel == "Verde")
                {
                    e.CellStyle.BackColor = Color.Green;
                    e.Value = "";
                }
            }
        }

        private void lblTitulo_Click(object sender, EventArgs e)
        {

        }
    }
}
