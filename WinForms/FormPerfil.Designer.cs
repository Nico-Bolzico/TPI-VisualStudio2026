namespace WinForms
{
    partial class FormPerfil
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            lblUsuario = new Label();
            lblLegajo = new Label();
            lblCategoria = new Label();
            lblNombre = new Label();
            lblApellido = new Label();
            lblEmail = new Label();
            lblDireccion = new Label();
            lblTelefono = new Label();
            lblFechaNacimiento = new Label();
            txtUsuario = new TextBox();
            txtLegajo = new TextBox();
            txtCategoria = new TextBox();
            txtNombre = new TextBox();
            txtApellido = new TextBox();
            txtEmail = new TextBox();
            txtDireccion = new TextBox();
            txtTelefono = new TextBox();
            dtpFechaNacimiento = new DateTimePicker();
            btnGuardar = new Button();
            btnCerrar = new Button();
            btnCambiarPassword = new Button();
            SuspendLayout();
            // 
            // lblUsuario
            // 
            lblUsuario.AutoSize = true;
            lblUsuario.Location = new Point(12, 9);
            lblUsuario.Name = "lblUsuario";
            lblUsuario.Size = new Size(60, 20);
            lblUsuario.TabIndex = 0;
            lblUsuario.Text = "Usuario";
            // 
            // lblLegajo
            // 
            lblLegajo.AutoSize = true;
            lblLegajo.Location = new Point(190, 9);
            lblLegajo.Name = "lblLegajo";
            lblLegajo.Size = new Size(54, 20);
            lblLegajo.TabIndex = 0;
            lblLegajo.Text = "Legajo";
            // 
            // lblCategoria
            // 
            lblCategoria.AutoSize = true;
            lblCategoria.Location = new Point(330, 9);
            lblCategoria.Name = "lblCategoria";
            lblCategoria.Size = new Size(74, 20);
            lblCategoria.TabIndex = 0;
            lblCategoria.Text = "Categoría";
            // 
            // lblNombre
            // 
            lblNombre.AutoSize = true;
            lblNombre.Location = new Point(12, 80);
            lblNombre.Name = "lblNombre";
            lblNombre.Size = new Size(64, 20);
            lblNombre.TabIndex = 0;
            lblNombre.Text = "Nombre";
            // 
            // lblApellido
            // 
            lblApellido.AutoSize = true;
            lblApellido.Location = new Point(280, 80);
            lblApellido.Name = "lblApellido";
            lblApellido.Size = new Size(66, 20);
            lblApellido.TabIndex = 0;
            lblApellido.Text = "Apellido";
            // 
            // lblEmail
            // 
            lblEmail.AutoSize = true;
            lblEmail.Location = new Point(12, 150);
            lblEmail.Name = "lblEmail";
            lblEmail.Size = new Size(46, 20);
            lblEmail.TabIndex = 0;
            lblEmail.Text = "Email";
            // 
            // lblDireccion
            // 
            lblDireccion.AutoSize = true;
            lblDireccion.Location = new Point(12, 220);
            lblDireccion.Name = "lblDireccion";
            lblDireccion.Size = new Size(72, 20);
            lblDireccion.TabIndex = 0;
            lblDireccion.Text = "Dirección";
            // 
            // lblTelefono
            // 
            lblTelefono.AutoSize = true;
            lblTelefono.Location = new Point(280, 220);
            lblTelefono.Name = "lblTelefono";
            lblTelefono.Size = new Size(67, 20);
            lblTelefono.TabIndex = 0;
            lblTelefono.Text = "Teléfono";
            // 
            // lblFechaNacimiento
            // 
            lblFechaNacimiento.AutoSize = true;
            lblFechaNacimiento.Location = new Point(12, 290);
            lblFechaNacimiento.Name = "lblFechaNacimiento";
            lblFechaNacimiento.Size = new Size(150, 20);
            lblFechaNacimiento.TabIndex = 0;
            lblFechaNacimiento.Text = "Fecha de nacimiento";
            // 
            // txtUsuario
            // 
            txtUsuario.Location = new Point(12, 32);
            txtUsuario.Name = "txtUsuario";
            txtUsuario.ReadOnly = true;
            txtUsuario.Size = new Size(160, 27);
            txtUsuario.TabIndex = 0;
            // 
            // txtLegajo
            // 
            txtLegajo.Location = new Point(190, 32);
            txtLegajo.Name = "txtLegajo";
            txtLegajo.ReadOnly = true;
            txtLegajo.Size = new Size(120, 27);
            txtLegajo.TabIndex = 1;
            // 
            // txtCategoria
            // 
            txtCategoria.Location = new Point(330, 32);
            txtCategoria.Name = "txtCategoria";
            txtCategoria.ReadOnly = true;
            txtCategoria.Size = new Size(160, 27);
            txtCategoria.TabIndex = 2;
            // 
            // txtNombre
            // 
            txtNombre.Location = new Point(12, 103);
            txtNombre.Name = "txtNombre";
            txtNombre.Size = new Size(250, 27);
            txtNombre.TabIndex = 3;
            // 
            // txtApellido
            // 
            txtApellido.Location = new Point(280, 103);
            txtApellido.Name = "txtApellido";
            txtApellido.Size = new Size(260, 27);
            txtApellido.TabIndex = 4;
            // 
            // txtEmail
            // 
            txtEmail.Location = new Point(12, 173);
            txtEmail.Name = "txtEmail";
            txtEmail.Size = new Size(528, 27);
            txtEmail.TabIndex = 5;
            // 
            // txtDireccion
            // 
            txtDireccion.Location = new Point(12, 243);
            txtDireccion.Name = "txtDireccion";
            txtDireccion.Size = new Size(250, 27);
            txtDireccion.TabIndex = 6;
            // 
            // txtTelefono
            // 
            txtTelefono.Location = new Point(280, 243);
            txtTelefono.Name = "txtTelefono";
            txtTelefono.Size = new Size(260, 27);
            txtTelefono.TabIndex = 7;
            // 
            // dtpFechaNacimiento
            // 
            dtpFechaNacimiento.Format = DateTimePickerFormat.Short;
            dtpFechaNacimiento.Location = new Point(12, 313);
            dtpFechaNacimiento.Name = "dtpFechaNacimiento";
            dtpFechaNacimiento.Size = new Size(160, 27);
            dtpFechaNacimiento.TabIndex = 8;
            // 
            // btnGuardar
            // 
            btnGuardar.Location = new Point(340, 355);
            btnGuardar.Name = "btnGuardar";
            btnGuardar.Size = new Size(100, 30);
            btnGuardar.TabIndex = 9;
            btnGuardar.Text = "Guardar";
            btnGuardar.UseVisualStyleBackColor = true;
            btnGuardar.Click += btnGuardar_Click;
            // 
            // btnCambiarPassword
            // 
            btnCambiarPassword.Location = new Point(12, 355);
            btnCambiarPassword.Name = "btnCambiarPassword";
            btnCambiarPassword.Size = new Size(170, 30);
            btnCambiarPassword.TabIndex = 11;
            btnCambiarPassword.Text = "Cambiar contraseña";
            btnCambiarPassword.UseVisualStyleBackColor = true;
            btnCambiarPassword.Click += btnCambiarPassword_Click;
            // 
            // btnCerrar
            // 
            btnCerrar.DialogResult = DialogResult.Cancel;
            btnCerrar.Location = new Point(446, 355);
            btnCerrar.Name = "btnCerrar";
            btnCerrar.Size = new Size(94, 30);
            btnCerrar.TabIndex = 10;
            btnCerrar.Text = "Cerrar";
            btnCerrar.UseVisualStyleBackColor = true;
            // 
            // FormPerfil
            // 
            AcceptButton = btnGuardar;
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            CancelButton = btnCerrar;
            ClientSize = new Size(560, 405);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Controls.Add(btnCambiarPassword);
            Controls.Add(btnCerrar);
            Controls.Add(btnGuardar);
            Controls.Add(dtpFechaNacimiento);
            Controls.Add(txtTelefono);
            Controls.Add(txtDireccion);
            Controls.Add(txtEmail);
            Controls.Add(txtApellido);
            Controls.Add(txtNombre);
            Controls.Add(txtCategoria);
            Controls.Add(txtLegajo);
            Controls.Add(txtUsuario);
            Controls.Add(lblFechaNacimiento);
            Controls.Add(lblTelefono);
            Controls.Add(lblDireccion);
            Controls.Add(lblEmail);
            Controls.Add(lblApellido);
            Controls.Add(lblNombre);
            Controls.Add(lblCategoria);
            Controls.Add(lblLegajo);
            Controls.Add(lblUsuario);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "FormPerfil";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Mi perfil";
            Load += FormPerfil_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblUsuario;
        private Label lblLegajo;
        private Label lblCategoria;
        private Label lblNombre;
        private Label lblApellido;
        private Label lblEmail;
        private Label lblDireccion;
        private Label lblTelefono;
        private Label lblFechaNacimiento;
        private TextBox txtUsuario;
        private TextBox txtLegajo;
        private TextBox txtCategoria;
        private TextBox txtNombre;
        private TextBox txtApellido;
        private TextBox txtEmail;
        private TextBox txtDireccion;
        private TextBox txtTelefono;
        private DateTimePicker dtpFechaNacimiento;
        private Button btnGuardar;
        private Button btnCerrar;
        private Button btnCambiarPassword;
    }
}
