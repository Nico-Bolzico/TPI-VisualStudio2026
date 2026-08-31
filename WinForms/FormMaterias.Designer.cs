namespace WinForms
{
    partial class FormMaterias
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
            btnBaja = new Button();
            Modificar = new Button();
            btnAlta = new Button();
            btnMostrarTodos = new Button();
            txtBusqueda = new TextBox();
            btnBuscar = new Button();
            dgvMaterias = new DataGridView();
            ((System.ComponentModel.ISupportInitialize)dgvMaterias).BeginInit();
            SuspendLayout();
            // 
            // btnBaja
            // 
            btnBaja.Location = new Point(212, 383);
            btnBaja.Name = "btnBaja";
            btnBaja.Size = new Size(94, 29);
            btnBaja.TabIndex = 13;
            btnBaja.Text = "Baja";
            btnBaja.UseVisualStyleBackColor = true;
            btnBaja.Click += btnBaja_Click;
            // 
            // Modificar
            // 
            Modificar.Location = new Point(112, 383);
            Modificar.Name = "Modificar";
            Modificar.Size = new Size(94, 29);
            Modificar.TabIndex = 12;
            Modificar.Text = "Modificar";
            Modificar.UseVisualStyleBackColor = true;
            Modificar.Click += btnModificar_Click;
            // 
            // btnAlta
            // 
            btnAlta.Location = new Point(12, 383);
            btnAlta.Name = "btnAlta";
            btnAlta.Size = new Size(94, 29);
            btnAlta.TabIndex = 11;
            btnAlta.Text = "Alta";
            btnAlta.UseVisualStyleBackColor = true;
            btnAlta.Click += btnAlta_Click;
            // 
            // btnMostrarTodos
            // 
            btnMostrarTodos.Location = new Point(662, 40);
            btnMostrarTodos.Name = "btnMostrarTodos";
            btnMostrarTodos.Size = new Size(126, 29);
            btnMostrarTodos.TabIndex = 10;
            btnMostrarTodos.Text = "Mostrar Todos";
            btnMostrarTodos.UseVisualStyleBackColor = true;
            btnMostrarTodos.Click += btnMostrarTodos_Click;
            // 
            // txtBusqueda
            // 
            txtBusqueda.Location = new Point(12, 40);
            txtBusqueda.Name = "txtBusqueda";
            txtBusqueda.Size = new Size(174, 27);
            txtBusqueda.TabIndex = 9;
            // 
            // btnBuscar
            // 
            btnBuscar.Location = new Point(192, 38);
            btnBuscar.Name = "btnBuscar";
            btnBuscar.Size = new Size(94, 29);
            btnBuscar.TabIndex = 8;
            btnBuscar.Text = "Buscar";
            btnBuscar.UseVisualStyleBackColor = true;
            btnBuscar.Click += btnBuscar_Click;
            // 
            // dgvMaterias
            // 
            dgvMaterias.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvMaterias.Location = new Point(12, 85);
            dgvMaterias.MultiSelect = false;
            dgvMaterias.Name = "dgvMaterias";
            dgvMaterias.RowHeadersWidth = 51;
            dgvMaterias.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvMaterias.Size = new Size(776, 292);
            dgvMaterias.TabIndex = 7;
            // 
            // FormMaterias
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(btnBaja);
            Controls.Add(Modificar);
            Controls.Add(btnAlta);
            Controls.Add(btnMostrarTodos);
            Controls.Add(txtBusqueda);
            Controls.Add(btnBuscar);
            Controls.Add(dgvMaterias);
            Name = "FormMaterias";
            Text = "FormMaterias";
            Load += FormMaterias_Load;
            ((System.ComponentModel.ISupportInitialize)dgvMaterias).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnBaja;
        private Button Modificar;
        private Button btnAlta;
        private Button btnMostrarTodos;
        private TextBox txtBusqueda;
        private Button btnBuscar;
        private DataGridView dgvMaterias;
    }
}