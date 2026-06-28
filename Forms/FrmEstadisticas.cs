using NeuroHealthDesktop.Servicios;
using System;
using System.Windows.Forms;

namespace NeuroHealthDesktop.Forms
{
    public partial class FrmEstadisticas : Form
    {
        private ServicioPacientes servicioPacientes;

        public FrmEstadisticas(ServicioPacientes servicioPacientes)
        {
            InitializeComponent();
            this.servicioPacientes = servicioPacientes;
            CargarEstadisticas();
        }

        private void CargarEstadisticas()
        {
            // TODO: Solicitar estadísticas al servicio y mostrarlas.
            lblEnEsperaValor.Text = servicioPacientes.ContarEnEspera().ToString();
            lblAdmitidosValor.Text = servicioPacientes.ContarAdmitidos().ToString();
            lblVerdesValor.Text = servicioPacientes.ContarPorNivel(NivelUrgencia.Verde).ToString();
            lblAmarillosValor.Text = servicioPacientes.ContarPorNivel(NivelUrgencia.Amarillo).ToString();
            lblRojosValor.Text = servicioPacientes.ContarPorNivel(NivelUrgencia.Rojo).ToString();
            lblEdadPromedioValor.Text = servicioPacientes.CalcularEdadPromedioAdmitidos().ToString("0");
            lblPorcentajeCriticosValor.Text = $"{servicioPacientes.CalcularPorcentajeCriticos().ToString("0.00")}%";
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            CargarEstadisticas();
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
