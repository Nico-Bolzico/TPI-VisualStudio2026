namespace WinForms
{
    partial class FormPlanes
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
            txtBusqueda = new TextBox();
            btnBuscar = new Button();
            btnMostrarTodos = new Button();
            lblPlanes = new Label();
            dgvPlanes = new DataGridView();
            lblDetalle = new Label();
            dgvDetalle = new DataGridView();
            btnAlta = new Button();
            btnModificar = new Button();
            btnBaja = new Button();
            ((System.ComponentModel.ISupportInitialize)dgvPlanes).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvDetalle).BeginInit();
            SuspendLayout();
            // 
            // txtBusqueda
            // 
            txtBusqueda.Location = new Point(12, 12);
            txtBusqueda.Name = "txtBusqueda";
            txtBusqueda.Size = new Size(174, 27);
            txtBusqueda.TabIndex = 0;
            // 
            // btnBuscar
            // 
            btnBuscar.Location = new Point(192, 10);
            btnBuscar.Name = "btnBuscar";
            btnBuscar.Size = new Size(94, 29);
            btnBuscar.TabIndex = 1;
            btnBuscar.Text = "Buscar";
            btnBuscar.UseVisualStyleBackColor = true;
            btnBuscar.Click += btnBuscar_Click;
            // 
            // btnMostrarTodos
            // 
            btnMostrarTodos.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnMostrarTodos.Location = new Point(662, 10);
            btnMostrarTodos.Name = "btnMostrarTodos";
            btnMostrarTodos.Size = new Size(126, 29);
            btnMostrarTodos.TabIndex = 2;
            btnMostrarTodos.Text = "Mostrar Todos";
            btnMostrarTodos.UseVisualStyleBackColor = true;
            btnMostrarTodos.Click += btnMostrarTodos_Click;
            // 
            // lblPlanes
            // 
            lblPlanes.AutoSize = true;
            lblPlanes.Location = new Point(12, 50);
            lblPlanes.Name = "lblPlanes";
            lblPlanes.Size = new Size(52, 20);
            lblPlanes.TabIndex = 3;
            lblPlanes.Text = "Planes";
            // 
            // dgvPlanes
            // 
            dgvPlanes.AllowUserToAddRows = false;
            dgvPlanes.AllowUserToDeleteRows = false;
            dgvPlanes.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            dgvPlanes.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvPlanes.Location = new Point(12, 73);
            dgvPlanes.MultiSelect = false;
            dgvPlanes.Name = "dgvPlanes";
            dgvPlanes.ReadOnly = true;
            dgvPlanes.RowHeadersWidth = 51;
            dgvPlanes.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvPlanes.Size = new Size(776, 170);
            dgvPlanes.TabIndex = 4;
            dgvPlanes.SelectionChanged += dgvPlanes_SelectionChanged;
            // 
            // lblDetalle
            // 
            lblDetalle.AutoSize = true;
            lblDetalle.Location = new Point(12, 255);
            lblDetalle.Name = "lblDetalle";
            lblDetalle.Size = new Size(202, 20);
            lblDetalle.TabIndex = 5;
            lblDetalle.Text = "Materias del plan seleccionado";
            // 
            // dgvDetalle
            // 
            dgvDetalle.AllowUserToAddRows = false;
            dgvDetalle.AllowUserToDeleteRows = false;
            dgvDetalle.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dgvDetalle.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvDetalle.Location = new Point(12, 278);
            dgvDetalle.MultiSelect = false;
            dgvDetalle.Name = "dgvDetalle";
            dgvDetalle.ReadOnly = true;
            dgvDetalle.RowHeadersWidth = 51;
            dgvDetalle.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvDetalle.Size = new Size(776, 110);
            dgvDetalle.TabIndex = 6;
            // 
            // btnAlta
            // 
            btnAlta.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnAlta.Location = new Point(12, 400);
            btnAlta.Name = "btnAlta";
            btnAlta.Size = new Size(94, 29);
            btnAlta.TabIndex = 7;
            btnAlta.Text = "Alta";
            btnAlta.UseVisualStyleBackColor = true;
            btnAlta.Click += btnAlta_Click;
            // 
            // btnModificar
            // 
            btnModificar.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnModificar.Location = new Point(112, 400);
            btnModificar.Name = "btnModificar";
            btnModificar.Size = new Size(94, 29);
            btnModificar.TabIndex = 8;
            btnModificar.Text = "Modificar";
            btnModificar.UseVisualStyleBackColor = true;
            btnModificar.Click += btnModificar_Click;
            // 
            // btnBaja
            // 
            btnBaja.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnBaja.Location = new Point(212, 400);
            btnBaja.Name = "btnBaja";
            btnBaja.Size = new Size(94, 29);
            btnBaja.TabIndex = 9;
            btnBaja.Text = "Baja";
            btnBaja.UseVisualStyleBackColor = true;
            btnBaja.Click += btnBaja_Click;
            // 
            // FormPlanes
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 441);
            Controls.Add(btnBaja);
            Controls.Add(btnModificar);
            Controls.Add(btnAlta);
            Controls.Add(dgvDetalle);
            Controls.Add(lblDetalle);
            Controls.Add(dgvPlanes);
            Controls.Add(lblPlanes);
            Controls.Add(btnMostrarTodos);
            Controls.Add(btnBuscar);
            Controls.Add(txtBusqueda);
            MinimumSize = new Size(818, 488);
            Name = "FormPlanes";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Planes";
            Load += FormPlanes_Load;
            ((System.ComponentModel.ISupportInitialize)dgvPlanes).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvDetalle).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox txtBusqueda;
        private Button btnBuscar;
        private Button btnMostrarTodos;
        private Label lblPlanes;
        private DataGridView dgvPlanes;
        private Label lblDetalle;
        private DataGridView dgvDetalle;
        private Button btnAlta;
        private Button btnModificar;
        private Button btnBaja;
    }
}
