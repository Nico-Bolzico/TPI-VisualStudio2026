namespace WinForms
{
    partial class FormPersonaEdit
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
            txtLegajo = new TextBox();
            txtNombre = new TextBox();
            txtDireccion = new TextBox();
            txtEmail = new TextBox();
            txtApellido = new TextBox();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            txtTelefono = new TextBox();
            label5 = new Label();
            label6 = new Label();
            label7 = new Label();
            dtpFechaNacimiento = new DateTimePicker();
            cmbTipoPersona = new ComboBox();
            label8 = new Label();
            nudIdPlan = new NumericUpDown();
            label9 = new Label();
            btnAceptar = new Button();
            btnCancelar = new Button();
            ((System.ComponentModel.ISupportInitialize)nudIdPlan).BeginInit();
            SuspendLayout();
            // 
            // txtLegajo
            // 
            txtLegajo.Location = new Point(12, 30);
            txtLegajo.Name = "txtLegajo";
            txtLegajo.Size = new Size(125, 27);
            txtLegajo.TabIndex = 0;
            // 
            // txtNombre
            // 
            txtNombre.Location = new Point(12, 95);
            txtNombre.Name = "txtNombre";
            txtNombre.Size = new Size(219, 27);
            txtNombre.TabIndex = 1;
            // 
            // txtDireccion
            // 
            txtDireccion.Location = new Point(12, 171);
            txtDireccion.Name = "txtDireccion";
            txtDireccion.Size = new Size(125, 27);
            txtDireccion.TabIndex = 2;
            // 
            // txtEmail
            // 
            txtEmail.Location = new Point(12, 241);
            txtEmail.Name = "txtEmail";
            txtEmail.Size = new Size(125, 27);
            txtEmail.TabIndex = 3;
            // 
            // txtApellido
            // 
            txtApellido.Location = new Point(237, 95);
            txtApellido.Name = "txtApellido";
            txtApellido.Size = new Size(228, 27);
            txtApellido.TabIndex = 4;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 9);
            label1.Name = "label1";
            label1.Size = new Size(54, 20);
            label1.TabIndex = 5;
            label1.Text = "Legajo";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(12, 72);
            label2.Name = "label2";
            label2.Size = new Size(64, 20);
            label2.TabIndex = 6;
            label2.Text = "Nombre";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(237, 72);
            label3.Name = "label3";
            label3.Size = new Size(66, 20);
            label3.TabIndex = 7;
            label3.Text = "Apellido";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(12, 148);
            label4.Name = "label4";
            label4.Size = new Size(72, 20);
            label4.TabIndex = 8;
            label4.Text = "Direccion";
            // 
            // txtTelefono
            // 
            txtTelefono.Location = new Point(12, 313);
            txtTelefono.Name = "txtTelefono";
            txtTelefono.Size = new Size(125, 27);
            txtTelefono.TabIndex = 9;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(12, 218);
            label5.Name = "label5";
            label5.Size = new Size(46, 20);
            label5.TabIndex = 10;
            label5.Text = "Email";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(12, 292);
            label6.Name = "label6";
            label6.Size = new Size(67, 20);
            label6.TabIndex = 11;
            label6.Text = "Telefono";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(175, 148);
            label7.Name = "label7";
            label7.Size = new Size(128, 20);
            label7.TabIndex = 12;
            label7.Text = "Fecha Nacimiento";
            // 
            // dtpFechaNacimiento
            // 
            dtpFechaNacimiento.Format = DateTimePickerFormat.Short;
            dtpFechaNacimiento.Location = new Point(175, 171);
            dtpFechaNacimiento.Name = "dtpFechaNacimiento";
            dtpFechaNacimiento.Size = new Size(128, 27);
            dtpFechaNacimiento.TabIndex = 13;
            // 
            // cmbTipoPersona
            // 
            cmbTipoPersona.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbTipoPersona.FormattingEnabled = true;
            cmbTipoPersona.Items.AddRange(new object[] { "Alumno", "Profesor" });
            cmbTipoPersona.Location = new Point(175, 240);
            cmbTipoPersona.Name = "cmbTipoPersona";
            cmbTipoPersona.Size = new Size(151, 28);
            cmbTipoPersona.TabIndex = 14;
            cmbTipoPersona.SelectedIndexChanged += cmbTipoPersona_SelectedIndexChanged;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(175, 217);
            label8.Name = "label8";
            label8.Size = new Size(74, 20);
            label8.TabIndex = 15;
            label8.Text = "Categoría";
            // 
            // nudIdPlan
            // 
            nudIdPlan.Location = new Point(176, 314);
            nudIdPlan.Name = "nudIdPlan";
            nudIdPlan.Size = new Size(150, 27);
            nudIdPlan.TabIndex = 16;
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Location = new Point(176, 292);
            label9.Name = "label9";
            label9.Size = new Size(69, 20);
            label9.TabIndex = 17;
            label9.Text = "Nro. Plan";
            // 
            // btnAceptar
            // 
            btnAceptar.DialogResult = DialogResult.OK;
            btnAceptar.Location = new Point(509, 311);
            btnAceptar.Name = "btnAceptar";
            btnAceptar.Size = new Size(94, 29);
            btnAceptar.TabIndex = 18;
            btnAceptar.Text = "Aceptar";
            btnAceptar.UseVisualStyleBackColor = true;
            // 
            // btnCancelar
            // 
            btnCancelar.DialogResult = DialogResult.Cancel;
            btnCancelar.Location = new Point(618, 312);
            btnCancelar.Name = "btnCancelar";
            btnCancelar.Size = new Size(94, 29);
            btnCancelar.TabIndex = 19;
            btnCancelar.Text = "Cancelar";
            btnCancelar.UseVisualStyleBackColor = true;
            // 
            // FormPersonaEdit
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(btnCancelar);
            Controls.Add(btnAceptar);
            Controls.Add(label9);
            Controls.Add(nudIdPlan);
            Controls.Add(label8);
            Controls.Add(cmbTipoPersona);
            Controls.Add(dtpFechaNacimiento);
            Controls.Add(label7);
            Controls.Add(label6);
            Controls.Add(label5);
            Controls.Add(txtTelefono);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(txtApellido);
            Controls.Add(txtEmail);
            Controls.Add(txtDireccion);
            Controls.Add(txtNombre);
            Controls.Add(txtLegajo);
            Name = "FormPersonaEdit";
            Text = "Form1";
            Load += FormPersonaEdit_Load;
            FormClosing += FormPersonaEdit_FormClosing;
            ((System.ComponentModel.ISupportInitialize)nudIdPlan).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox txtLegajo;
        private TextBox txtNombre;
        private TextBox txtDireccion;
        private TextBox txtEmail;
        private TextBox txtApellido;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private TextBox txtTelefono;
        private Label label5;
        private Label label6;
        private Label label7;
        private DateTimePicker dtpFechaNacimiento;
        private ComboBox cmbTipoPersona;
        private Label label8;
        private NumericUpDown nudIdPlan;
        private Label label9;
        private Button btnAceptar;
        private Button btnCancelar;
    }
}