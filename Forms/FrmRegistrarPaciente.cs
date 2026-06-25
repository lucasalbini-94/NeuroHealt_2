using NeuroHealthDesktop.Servicios;
using System;
using System.Diagnostics.Eventing.Reader;
using System.Net;
using System.Windows.Forms;

namespace NeuroHealthDesktop.Forms
{
    public partial class FrmRegistrarPaciente : Form
    {
        private ServicioPacientes servicioPacientes;

        public FrmRegistrarPaciente(ServicioPacientes servicioPacientes)
        {
            InitializeComponent();
            this.servicioPacientes = servicioPacientes;
            CargarCombos();
            ConfigurarEstadoInicial();
        }

        private void CargarCombos()
        {
            // TODO: Cargar combos con los valores de los enums.
            cmbMotivo.DataSource = Enum.GetValues(typeof(MotivoConsulta));
            // Se pone en blanco el motivo
            cmbMotivo.SelectedIndex = -1;

            cmbTipoPaciente.DataSource = Enum.GetValues(typeof(TipoPaciente));
        }

        private void ConfigurarEstadoInicial()
        {
            // TODO: Definir estado inicial del formulario.
            cmbTipoPaciente.SelectedItem = TipoPaciente.Pediatrico;
        }

        private void cmbTipoPaciente_SelectedIndexChanged(object sender, EventArgs e)
        {
            chkRequiereCamilla.Enabled = (cmbTipoPaciente.Text == "Guardia");
            txtAdultoResponsable.Enabled = (cmbTipoPaciente.Text == "Pediatrico");

        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (!ValidarCampos())
                return;

            try
            {
                Paciente nuevo = CrearPacienteDesdeFormulario();

                ResultadoOperacion<Paciente> resultado = servicioPacientes.RegistrarPaciente(nuevo);

                if (resultado.Exito)
                {
                    MessageBox.Show(resultado.Mensaje, "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
                else
                {
                    MessageBox.Show(resultado.Mensaje, "Error de Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ocurrió un error al registrar el paciente: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private Paciente CrearPacienteDesdeFormulario()
        {
            // TODO: Leer datos y crear PacienteGuardia o PacientePediatrico.
            long dni = long.Parse(txtDni.Text.Trim());
            string nombreApellido = txtNombreApellido.Text.Trim();
            int edad = (int)nudEdad.Value;
            MotivoConsulta motivo = (MotivoConsulta)cmbMotivo.SelectedItem;

            int pulso = (int)nudPulso.Value;
            double temperatura = (double)nudTemperatura.Value;
            string presion = txtPresion.Text.Trim();
            int saturacion = (int)nudSaturacion.Value;
            int dolor = (int)nudDolor.Value;

            SignosVitales signos = new SignosVitales(pulso, temperatura, presion, saturacion, dolor);

            if (cmbTipoPaciente.Text == "Guardia")
            {
                bool requiereCamilla = chkRequiereCamilla.Checked;
                return new PacienteGuardia(dni, nombreApellido, edad, motivo, signos, requiereCamilla);
            }
            else if (cmbTipoPaciente.Text == "Pediatría")
            {
                string adultoResponsable = txtAdultoResponsable.Text.Trim();
                return new PacientePediatrico(dni, nombreApellido, edad, motivo, signos, adultoResponsable);
            }

            return null;
        }

        private long ObtenerDni()
        {
            // TODO: Obtener y validar DNI.
            return 0;
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void nudEdad_ValueChanged(object sender, EventArgs e)
        {
            if (nudEdad.Value < 18)
            {
                cmbTipoPaciente.SelectedItem = TipoPaciente.Pediatrico;
                txtAdultoResponsable.Enabled = true;
            }
            else
            {
                cmbTipoPaciente.SelectedItem = TipoPaciente.Guardia;
                txtAdultoResponsable.Enabled = false;
            }
        }

        private bool ValidarCampos()
        {
            // VALIDACION DEL DNI, QUE SOLO TENGA NUMEROS Y NO ESTE VACIO

            if (!txtDni.Text.Replace(" ", "").All(char.IsDigit))
            {
                MessageBox.Show("El DNI solo puede contener números.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtDni.Clear();
                return false;
            }
            if (txtDni.Text.Replace(" ", "") == "")
            {
                MessageBox.Show("El DNI no puede estar vacío.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            // VALIDACION DEL NOMBRE Y APELLIDO y PRESION, QUE SOLO TENGA LETRAS Y NO ESTE VACIO
            bool soloLetras = txtNombreApellido.Text.Replace(" ", "").All(char.IsLetter);
            // Si hay algún caracter que no va o el campo esta vacio, muestra el mensaje de error
            if (!soloLetras)
            {
                MessageBox.Show("El NOMBRE Y APELLIDO solo puede contener letras.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtNombreApellido.Clear();
                return false;
            }
            if (txtNombreApellido.Text.Replace(" ", "") == "")
            {
                MessageBox.Show("El NOMBRE Y APELLIDO no puede estar vacío.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (cmbMotivo.Text == "")
            {
                MessageBox.Show("El MOTIVO no puede estar vacio.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (txtPresion.Text.Replace(" ", "") == "")
            {
                MessageBox.Show("La PRESION no puede estar vacia.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (txtPresion.Text.Replace(" ", "").All(char.IsDigit))
            {
                MessageBox.Show("La PRESION solo puede contener números.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtPresion.Clear();
                return false;
            }
            // SI EL CAMPO ADULTO RESPONSABLE ESTA HABILITADO, VE QUE NO ESTÉ VACIO Y QUE SEAN SOLO LETRAS
            if (txtAdultoResponsable.Enabled)
            {
                // Repetí el codigo para validar el nombre del paciente, pero para el adulto responsable. Se podria hacer una funcion
                bool soloLetrasAdulto = txtAdultoResponsable.Text.Replace(" ", "").All(char.IsLetter);
                // Si hay algún caracter que no va o el campo esta vacio, muestra el mensaje de error
                if (!soloLetrasAdulto)
                {
                    MessageBox.Show("El NOMBRE DEL ADULTO RESPONSABLE solo puede contener letras.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtAdultoResponsable.Clear();
                    return false;
                }
                if (txtAdultoResponsable.Text.Replace(" ", "") == "")
                {
                    MessageBox.Show("El NOMBRE DEL ADULTO RESPONSABLE no puede estar vacío.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
            return true;
        }
    }
}
