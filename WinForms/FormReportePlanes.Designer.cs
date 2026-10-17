namespace WinForms
{
    partial class FormReportePlanes
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
            lblPlan = new Label();
            cmbPlanes = new ComboBox();
            btnExportarPdf = new Button();
            dgvMaterias = new DataGridView();
            panelTotales = new Panel();
            lblTotalHsTotales = new Label();
            lblTotalHsSemanales = new Label();
            lblTotalMaterias = new Label();
            ((System.ComponentModel.ISupportInitialize)dgvMaterias).BeginInit();
            panelTotales.SuspendLayout();
            SuspendLayout();
            // 
            // lblPlan
            // 
            lblPlan.AutoSize = true;
            lblPlan.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblPlan.Location = new Point(14, 18);
            lblPlan.Name = "lblPlan";
            lblPlan.Size = new Size(198, 20);
            lblPlan.TabIndex = 0;
            lblPlan.Text = "Seleccionar Plan de Estudio:";
            // 
            // cmbPlanes
            // 
            cmbPlanes.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbPlanes.FormattingEnabled = true;
            cmbPlanes.Location = new Point(218, 15);
            cmbPlanes.Name = "cmbPlanes";
            cmbPlanes.Size = new Size(420, 28);
            cmbPlanes.TabIndex = 1;
            cmbPlanes.SelectedIndexChanged += cmbPlanes_SelectedIndexChanged;
            // 
            // btnExportarPdf
            // 
            btnExportarPdf.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnExportarPdf.BackColor = Color.SteelBlue;
            btnExportarPdf.FlatStyle = FlatStyle.Flat;
            btnExportarPdf.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnExportarPdf.ForeColor = Color.White;
            btnExportarPdf.Location = new Point(660, 12);
            btnExportarPdf.Name = "btnExportarPdf";
            btnExportarPdf.Size = new Size(150, 34);
            btnExportarPdf.TabIndex = 2;
            btnExportarPdf.Text = "📄 Exportar a PDF";
            btnExportarPdf.UseVisualStyleBackColor = false;
            btnExportarPdf.Click += btnExportarPdf_Click;
            // 
            // dgvMaterias
            // 
            dgvMaterias.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dgvMaterias.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvMaterias.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvMaterias.Location = new Point(14, 58);
            dgvMaterias.MultiSelect = false;
            dgvMaterias.Name = "dgvMaterias";
            dgvMaterias.ReadOnly = true;
            dgvMaterias.RowHeadersVisible = false;
            dgvMaterias.RowHeadersWidth = 51;
            dgvMaterias.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvMaterias.Size = new Size(796, 360);
            dgvMaterias.TabIndex = 3;
            // 
            // panelTotales
            // 
            panelTotales.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            panelTotales.BackColor = Color.FromArgb(240, 244, 248);
            panelTotales.BorderStyle = BorderStyle.FixedSingle;
            panelTotales.Controls.Add(lblTotalHsTotales);
            panelTotales.Controls.Add(lblTotalHsSemanales);
            panelTotales.Controls.Add(lblTotalMaterias);
            panelTotales.Location = new Point(14, 430);
            panelTotales.Name = "panelTotales";
            panelTotales.Size = new Size(796, 50);
            panelTotales.TabIndex = 4;
            // 
            // lblTotalHsTotales
            // 
            lblTotalHsTotales.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lblTotalHsTotales.AutoSize = true;
            lblTotalHsTotales.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblTotalHsTotales.ForeColor = Color.FromArgb(30, 60, 90);
            lblTotalHsTotales.Location = new Point(500, 13);
            lblTotalHsTotales.Name = "lblTotalHsTotales";
            lblTotalHsTotales.Size = new Size(220, 23);
            lblTotalHsTotales.TabIndex = 2;
            lblTotalHsTotales.Text = "Carga Horaria Total: 0 hs";
            // 
            // lblTotalHsSemanales
            // 
            lblTotalHsSemanales.AutoSize = true;
            lblTotalHsSemanales.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblTotalHsSemanales.ForeColor = Color.FromArgb(30, 60, 90);
            lblTotalHsSemanales.Location = new Point(230, 13);
            lblTotalHsSemanales.Name = "lblTotalHsSemanales";
            lblTotalHsSemanales.Size = new Size(200, 23);
            lblTotalHsSemanales.TabIndex = 1;
            lblTotalHsSemanales.Text = "Horas Semanales: 0 hs";
            // 
            // lblTotalMaterias
            // 
            lblTotalMaterias.AutoSize = true;
            lblTotalMaterias.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblTotalMaterias.ForeColor = Color.FromArgb(30, 60, 90);
            lblTotalMaterias.Location = new Point(15, 13);
            lblTotalMaterias.Name = "lblTotalMaterias";
            lblTotalMaterias.Size = new Size(150, 23);
            lblTotalMaterias.TabIndex = 0;
            lblTotalMaterias.Text = "Total Materias: 0";
            // 
            // FormReportePlanes
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(824, 494);
            Controls.Add(panelTotales);
            Controls.Add(dgvMaterias);
            Controls.Add(btnExportarPdf);
            Controls.Add(cmbPlanes);
            Controls.Add(lblPlan);
            MinimumSize = new Size(700, 450);
            Name = "FormReportePlanes";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Reporte de Planes de Estudio y Carga Horaria";
            Load += FormReportePlanes_Load;
            ((System.ComponentModel.ISupportInitialize)dgvMaterias).EndInit();
            panelTotales.ResumeLayout(false);
            panelTotales.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblPlan;
        private ComboBox cmbPlanes;
        private Button btnExportarPdf;
        private DataGridView dgvMaterias;
        private Panel panelTotales;
        private Label lblTotalMaterias;
        private Label lblTotalHsSemanales;
        private Label lblTotalHsTotales;
    }
}
