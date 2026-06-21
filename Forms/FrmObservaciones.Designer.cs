namespace NeuroHealthDesktop.Forms
{
    partial class FrmObservaciones
    {
        private System.ComponentModel.IContainer components = null;
        private Label lblTitulo;
        private Label lblDni;
        private Label lblObservacion;
        private Label lblListado;
        private Label lblAyuda;
        private TextBox txtDni;
        private TextBox txtObservacion;
        private Button btnBuscar;
        private Button btnAgregar;
        private Button btnCerrar;
        private ListBox lstObservaciones;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            lblTitulo = new Label();
            lblDni = new Label();
            lblObservacion = new Label();
            lblListado = new Label();
            lblAyuda = new Label();
            txtDni = new TextBox();
            txtObservacion = new TextBox();
            btnBuscar = new Button();
            btnAgregar = new Button();
            btnCerrar = new Button();
            lstObservaciones = new ListBox();
            SuspendLayout();
            // 
            // lblTitulo
            // 
            lblTitulo.AutoSize = true;
            lblTitulo.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            lblTitulo.Location = new Point(20, 18);
            lblTitulo.Name = "lblTitulo";
            lblTitulo.Size = new Size(287, 30);
            lblTitulo.TabIndex = 0;
            lblTitulo.Text = "Observaciones - Esqueleto";
            // 
            // lblDni
            // 
            lblDni.AutoSize = true;
            lblDni.Location = new Point(24, 94);
            lblDni.Name = "lblDni";
            lblDni.Size = new Size(94, 15);
            lblDni.TabIndex = 2;
            lblDni.Text = "DNI del paciente";
            // 
            // lblObservacion
            // 
            lblObservacion.AutoSize = true;
            lblObservacion.Location = new Point(24, 138);
            lblObservacion.Name = "lblObservacion";
            lblObservacion.Size = new Size(73, 15);
            lblObservacion.TabIndex = 5;
            lblObservacion.Text = "Observación";
            // 
            // lblListado
            // 
            lblListado.AutoSize = true;
            lblListado.Location = new Point(24, 260);
            lblListado.Name = "lblListado";
            lblListado.Size = new Size(139, 15);
            lblListado.TabIndex = 8;
            lblListado.Text = "Listado de observaciones";
            // 
            // lblAyuda
            // 
            lblAyuda.AutoSize = true;
            lblAyuda.Location = new Point(24, 55);
            lblAyuda.Name = "lblAyuda";
            lblAyuda.Size = new Size(365, 15);
            lblAyuda.TabIndex = 1;
            lblAyuda.Text = "Aquí se deberá buscar un paciente, cargar y listar sus observaciones.";
            // 
            // txtDni
            // 
            txtDni.Location = new Point(150, 90);
            txtDni.Name = "txtDni";
            txtDni.Size = new Size(180, 23);
            txtDni.TabIndex = 3;
            // 
            // txtObservacion
            // 
            txtObservacion.Location = new Point(150, 135);
            txtObservacion.Multiline = true;
            txtObservacion.Name = "txtObservacion";
            txtObservacion.Size = new Size(520, 95);
            txtObservacion.TabIndex = 6;
            // 
            // btnBuscar
            // 
            btnBuscar.Location = new Point(350, 88);
            btnBuscar.Name = "btnBuscar";
            btnBuscar.Size = new Size(150, 30);
            btnBuscar.TabIndex = 4;
            btnBuscar.Text = "Aquí puede ir: Buscar";
            btnBuscar.Click += btnBuscar_Click;
            // 
            // btnAgregar
            // 
            btnAgregar.Location = new Point(690, 135);
            btnAgregar.Name = "btnAgregar";
            btnAgregar.Size = new Size(150, 38);
            btnAgregar.TabIndex = 7;
            btnAgregar.Text = "Aquí puede ir: Agregar";
            btnAgregar.Click += btnAgregar_Click;
            // 
            // btnCerrar
            // 
            btnCerrar.Location = new Point(690, 460);
            btnCerrar.Name = "btnCerrar";
            btnCerrar.Size = new Size(150, 35);
            btnCerrar.TabIndex = 10;
            btnCerrar.Text = "Cerrar";
            btnCerrar.Click += btnCerrar_Click;
            // 
            // lstObservaciones
            // 
            lstObservaciones.Location = new Point(24, 285);
            lstObservaciones.Name = "lstObservaciones";
            lstObservaciones.Size = new Size(816, 154);
            lstObservaciones.TabIndex = 9;
            // 
            // FrmObservaciones
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(870, 515);
            Controls.Add(lblTitulo);
            Controls.Add(lblAyuda);
            Controls.Add(lblDni);
            Controls.Add(txtDni);
            Controls.Add(btnBuscar);
            Controls.Add(lblObservacion);
            Controls.Add(txtObservacion);
            Controls.Add(btnAgregar);
            Controls.Add(lblListado);
            Controls.Add(lstObservaciones);
            Controls.Add(btnCerrar);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "FrmObservaciones";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Observaciones";
            ResumeLayout(false);
            PerformLayout();
        }
    }
}
