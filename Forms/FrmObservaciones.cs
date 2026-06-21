using NeuroHealthDesktop.Servicios;
using System;
using System.Windows.Forms;

namespace NeuroHealthDesktop.Forms
{
    public partial class FrmObservaciones : Form
    {
        private ServicioObservaciones servicioObservaciones;

        public FrmObservaciones(ServicioObservaciones servicioObservaciones)
        {
            InitializeComponent();
            this.servicioObservaciones = servicioObservaciones;
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            // TODO: Agregar observación mediante el servicio.
            MessageBox.Show("Aquí se agregará una observación.");
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            // TODO: Buscar observaciones por DNI.
            MessageBox.Show("Aquí se buscarán observaciones por DNI.");
        }

        private void CargarObservaciones(long dni)
        {
            // TODO: Cargar observaciones en la lista.
            lstObservaciones.Items.Clear();
            lstObservaciones.Items.Add("Aquí se mostrarán las observaciones del paciente.");
        }

        private long ObtenerDni()
        {
            // TODO: Obtener y validar DNI.
            return 0;
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
