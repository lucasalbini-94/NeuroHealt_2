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
            lblEnEsperaValor.Text = "0";
            lblAdmitidosValor.Text = "0";
            lblVerdesValor.Text = "0";
            lblAmarillosValor.Text = "0";
            lblRojosValor.Text = "0";
            lblEdadPromedioValor.Text = "0.00";
            lblPorcentajeCriticosValor.Text = "0.00 %";
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
