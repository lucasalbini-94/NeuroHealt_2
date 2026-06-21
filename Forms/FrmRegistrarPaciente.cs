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
            cmbTipoPaciente.SelectedItem = TipoPaciente.Guardia;
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

            long dni = long.Parse(txtDni.Text);
            string nombreApellido = txtNombreApellido.Text;
            int edad = (int)nudEdad.Value;
            MotivoConsulta motivo = (MotivoConsulta)cmbMotivo.SelectedItem;

            // ACÁ NO TOMA LOS DATOS DEL FORMULARIO, CREA UN PACIENTE DE PRUEBA
            if (cmbTipoPaciente.Text == "Guardia")
            {
                // VALIDA Y TRANSFORMACIONS DE LOS DATOS DE LOS CAMPOS DEL FORMULARIO

                //bool resultadoDni = long.TryParse(txtDni.Text, out long dni);

                // Esto siempre es decimal y dentro del rango elegido, NO HACE FALTA validarlo
                //int edad = (int)nudEdad.Value;

                // Convierte el motivo elegido en el valor del enum de motivo de consulta
                // Siempre lo puede hacer porque estan cargados los valores del enum en el comboBox

                //bool resultadoMotivo = Enum.TryParse(cmbMotivo.Text, out MotivoConsulta motivo);

                // PASAR EL VALOR DEL CHECKOBX A BOOLEANO
                bool resultadoCamilla = chkRequiereCamilla.Checked;

                PacienteGuardia nuevoPaciente = new PacienteGuardia(dni,
                                                                    txtNombreApellido.Text,
                                                                    edad,
                                                                    motivo,
                                                                    new SignosVitales(1, 130, "85", 90, 38),
                                                                    resultadoCamilla);
                servicioPacientes.RegistrarPaciente(nuevoPaciente);
            }
            else
            {
                // VALIDA Y TRANSFORMACIONS DE LOS DATOS DE LOS CAMPOS DEL FORMULARIO
                // VALIDACION DEL DNI, QUE SOLO TENGA NUMEROS Y NO ESTE VACIO
                bool soloDigitos = txtDni.Text.Replace(" ", "").All(char.IsDigit);

                if (!soloDigitos)
                {
                    MessageBox.Show("El DNI solo puede contener números.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtDni.Clear();
                    return;
                }

                if (txtDni.Text.Replace(" ", "") == "")
                {
                    MessageBox.Show("El DNI no puede estar vacío.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                bool resultadoDni = long.TryParse(txtDni.Text, out long dni);

                // VALIDACION DEL NOMBRE Y APELLIDO, QUE SOLO TENGA LETRAS Y NO ESTE VACIO
                bool soloLetras = txtNombreApellido.Text.Replace(" ", "").All(char.IsLetter);
                // Si hay algún caracter que no va o el campo esta vacio, muestra el mensaje de error
                if (!soloLetras)
                {
                    MessageBox.Show("El NOMBRE Y APELLIDO solo puede contener letras.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtNombreApellido.Clear();
                    return;
                }
                if (txtNombreApellido.Text.Replace(" ", "") == "")
                {
                    MessageBox.Show("El NOMBRE Y APELLIDO no puede estar vacío.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Esto siempre es decimal y dentro del rango elegido, NO HACE FALTA validarlo
                int edad = (int)nudEdad.Value;

                // Convierte el motivo elegido en el valor del enum de motivo de consulta
                // Siempre lo puede hacer porque estan cargados los valores del enum en el comboBox


                bool resultadoMotivo = Enum.TryParse(cmbMotivo.Text, out MotivoConsulta motivo);

                if (!resultadoMotivo)
                {
                    MessageBox.Show("El MOTIVO no puede estar vacio.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // VALIDACION DEL NOMBRE Y APELLIDO, QUE SOLO TENGA LETRAS Y NO ESTE VACIO
                bool soloLetrasAdulto = txtAdultoResponsable.Text.Replace(" ", "").All(char.IsLetter);
                // Si hay algún caracter que no va o el campo esta vacio, muestra el mensaje de error
                if (!soloLetrasAdulto)
                {
                    MessageBox.Show("El NOMBRE Y APELLIDO DEL ADULTO RESPONSABLE solo puede contener letras.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtAdultoResponsable.Clear();
                    return;
                }
                if (txtAdultoResponsable.Text.Replace(" ", "") == "")
                {
                    MessageBox.Show("El NOMBRE Y APELLIDO DEL ADULTO RESPONSABLE no puede estar vacío.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                PacientePediatrico nuevoPaciente = new PacientePediatrico(dni,
                                                    txtNombreApellido.Text,
                                                    edad,
                                                    motivo,
                                                    new SignosVitales(1, 130, "85", 90, 38),
                                                    txtAdultoResponsable.Text);
                servicioPacientes.RegistrarPaciente(nuevoPaciente);
            }

            this.Close();

        }

        private Paciente CrearPacienteDesdeFormulario()
        {
            // TODO: Leer datos y crear PacienteGuardia o PacientePediatrico.
            throw new NotImplementedException("Completar creación del paciente desde el formulario.");
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

        private bool ValidarCampos()
        {
            // VALIDACION DEL DNI, QUE SOLO TENGA NUMEROS Y NO ESTE VACIO
            bool soloDigitos = txtDni.Text.Replace(" ", "").All(char.IsDigit);
            bool vacio = txtDni.Text.Replace(" ", "") == "";

            if (!soloDigitos)
            {
                MessageBox.Show("El DNI solo puede contener números.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtDni.Clear();
                return false;
            }
            if (vacio)
            {
                MessageBox.Show("El DNI no puede estar vacío.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            // VALIDACION DEL NOMBRE Y APELLIDO, QUE SOLO TENGA LETRAS Y NO ESTE VACIO
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
            return true;
        }
    }
}
