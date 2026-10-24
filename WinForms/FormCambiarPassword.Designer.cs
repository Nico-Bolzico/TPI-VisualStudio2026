namespace WinForms
{
    partial class FormCambiarPassword
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
            lblActual = new Label();
            txtActual = new TextBox();
            lblNueva = new Label();
            txtNueva = new TextBox();
            lblConfirmar = new Label();
            txtConfirmar = new TextBox();
            btnAceptar = new Button();
            btnCancelar = new Button();
            SuspendLayout();
            // 
            // lblActual
            // 
            lblActual.AutoSize = true;
            lblActual.Location = new Point(12, 9);
            lblActual.Name = "lblActual";
            lblActual.Size = new Size(125, 20);
            lblActual.TabIndex = 0;
            lblActual.Text = "Contraseña actual";
            // 
            // txtActual
            // 
            txtActual.Location = new Point(12, 32);
            txtActual.MaxLength = 100;
            txtActual.Name = "txtActual";
            txtActual.PasswordChar = '*';
            txtActual.Size = new Size(316, 27);
            txtActual.TabIndex = 0;
            // 
            // lblNueva
            // 
            lblNueva.AutoSize = true;
            lblNueva.Location = new Point(12, 75);
            lblNueva.Name = "lblNueva";
            lblNueva.Size = new Size(260, 20);
            lblNueva.TabIndex = 1;
            lblNueva.Text = "Contraseña nueva (mín. 8 caracteres)";
            // 
            // txtNueva
            // 
            txtNueva.Location = new Point(12, 98);
            txtNueva.MaxLength = 100;
            txtNueva.Name = "txtNueva";
            txtNueva.PasswordChar = '*';
            txtNueva.Size = new Size(316, 27);
            txtNueva.TabIndex = 1;
            // 
            // lblConfirmar
            // 
            lblConfirmar.AutoSize = true;
            lblConfirmar.Location = new Point(12, 141);
            lblConfirmar.Name = "lblConfirmar";
            lblConfirmar.Size = new Size(185, 20);
            lblConfirmar.TabIndex = 2;
            lblConfirmar.Text = "Repetir contraseña nueva";
            // 
            // txtConfirmar
            // 
            txtConfirmar.Location = new Point(12, 164);
            txtConfirmar.MaxLength = 100;
            txtConfirmar.Name = "txtConfirmar";
            txtConfirmar.PasswordChar = '*';
            txtConfirmar.Size = new Size(316, 27);
            txtConfirmar.TabIndex = 2;
            // 
            // btnAceptar
            // 
            btnAceptar.DialogResult = DialogResult.OK;
            btnAceptar.Location = new Point(132, 215);
            btnAceptar.Name = "btnAceptar";
            btnAceptar.Size = new Size(94, 30);
            btnAceptar.TabIndex = 3;
            btnAceptar.Text = "Aceptar";
            btnAceptar.UseVisualStyleBackColor = true;
            // 
            // btnCancelar
            // 
            btnCancelar.DialogResult = DialogResult.Cancel;
            btnCancelar.Location = new Point(234, 215);
            btnCancelar.Name = "btnCancelar";
            btnCancelar.Size = new Size(94, 30);
            btnCancelar.TabIndex = 4;
            btnCancelar.Text = "Cancelar";
            btnCancelar.UseVisualStyleBackColor = true;
            // 
            // FormCambiarPassword
            // 
            AcceptButton = btnAceptar;
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            CancelButton = btnCancelar;
            ClientSize = new Size(340, 262);
            Controls.Add(btnCancelar);
            Controls.Add(btnAceptar);
            Controls.Add(txtConfirmar);
            Controls.Add(lblConfirmar);
            Controls.Add(txtNueva);
            Controls.Add(lblNueva);
            Controls.Add(txtActual);
            Controls.Add(lblActual);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "FormCambiarPassword";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Cambiar contraseña";
            FormClosing += FormCambiarPassword_FormClosing;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblActual;
        private TextBox txtActual;
        private Label lblNueva;
        private TextBox txtNueva;
        private Label lblConfirmar;
        private TextBox txtConfirmar;
        private Button btnAceptar;
        private Button btnCancelar;
    }
}
