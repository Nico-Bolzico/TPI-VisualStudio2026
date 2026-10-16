namespace WinForms
{
    partial class FormInscripcionEdit
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
            lblAlumno = new Label();
            cmbAlumno = new ComboBox();
            lblCondicion = new Label();
            cmbCondicion = new ComboBox();
            chkConNota = new CheckBox();
            nudNota = new NumericUpDown();
            btnAceptar = new Button();
            btnCancelar = new Button();
            ((System.ComponentModel.ISupportInitialize)nudNota).BeginInit();
            SuspendLayout();
            // 
            // lblAlumno
            // 
            lblAlumno.AutoSize = true;
            lblAlumno.Location = new Point(12, 12);
            lblAlumno.Name = "lblAlumno";
            lblAlumno.Size = new Size(62, 20);
            lblAlumno.TabIndex = 0;
            lblAlumno.Text = "Alumno";
            // 
            // cmbAlumno
            // 
            cmbAlumno.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbAlumno.FormattingEnabled = true;
            cmbAlumno.Location = new Point(12, 34);
            cmbAlumno.Name = "cmbAlumno";
            cmbAlumno.Size = new Size(396, 28);
            cmbAlumno.TabIndex = 1;
            // 
            // lblCondicion
            // 
            lblCondicion.AutoSize = true;
            lblCondicion.Location = new Point(12, 72);
            lblCondicion.Name = "lblCondicion";
            lblCondicion.Size = new Size(77, 20);
            lblCondicion.TabIndex = 2;
            lblCondicion.Text = "Condición";
            // 
            // cmbCondicion
            // 
            cmbCondicion.FormattingEnabled = true;
            cmbCondicion.Items.AddRange(new object[] { "Inscripto", "Regular", "Aprobado", "Libre" });
            cmbCondicion.Location = new Point(12, 94);
            cmbCondicion.Name = "cmbCondicion";
            cmbCondicion.Size = new Size(200, 28);
            cmbCondicion.TabIndex = 3;
            // 
            // chkConNota
            // 
            chkConNota.AutoSize = true;
            chkConNota.Location = new Point(12, 142);
            chkConNota.Name = "chkConNota";
            chkConNota.Size = new Size(131, 24);
            chkConNota.TabIndex = 4;
            chkConNota.Text = "Registrar nota";
            chkConNota.UseVisualStyleBackColor = true;
            chkConNota.CheckedChanged += chkConNota_CheckedChanged;
            // 
            // nudNota
            // 
            nudNota.Enabled = false;
            nudNota.Location = new Point(160, 140);
            nudNota.Maximum = new decimal(new int[] { 10, 0, 0, 0 });
            nudNota.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            nudNota.Name = "nudNota";
            nudNota.Size = new Size(80, 27);
            nudNota.TabIndex = 5;
            nudNota.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // btnAceptar
            // 
            btnAceptar.DialogResult = DialogResult.OK;
            btnAceptar.Location = new Point(12, 192);
            btnAceptar.Name = "btnAceptar";
            btnAceptar.Size = new Size(94, 29);
            btnAceptar.TabIndex = 6;
            btnAceptar.Text = "Aceptar";
            btnAceptar.UseVisualStyleBackColor = true;
            // 
            // btnCancelar
            // 
            btnCancelar.DialogResult = DialogResult.Cancel;
            btnCancelar.Location = new Point(112, 192);
            btnCancelar.Name = "btnCancelar";
            btnCancelar.Size = new Size(94, 29);
            btnCancelar.TabIndex = 7;
            btnCancelar.Text = "Cancelar";
            btnCancelar.UseVisualStyleBackColor = true;
            // 
            // FormInscripcionEdit
            // 
            AcceptButton = btnAceptar;
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            CancelButton = btnCancelar;
            ClientSize = new Size(420, 240);
            Controls.Add(btnCancelar);
            Controls.Add(btnAceptar);
            Controls.Add(nudNota);
            Controls.Add(chkConNota);
            Controls.Add(cmbCondicion);
            Controls.Add(lblCondicion);
            Controls.Add(cmbAlumno);
            Controls.Add(lblAlumno);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "FormInscripcionEdit";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Inscripción";
            FormClosing += FormInscripcionEdit_FormClosing;
            ((System.ComponentModel.ISupportInitialize)nudNota).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblAlumno;
        private ComboBox cmbAlumno;
        private Label lblCondicion;
        private ComboBox cmbCondicion;
        private CheckBox chkConNota;
        private NumericUpDown nudNota;
        private Button btnAceptar;
        private Button btnCancelar;
    }
}
