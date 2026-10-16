namespace WinForms
{
    partial class FormCursos
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
            dgvCursos = new DataGridView();
            btnAlta = new Button();
            btnModificar = new Button();
            btnBaja = new Button();
            btnRefrescar = new Button();
            lblCursoSeleccionado = new Label();
            tabDetalle = new TabControl();
            tabInscripciones = new TabPage();
            dgvInscripciones = new DataGridView();
            btnInscribir = new Button();
            btnModificarInscripcion = new Button();
            btnQuitarInscripcion = new Button();
            tabDocentes = new TabPage();
            dgvDocentes = new DataGridView();
            btnAsignarDocente = new Button();
            btnModificarDocente = new Button();
            btnQuitarDocente = new Button();
            ((System.ComponentModel.ISupportInitialize)dgvCursos).BeginInit();
            tabDetalle.SuspendLayout();
            tabInscripciones.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvInscripciones).BeginInit();
            tabDocentes.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvDocentes).BeginInit();
            SuspendLayout();
            // 
            // dgvCursos
            // 
            dgvCursos.AllowUserToAddRows = false;
            dgvCursos.AllowUserToDeleteRows = false;
            dgvCursos.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            dgvCursos.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvCursos.Location = new Point(12, 12);
            dgvCursos.MultiSelect = false;
            dgvCursos.Name = "dgvCursos";
            dgvCursos.ReadOnly = true;
            dgvCursos.RowHeadersVisible = false;
            dgvCursos.RowHeadersWidth = 51;
            dgvCursos.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvCursos.Size = new Size(960, 200);
            dgvCursos.TabIndex = 0;
            dgvCursos.SelectionChanged += dgvCursos_SelectionChanged;
            // 
            // btnAlta
            // 
            btnAlta.Location = new Point(12, 218);
            btnAlta.Name = "btnAlta";
            btnAlta.Size = new Size(94, 29);
            btnAlta.TabIndex = 1;
            btnAlta.Text = "Alta";
            btnAlta.UseVisualStyleBackColor = true;
            btnAlta.Click += btnAlta_Click;
            // 
            // btnModificar
            // 
            btnModificar.Location = new Point(112, 218);
            btnModificar.Name = "btnModificar";
            btnModificar.Size = new Size(94, 29);
            btnModificar.TabIndex = 2;
            btnModificar.Text = "Modificar";
            btnModificar.UseVisualStyleBackColor = true;
            btnModificar.Click += btnModificar_Click;
            // 
            // btnBaja
            // 
            btnBaja.Location = new Point(212, 218);
            btnBaja.Name = "btnBaja";
            btnBaja.Size = new Size(94, 29);
            btnBaja.TabIndex = 3;
            btnBaja.Text = "Baja";
            btnBaja.UseVisualStyleBackColor = true;
            btnBaja.Click += btnBaja_Click;
            // 
            // btnRefrescar
            // 
            btnRefrescar.Location = new Point(312, 218);
            btnRefrescar.Name = "btnRefrescar";
            btnRefrescar.Size = new Size(94, 29);
            btnRefrescar.TabIndex = 4;
            btnRefrescar.Text = "Refrescar";
            btnRefrescar.UseVisualStyleBackColor = true;
            btnRefrescar.Click += btnRefrescar_Click;
            // 
            // lblCursoSeleccionado
            // 
            lblCursoSeleccionado.AutoSize = true;
            lblCursoSeleccionado.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblCursoSeleccionado.Location = new Point(12, 258);
            lblCursoSeleccionado.Name = "lblCursoSeleccionado";
            lblCursoSeleccionado.Size = new Size(146, 20);
            lblCursoSeleccionado.TabIndex = 5;
            lblCursoSeleccionado.Text = "Seleccione un curso";
            // 
            // tabDetalle
            // 
            tabDetalle.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            tabDetalle.Controls.Add(tabInscripciones);
            tabDetalle.Controls.Add(tabDocentes);
            tabDetalle.Location = new Point(12, 282);
            tabDetalle.Name = "tabDetalle";
            tabDetalle.SelectedIndex = 0;
            tabDetalle.Size = new Size(960, 307);
            tabDetalle.TabIndex = 6;
            // 
            // tabInscripciones
            // 
            tabInscripciones.Controls.Add(btnQuitarInscripcion);
            tabInscripciones.Controls.Add(btnModificarInscripcion);
            tabInscripciones.Controls.Add(btnInscribir);
            tabInscripciones.Controls.Add(dgvInscripciones);
            tabInscripciones.Location = new Point(4, 29);
            tabInscripciones.Name = "tabInscripciones";
            tabInscripciones.Padding = new Padding(3);
            tabInscripciones.Size = new Size(952, 274);
            tabInscripciones.TabIndex = 0;
            tabInscripciones.Text = "Alumnos inscriptos";
            tabInscripciones.UseVisualStyleBackColor = true;
            // 
            // dgvInscripciones
            // 
            dgvInscripciones.AllowUserToAddRows = false;
            dgvInscripciones.AllowUserToDeleteRows = false;
            dgvInscripciones.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dgvInscripciones.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvInscripciones.Location = new Point(6, 6);
            dgvInscripciones.MultiSelect = false;
            dgvInscripciones.Name = "dgvInscripciones";
            dgvInscripciones.ReadOnly = true;
            dgvInscripciones.RowHeadersVisible = false;
            dgvInscripciones.RowHeadersWidth = 51;
            dgvInscripciones.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvInscripciones.Size = new Size(940, 228);
            dgvInscripciones.TabIndex = 0;
            // 
            // btnInscribir
            // 
            btnInscribir.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnInscribir.Location = new Point(6, 240);
            btnInscribir.Name = "btnInscribir";
            btnInscribir.Size = new Size(94, 29);
            btnInscribir.TabIndex = 1;
            btnInscribir.Text = "Inscribir";
            btnInscribir.UseVisualStyleBackColor = true;
            btnInscribir.Click += btnInscribir_Click;
            // 
            // btnModificarInscripcion
            // 
            btnModificarInscripcion.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnModificarInscripcion.Location = new Point(106, 240);
            btnModificarInscripcion.Name = "btnModificarInscripcion";
            btnModificarInscripcion.Size = new Size(94, 29);
            btnModificarInscripcion.TabIndex = 2;
            btnModificarInscripcion.Text = "Modificar";
            btnModificarInscripcion.UseVisualStyleBackColor = true;
            btnModificarInscripcion.Click += btnModificarInscripcion_Click;
            // 
            // btnQuitarInscripcion
            // 
            btnQuitarInscripcion.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnQuitarInscripcion.Location = new Point(206, 240);
            btnQuitarInscripcion.Name = "btnQuitarInscripcion";
            btnQuitarInscripcion.Size = new Size(94, 29);
            btnQuitarInscripcion.TabIndex = 3;
            btnQuitarInscripcion.Text = "Quitar";
            btnQuitarInscripcion.UseVisualStyleBackColor = true;
            btnQuitarInscripcion.Click += btnQuitarInscripcion_Click;
            // 
            // tabDocentes
            // 
            tabDocentes.Controls.Add(btnQuitarDocente);
            tabDocentes.Controls.Add(btnModificarDocente);
            tabDocentes.Controls.Add(btnAsignarDocente);
            tabDocentes.Controls.Add(dgvDocentes);
            tabDocentes.Location = new Point(4, 29);
            tabDocentes.Name = "tabDocentes";
            tabDocentes.Padding = new Padding(3);
            tabDocentes.Size = new Size(952, 274);
            tabDocentes.TabIndex = 1;
            tabDocentes.Text = "Docentes";
            tabDocentes.UseVisualStyleBackColor = true;
            // 
            // dgvDocentes
            // 
            dgvDocentes.AllowUserToAddRows = false;
            dgvDocentes.AllowUserToDeleteRows = false;
            dgvDocentes.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dgvDocentes.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvDocentes.Location = new Point(6, 6);
            dgvDocentes.MultiSelect = false;
            dgvDocentes.Name = "dgvDocentes";
            dgvDocentes.ReadOnly = true;
            dgvDocentes.RowHeadersVisible = false;
            dgvDocentes.RowHeadersWidth = 51;
            dgvDocentes.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvDocentes.Size = new Size(940, 228);
            dgvDocentes.TabIndex = 0;
            // 
            // btnAsignarDocente
            // 
            btnAsignarDocente.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnAsignarDocente.Location = new Point(6, 240);
            btnAsignarDocente.Name = "btnAsignarDocente";
            btnAsignarDocente.Size = new Size(94, 29);
            btnAsignarDocente.TabIndex = 1;
            btnAsignarDocente.Text = "Asignar";
            btnAsignarDocente.UseVisualStyleBackColor = true;
            btnAsignarDocente.Click += btnAsignarDocente_Click;
            // 
            // btnModificarDocente
            // 
            btnModificarDocente.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnModificarDocente.Location = new Point(106, 240);
            btnModificarDocente.Name = "btnModificarDocente";
            btnModificarDocente.Size = new Size(94, 29);
            btnModificarDocente.TabIndex = 2;
            btnModificarDocente.Text = "Modificar";
            btnModificarDocente.UseVisualStyleBackColor = true;
            btnModificarDocente.Click += btnModificarDocente_Click;
            // 
            // btnQuitarDocente
            // 
            btnQuitarDocente.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnQuitarDocente.Location = new Point(206, 240);
            btnQuitarDocente.Name = "btnQuitarDocente";
            btnQuitarDocente.Size = new Size(94, 29);
            btnQuitarDocente.TabIndex = 3;
            btnQuitarDocente.Text = "Quitar";
            btnQuitarDocente.UseVisualStyleBackColor = true;
            btnQuitarDocente.Click += btnQuitarDocente_Click;
            // 
            // FormCursos
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(984, 601);
            Controls.Add(tabDetalle);
            Controls.Add(lblCursoSeleccionado);
            Controls.Add(btnRefrescar);
            Controls.Add(btnBaja);
            Controls.Add(btnModificar);
            Controls.Add(btnAlta);
            Controls.Add(dgvCursos);
            MinimumSize = new Size(800, 560);
            Name = "FormCursos";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Cursos";
            Load += FormCursos_Load;
            ((System.ComponentModel.ISupportInitialize)dgvCursos).EndInit();
            tabDetalle.ResumeLayout(false);
            tabInscripciones.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvInscripciones).EndInit();
            tabDocentes.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvDocentes).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView dgvCursos;
        private Button btnAlta;
        private Button btnModificar;
        private Button btnBaja;
        private Button btnRefrescar;
        private Label lblCursoSeleccionado;
        private TabControl tabDetalle;
        private TabPage tabInscripciones;
        private DataGridView dgvInscripciones;
        private Button btnInscribir;
        private Button btnModificarInscripcion;
        private Button btnQuitarInscripcion;
        private TabPage tabDocentes;
        private DataGridView dgvDocentes;
        private Button btnAsignarDocente;
        private Button btnModificarDocente;
        private Button btnQuitarDocente;
    }
}
