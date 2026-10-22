namespace WinForms
{
    partial class FormPlanEdit
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
            lblIdEspecialidad = new Label();
            cmbEspecialidades = new ComboBox();
            grpMaterias = new GroupBox();
            btnQuitarMateria = new Button();
            btnEditarMateria = new Button();
            btnAgregarMateria = new Button();
            dgvMaterias = new DataGridView();
            btnAceptar = new Button();
            btnCancelar = new Button();
            grpMaterias.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvMaterias).BeginInit();
            SuspendLayout();
            // 
            // lblDescripcion
            // 
            lblDescripcion.AutoSize = true;
            lblDescripcion.Location = new Point(12, 15);
            lblDescripcion.Name = "lblDescripcion";
            lblDescripcion.Size = new Size(87, 20);
            lblDescripcion.TabIndex = 0;
            lblDescripcion.Text = "Descripción";
            // 
            // txtDescripcion
            // 
            txtDescripcion.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtDescripcion.Location = new Point(12, 38);
            txtDescripcion.MaxLength = 100;
            txtDescripcion.Name = "txtDescripcion";
            txtDescripcion.Size = new Size(450, 27);
            txtDescripcion.TabIndex = 1;
            // 
            // lblIdEspecialidad
            // 
            lblIdEspecialidad.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lblIdEspecialidad.AutoSize = true;
            lblIdEspecialidad.Location = new Point(480, 15);
            lblIdEspecialidad.Name = "lblIdEspecialidad";
            lblIdEspecialidad.Size = new Size(92, 20);
            lblIdEspecialidad.TabIndex = 2;
            lblIdEspecialidad.Text = "Especialidad";
            // 
            // cmbEspecialidades
            // 
            cmbEspecialidades.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            cmbEspecialidades.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbEspecialidades.FormattingEnabled = true;
            cmbEspecialidades.Location = new Point(480, 38);
            cmbEspecialidades.Name = "cmbEspecialidades";
            cmbEspecialidades.Size = new Size(260, 28);
            cmbEspecialidades.TabIndex = 3;
            // 
            // grpMaterias
            // 
            grpMaterias.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            grpMaterias.Controls.Add(btnQuitarMateria);
            grpMaterias.Controls.Add(btnEditarMateria);
            grpMaterias.Controls.Add(btnAgregarMateria);
            grpMaterias.Controls.Add(dgvMaterias);
            grpMaterias.Location = new Point(12, 80);
            grpMaterias.Name = "grpMaterias";
            grpMaterias.Size = new Size(728, 330);
            grpMaterias.TabIndex = 4;
            grpMaterias.TabStop = false;
            grpMaterias.Text = "Materias del plan (detalle)";
            // 
            // btnQuitarMateria
            // 
            btnQuitarMateria.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnQuitarMateria.Location = new Point(206, 290);
            btnQuitarMateria.Name = "btnQuitarMateria";
            btnQuitarMateria.Size = new Size(94, 29);
            btnQuitarMateria.TabIndex = 3;
            btnQuitarMateria.Text = "Quitar";
            btnQuitarMateria.UseVisualStyleBackColor = true;
            btnQuitarMateria.Click += btnQuitarMateria_Click;
            // 
            // btnEditarMateria
            // 
            btnEditarMateria.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnEditarMateria.Location = new Point(106, 290);
            btnEditarMateria.Name = "btnEditarMateria";
            btnEditarMateria.Size = new Size(94, 29);
            btnEditarMateria.TabIndex = 2;
            btnEditarMateria.Text = "Editar";
            btnEditarMateria.UseVisualStyleBackColor = true;
            btnEditarMateria.Click += btnEditarMateria_Click;
            // 
            // btnAgregarMateria
            // 
            btnAgregarMateria.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnAgregarMateria.Location = new Point(6, 290);
            btnAgregarMateria.Name = "btnAgregarMateria";
            btnAgregarMateria.Size = new Size(94, 29);
            btnAgregarMateria.TabIndex = 1;
            btnAgregarMateria.Text = "Agregar";
            btnAgregarMateria.UseVisualStyleBackColor = true;
            btnAgregarMateria.Click += btnAgregarMateria_Click;
            // 
            // dgvMaterias
            // 
            dgvMaterias.AllowUserToAddRows = false;
            dgvMaterias.AllowUserToDeleteRows = false;
            dgvMaterias.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dgvMaterias.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvMaterias.Location = new Point(6, 26);
            dgvMaterias.MultiSelect = false;
            dgvMaterias.Name = "dgvMaterias";
            dgvMaterias.ReadOnly = true;
            dgvMaterias.RowHeadersWidth = 51;
            dgvMaterias.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvMaterias.Size = new Size(716, 255);
            dgvMaterias.TabIndex = 0;
            // 
            // btnAceptar
            // 
            btnAceptar.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnAceptar.DialogResult = DialogResult.OK;
            btnAceptar.Location = new Point(12, 420);
            btnAceptar.Name = "btnAceptar";
            btnAceptar.Size = new Size(94, 29);
            btnAceptar.TabIndex = 5;
            btnAceptar.Text = "Aceptar";
            btnAceptar.UseVisualStyleBackColor = true;
            // 
            // btnCancelar
            // 
            btnCancelar.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnCancelar.DialogResult = DialogResult.Cancel;
            btnCancelar.Location = new Point(112, 420);
            btnCancelar.Name = "btnCancelar";
            btnCancelar.Size = new Size(94, 29);
            btnCancelar.TabIndex = 6;
            btnCancelar.Text = "Cancelar";
            btnCancelar.UseVisualStyleBackColor = true;
            // 
            // FormPlanEdit
            // 
            AcceptButton = btnAceptar;
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            CancelButton = btnCancelar;
            ClientSize = new Size(752, 461);
            Controls.Add(btnCancelar);
            Controls.Add(btnAceptar);
            Controls.Add(grpMaterias);
            Controls.Add(cmbEspecialidades);
            Controls.Add(lblIdEspecialidad);
            Controls.Add(txtDescripcion);
            Controls.Add(lblDescripcion);
            MinimumSize = new Size(770, 508);
            Name = "FormPlanEdit";
            StartPosition = FormStartPosition.CenterParent;
            Text = "FormPlanEdit";
            Load += FormPlanEdit_Load;
            FormClosing += FormPlanEdit_FormClosing;
            grpMaterias.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvMaterias).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblDescripcion;
        private TextBox txtDescripcion;
        private Label lblIdEspecialidad;
        private ComboBox cmbEspecialidades;
        private GroupBox grpMaterias;
        private Button btnQuitarMateria;
        private Button btnEditarMateria;
        private Button btnAgregarMateria;
        private DataGridView dgvMaterias;
        private Button btnAceptar;
        private Button btnCancelar;
    }
}
