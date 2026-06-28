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
            // Obtener y validar el DNI
            long dni = ObtenerDni();
            if (dni == 0) return; // Si es 0, la validación falló y ya mostró el mensaje.

            // Valida que la observación no esté vacía
            //
            string textoObservacion = txtObservacion.Text.Trim();
            if (string.IsNullOrWhiteSpace(textoObservacion))
            {
                MessageBox.Show("Por favor, ingrese el texto de la observación.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
               
               
                servicioObservaciones.AgregarObservacion(dni, textoObservacion);

                MessageBox.Show("Observación agregada con éxito.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                
                txtObservacion.Clear();
                CargarObservaciones(dni);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al guardar la observación: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            long dni = ObtenerDni();
            if (dni != 0)
            {
                CargarObservaciones(dni);
            }
        }

        private void CargarObservaciones(long dni)
        {
            lstObservaciones.Items.Clear();

            try
            {
                
                var lista = servicioObservaciones.ObtenerObservacionesPorPaciente(dni);

                if (lista == null || lista.Count == 0)
                {
                    lstObservaciones.Items.Add("No se encontraron observaciones para el paciente.");
                    return;
                }

                foreach (var obs in lista)
                {
                    lstObservaciones.Items.Add(obs);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar las observaciones: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private long ObtenerDni()
        {
            
            string inputDni = txtDni.Text.Trim();

            if (string.IsNullOrWhiteSpace(inputDni))
            {
                MessageBox.Show("Por favor, ingrese el DNI del paciente.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return 0;
            }

            // Valida que realmente sea un número de tipo long
            if (!long.TryParse(inputDni, out long dni))
            {
                MessageBox.Show("El DNI debe ser un número válido.", "Error de formato", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return 0;
            }

            return dni;
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void lblTitulo_Click(object sender, EventArgs e)
        {
            // Este método quedó vacío por un doble clic accidental.
        }
    }
}