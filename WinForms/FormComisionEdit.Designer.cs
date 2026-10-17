namespace WinForms
{
    partial class FormComisionEdit
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            lblDescripcion = new Label();
            txtDescripcion = new TextBox();
            lblAnioEspecialidad = new Label();
            nudAnioEspecialidad = new NumericUpDown();
            lblPlan = new Label();
            cmbPlanes = new ComboBox();
            btnGuardar = new Button();
            btnCancelar = new Button();
            ((System.ComponentModel.ISupportInitialize)nudAnioEspecialidad).BeginInit();
            SuspendLayout();
            // 
            // lblDescripcion
            // 
            lblDescripcion.AutoSize = true;
            lblDescripcion.Location = new Point(30, 25);
            lblDescripcion.Name = "lblDescripcion";
            lblDescripcion.Size = new Size(157, 20);
            lblDescripcion.TabIndex = 0;
            lblDescripcion.Text = "Comisión (Ej: 1K1):";
            // 
            // txtDescripcion
            // 
            txtDescripcion.Location = new Point(30, 50);
            txtDescripcion.MaxLength = 50;
            txtDescripcion.Name = "txtDescripcion";
            txtDescripcion.Size = new Size(360, 27);
            txtDescripcion.TabIndex = 1;
            // 
            // lblAnioEspecialidad
            // 
            lblAnioEspecialidad.AutoSize = true;
            lblAnioEspecialidad.Location = new Point(30, 95);
            lblAnioEspecialidad.Name = "lblAnioEspecialidad";
            lblAnioEspecialidad.Size = new Size(181, 20);
            lblAnioEspecialidad.TabIndex = 2;
            lblAnioEspecialidad.Text = "Año de Especialidad (1 a 6):";
            // 
            // nudAnioEspecialidad
            // 
            nudAnioEspecialidad.Location = new Point(30, 120);
            nudAnioEspecialidad.Maximum = new decimal(new int[] { 6, 0, 0, 0 });
            nudAnioEspecialidad.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            nudAnioEspecialidad.Name = "nudAnioEspecialidad";
            nudAnioEspecialidad.Size = new Size(120, 27);
            nudAnioEspecialidad.TabIndex = 3;
            nudAnioEspecialidad.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // lblPlan
            // 
            lblPlan.AutoSize = true;
            lblPlan.Location = new Point(30, 165);
            lblPlan.Name = "lblPlan";
            lblPlan.Size = new Size(116, 20);
            lblPlan.TabIndex = 4;
            lblPlan.Text = "Plan de Estudio:";
            // 
            // cmbPlanes
            // 
            cmbPlanes.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbPlanes.FormattingEnabled = true;
            cmbPlanes.Location = new Point(30, 190);
            cmbPlanes.Name = "cmbPlanes";
            cmbPlanes.Size = new Size(360, 28);
            cmbPlanes.TabIndex = 5;
            // 
            // btnGuardar
            // 
            btnGuardar.Location = new Point(190, 250);
            btnGuardar.Name = "btnGuardar";
            btnGuardar.Size = new Size(95, 32);
            btnGuardar.TabIndex = 6;
            btnGuardar.Text = "Guardar";
            btnGuardar.UseVisualStyleBackColor = true;
            btnGuardar.Click += btnGuardar_Click;
            // 
            // btnCancelar
            // 
            btnCancelar.Location = new Point(295, 250);
            btnCancelar.Name = "btnCancelar";
            btnCancelar.Size = new Size(95, 32);
            btnCancelar.TabIndex = 7;
            btnCancelar.Text = "Cancelar";
            btnCancelar.UseVisualStyleBackColor = true;
            btnCancelar.Click += btnCancelar_Click;
            // 
            // FormComisionEdit
            // 
            AcceptButton = btnGuardar;
            CancelButton = btnCancelar;
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(420, 310);
            Controls.Add(btnCancelar);
            Controls.Add(btnGuardar);
            Controls.Add(cmbPlanes);
            Controls.Add(lblPlan);
            Controls.Add(nudAnioEspecialidad);
            Controls.Add(lblAnioEspecialidad);
            Controls.Add(txtDescripcion);
            Controls.Add(lblDescripcion);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "FormComisionEdit";
            StartPosition = FormStartPosition.CenterParent;
            Text = "FormComisionEdit";
            ((System.ComponentModel.ISupportInitialize)nudAnioEspecialidad).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblDescripcion;
        private TextBox txtDescripcion;
        private Label lblAnioEspecialidad;
        private NumericUpDown nudAnioEspecialidad;
        private Label lblPlan;
        private ComboBox cmbPlanes;
        private Button btnGuardar;
        private Button btnCancelar;
    }
}
