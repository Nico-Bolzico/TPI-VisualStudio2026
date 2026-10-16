namespace WinForms
{
    partial class FormCursoEdit
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
            lblMateria = new Label();
            cmbMateria = new ComboBox();
            lblComision = new Label();
            cmbComision = new ComboBox();
            lblAnio = new Label();
            nudAnio = new NumericUpDown();
            lblCupo = new Label();
            nudCupo = new NumericUpDown();
            btnAceptar = new Button();
            btnCancelar = new Button();
            ((System.ComponentModel.ISupportInitialize)nudAnio).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudCupo).BeginInit();
            SuspendLayout();
            // 
            // lblMateria
            // 
            lblMateria.AutoSize = true;
            lblMateria.Location = new Point(12, 12);
            lblMateria.Name = "lblMateria";
            lblMateria.Size = new Size(61, 20);
            lblMateria.TabIndex = 0;
            lblMateria.Text = "Materia";
            // 
            // cmbMateria
            // 
            cmbMateria.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbMateria.FormattingEnabled = true;
            cmbMateria.Location = new Point(12, 34);
            cmbMateria.Name = "cmbMateria";
            cmbMateria.Size = new Size(376, 28);
            cmbMateria.TabIndex = 1;
            // 
            // lblComision
            // 
            lblComision.AutoSize = true;
            lblComision.Location = new Point(12, 72);
            lblComision.Name = "lblComision";
            lblComision.Size = new Size(73, 20);
            lblComision.TabIndex = 2;
            lblComision.Text = "Comisión";
            // 
            // cmbComision
            // 
            cmbComision.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbComision.FormattingEnabled = true;
            cmbComision.Location = new Point(12, 94);
            cmbComision.Name = "cmbComision";
            cmbComision.Size = new Size(376, 28);
            cmbComision.TabIndex = 3;
            // 
            // lblAnio
            // 
            lblAnio.AutoSize = true;
            lblAnio.Location = new Point(12, 132);
            lblAnio.Name = "lblAnio";
            lblAnio.Size = new Size(116, 20);
            lblAnio.TabIndex = 4;
            lblAnio.Text = "Año calendario";
            // 
            // nudAnio
            // 
            nudAnio.Location = new Point(12, 154);
            nudAnio.Maximum = new decimal(new int[] { 2100, 0, 0, 0 });
            nudAnio.Minimum = new decimal(new int[] { 2000, 0, 0, 0 });
            nudAnio.Name = "nudAnio";
            nudAnio.Size = new Size(120, 27);
            nudAnio.TabIndex = 5;
            nudAnio.Value = new decimal(new int[] { 2026, 0, 0, 0 });
            // 
            // lblCupo
            // 
            lblCupo.AutoSize = true;
            lblCupo.Location = new Point(200, 132);
            lblCupo.Name = "lblCupo";
            lblCupo.Size = new Size(45, 20);
            lblCupo.TabIndex = 6;
            lblCupo.Text = "Cupo";
            // 
            // nudCupo
            // 
            nudCupo.Location = new Point(200, 154);
            nudCupo.Maximum = new decimal(new int[] { 1000, 0, 0, 0 });
            nudCupo.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            nudCupo.Name = "nudCupo";
            nudCupo.Size = new Size(120, 27);
            nudCupo.TabIndex = 7;
            nudCupo.Value = new decimal(new int[] { 30, 0, 0, 0 });
            // 
            // btnAceptar
            // 
            btnAceptar.DialogResult = DialogResult.OK;
            btnAceptar.Location = new Point(12, 205);
            btnAceptar.Name = "btnAceptar";
            btnAceptar.Size = new Size(94, 29);
            btnAceptar.TabIndex = 8;
            btnAceptar.Text = "Aceptar";
            btnAceptar.UseVisualStyleBackColor = true;
            // 
            // btnCancelar
            // 
            btnCancelar.DialogResult = DialogResult.Cancel;
            btnCancelar.Location = new Point(112, 205);
            btnCancelar.Name = "btnCancelar";
            btnCancelar.Size = new Size(94, 29);
            btnCancelar.TabIndex = 9;
            btnCancelar.Text = "Cancelar";
            btnCancelar.UseVisualStyleBackColor = true;
            // 
            // FormCursoEdit
            // 
            AcceptButton = btnAceptar;
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            CancelButton = btnCancelar;
            ClientSize = new Size(400, 250);
            Controls.Add(btnCancelar);
            Controls.Add(btnAceptar);
            Controls.Add(nudCupo);
            Controls.Add(lblCupo);
            Controls.Add(nudAnio);
            Controls.Add(lblAnio);
            Controls.Add(cmbComision);
            Controls.Add(lblComision);
            Controls.Add(cmbMateria);
            Controls.Add(lblMateria);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "FormCursoEdit";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Curso";
            FormClosing += FormCursoEdit_FormClosing;
            ((System.ComponentModel.ISupportInitialize)nudAnio).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudCupo).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblMateria;
        private ComboBox cmbMateria;
        private Label lblComision;
        private ComboBox cmbComision;
        private Label lblAnio;
        private NumericUpDown nudAnio;
        private Label lblCupo;
        private NumericUpDown nudCupo;
        private Button btnAceptar;
        private Button btnCancelar;
    }
}
