namespace WinForms
{
    partial class FormComisiones
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
            dgvComisiones = new DataGridView();
            btnBuscar = new Button();
            txtBusqueda = new TextBox();
            btnMostrarTodos = new Button();
            btnAlta = new Button();
            btnModificar = new Button();
            btnBaja = new Button();
            ((System.ComponentModel.ISupportInitialize)dgvComisiones).BeginInit();
            SuspendLayout();
            // 
            // dgvComisiones
            // 
            dgvComisiones.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dgvComisiones.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvComisiones.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvComisiones.Location = new Point(12, 70);
            dgvComisiones.MultiSelect = false;
            dgvComisiones.Name = "dgvComisiones";
            dgvComisiones.ReadOnly = true;
            dgvComisiones.RowHeadersWidth = 51;
            dgvComisiones.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvComisiones.Size = new Size(776, 310);
            dgvComisiones.TabIndex = 0;
            // 
            // txtBusqueda
            // 
            txtBusqueda.Location = new Point(12, 25);
            txtBusqueda.Name = "txtBusqueda";
            txtBusqueda.PlaceholderText = "Buscar por nombre de comisión...";
            txtBusqueda.Size = new Size(240, 27);
            txtBusqueda.TabIndex = 1;
            // 
            // btnBuscar
            // 
            btnBuscar.Location = new Point(260, 23);
            btnBuscar.Name = "btnBuscar";
            btnBuscar.Size = new Size(94, 29);
            btnBuscar.TabIndex = 2;
            btnBuscar.Text = "Buscar";
            btnBuscar.UseVisualStyleBackColor = true;
            btnBuscar.Click += btnBuscar_Click;
            // 
            // btnMostrarTodos
            // 
            btnMostrarTodos.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnMostrarTodos.Location = new Point(662, 23);
            btnMostrarTodos.Name = "btnMostrarTodos";
            btnMostrarTodos.Size = new Size(126, 29);
            btnMostrarTodos.TabIndex = 3;
            btnMostrarTodos.Text = "Mostrar Todos";
            btnMostrarTodos.UseVisualStyleBackColor = true;
            btnMostrarTodos.Click += btnMostrarTodos_Click;
            // 
            // btnAlta
            // 
            btnAlta.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnAlta.Location = new Point(12, 395);
            btnAlta.Name = "btnAlta";
            btnAlta.Size = new Size(100, 32);
            btnAlta.TabIndex = 4;
            btnAlta.Text = "Nueva";
            btnAlta.UseVisualStyleBackColor = true;
            btnAlta.Click += btnAlta_Click;
            // 
            // btnModificar
            // 
            btnModificar.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnModificar.Location = new Point(120, 395);
            btnModificar.Name = "btnModificar";
            btnModificar.Size = new Size(100, 32);
            btnModificar.TabIndex = 5;
            btnModificar.Text = "Modificar";
            btnModificar.UseVisualStyleBackColor = true;
            btnModificar.Click += btnModificar_Click;
            // 
            // btnBaja
            // 
            btnBaja.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnBaja.Location = new Point(228, 395);
            btnBaja.Name = "btnBaja";
            btnBaja.Size = new Size(100, 32);
            btnBaja.TabIndex = 6;
            btnBaja.Text = "Eliminar";
            btnBaja.UseVisualStyleBackColor = true;
            btnBaja.Click += btnBaja_Click;
            // 
            // FormComisiones
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 445);
            Controls.Add(btnBaja);
            Controls.Add(btnModificar);
            Controls.Add(btnAlta);
            Controls.Add(btnMostrarTodos);
            Controls.Add(btnBuscar);
            Controls.Add(txtBusqueda);
            Controls.Add(dgvComisiones);
            MinimumSize = new Size(600, 350);
            Name = "FormComisiones";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Gestión de Comisiones";
            Load += FormComisiones_Load;
            ((System.ComponentModel.ISupportInitialize)dgvComisiones).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView dgvComisiones;
        private TextBox txtBusqueda;
        private Button btnBuscar;
        private Button btnMostrarTodos;
        private Button btnAlta;
        private Button btnModificar;
        private Button btnBaja;
    }
}
