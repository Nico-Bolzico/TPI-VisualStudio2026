namespace WinForms
{
    partial class FormPersonas
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
            dgvPersonas = new DataGridView();
            btnBuscar = new Button();
            txtBusqueda = new TextBox();
            btnMostrarTodos = new Button();
            btnAlta = new Button();
            Modificar = new Button();
            btnBaja = new Button();
            ((System.ComponentModel.ISupportInitialize)dgvPersonas).BeginInit();
            SuspendLayout();
            // 
            // dgvPersonas
            // 
            dgvPersonas.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvPersonas.Location = new Point(12, 70);
            dgvPersonas.MultiSelect = false;
            dgvPersonas.Name = "dgvPersonas";
            dgvPersonas.RowHeadersWidth = 51;
            dgvPersonas.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvPersonas.Size = new Size(776, 292);
            dgvPersonas.TabIndex = 0;
            // 
            // btnBuscar
            // 
            btnBuscar.Location = new Point(192, 23);
            btnBuscar.Name = "btnBuscar";
            btnBuscar.Size = new Size(94, 29);
            btnBuscar.TabIndex = 1;
            btnBuscar.Text = "Buscar";
            btnBuscar.UseVisualStyleBackColor = true;
            btnBuscar.Click += this.btnBuscar_Click;
            // 
            // txtBusqueda
            // 
            txtBusqueda.Location = new Point(12, 25);
            txtBusqueda.Name = "txtBusqueda";
            txtBusqueda.Size = new Size(174, 27);
            txtBusqueda.TabIndex = 2;
            // 
            // btnMostrarTodos
            // 
            btnMostrarTodos.Location = new Point(662, 25);
            btnMostrarTodos.Name = "btnMostrarTodos";
            btnMostrarTodos.Size = new Size(126, 29);
            btnMostrarTodos.TabIndex = 3;
            btnMostrarTodos.Text = "Mostrar Todos";
            btnMostrarTodos.UseVisualStyleBackColor = true;
            btnMostrarTodos.Click += this.btnMostrarTodos_Click;
            // 
            // btnAlta
            // 
            btnAlta.Location = new Point(12, 368);
            btnAlta.Name = "btnAlta";
            btnAlta.Size = new Size(94, 29);
            btnAlta.TabIndex = 4;
            btnAlta.Text = "Alta";
            btnAlta.UseVisualStyleBackColor = true;
            btnAlta.Click += this.btnAlta_Click;
            // 
            // Modificar
            // 
            Modificar.Location = new Point(112, 368);
            Modificar.Name = "Modificar";
            Modificar.Size = new Size(94, 29);
            Modificar.TabIndex = 5;
            Modificar.Text = "Modificar";
            Modificar.UseVisualStyleBackColor = true;
            Modificar.Click += this.btnModificar_Click;
            // 
            // btnBaja
            // 
            btnBaja.Location = new Point(212, 368);
            btnBaja.Name = "btnBaja";
            btnBaja.Size = new Size(94, 29);
            btnBaja.TabIndex = 6;
            btnBaja.Text = "Baja";
            btnBaja.UseVisualStyleBackColor = true;
            btnBaja.Click += btnBaja_Click;
            // 
            // FormPersonas
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
            Controls.Add(dgvPersonas);
            Name = "FormPersonas";
            Text = "Form1";
            Load += FormPersonas_Load;
            ((System.ComponentModel.ISupportInitialize)dgvPersonas).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView dgvPersonas;
        private Button btnBuscar;
        private TextBox txtBusqueda;
        private Button btnMostrarTodos;
        private Button btnAlta;
        private Button Modificar;
        private Button btnBaja;
    }
}