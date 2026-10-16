namespace WinForms
{
    partial class FormDocenteCursoEdit
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
            lblDocente = new Label();
            cmbDocente = new ComboBox();
            lblCargo = new Label();
            cmbCargo = new ComboBox();
            btnAceptar = new Button();
            btnCancelar = new Button();
            SuspendLayout();
            // 
            // lblDocente
            // 
            lblDocente.AutoSize = true;
            lblDocente.Location = new Point(12, 12);
            lblDocente.Name = "lblDocente";
            lblDocente.Size = new Size(65, 20);
            lblDocente.TabIndex = 0;
            lblDocente.Text = "Docente";
            // 
            // cmbDocente
            // 
            cmbDocente.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbDocente.FormattingEnabled = true;
            cmbDocente.Location = new Point(12, 34);
            cmbDocente.Name = "cmbDocente";
            cmbDocente.Size = new Size(396, 28);
            cmbDocente.TabIndex = 1;
            // 
            // lblCargo
            // 
            lblCargo.AutoSize = true;
            lblCargo.Location = new Point(12, 72);
            lblCargo.Name = "lblCargo";
            lblCargo.Size = new Size(49, 20);
            lblCargo.TabIndex = 2;
            lblCargo.Text = "Cargo";
            // 
            // cmbCargo
            // 
            cmbCargo.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbCargo.FormattingEnabled = true;
            cmbCargo.Location = new Point(12, 94);
            cmbCargo.Name = "cmbCargo";
            cmbCargo.Size = new Size(250, 28);
            cmbCargo.TabIndex = 3;
            // 
            // btnAceptar
            // 
            btnAceptar.DialogResult = DialogResult.OK;
            btnAceptar.Location = new Point(12, 142);
            btnAceptar.Name = "btnAceptar";
            btnAceptar.Size = new Size(94, 29);
            btnAceptar.TabIndex = 4;
            btnAceptar.Text = "Aceptar";
            btnAceptar.UseVisualStyleBackColor = true;
            btnAceptar.Click += btnAceptar_Click;
            // 
            // btnCancelar
            // 
            btnCancelar.DialogResult = DialogResult.Cancel;
            btnCancelar.Location = new Point(112, 142);
            btnCancelar.Name = "btnCancelar";
            btnCancelar.Size = new Size(94, 29);
            btnCancelar.TabIndex = 5;
            btnCancelar.Text = "Cancelar";
            btnCancelar.UseVisualStyleBackColor = true;
            // 
            // FormDocenteCursoEdit
            // 
            AcceptButton = btnAceptar;
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            CancelButton = btnCancelar;
            ClientSize = new Size(420, 190);
            Controls.Add(btnCancelar);
            Controls.Add(btnAceptar);
            Controls.Add(cmbCargo);
            Controls.Add(lblCargo);
            Controls.Add(cmbDocente);
            Controls.Add(lblDocente);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "FormDocenteCursoEdit";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Docente";
            FormClosing += FormDocenteCursoEdit_FormClosing;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblDocente;
        private ComboBox cmbDocente;
        private Label lblCargo;
        private ComboBox cmbCargo;
        private Button btnAceptar;
        private Button btnCancelar;
    }
}
