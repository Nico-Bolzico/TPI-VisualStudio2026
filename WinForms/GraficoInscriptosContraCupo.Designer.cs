namespace WinForms
{
    partial class GraficoInscriptosContraCupo
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            splitContainer1 = new SplitContainer();
            DgvReporte = new DataGridView();
            chart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.Panel2.SuspendLayout();
            splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)DgvReporte).BeginInit();
            ((System.ComponentModel.ISupportInitialize)chart).BeginInit();
            SuspendLayout();
            // 
            // splitContainer1
            // 
            splitContainer1.Dock = DockStyle.Fill;
            splitContainer1.Location = new Point(0, 0);
            splitContainer1.Name = "splitContainer1";
            splitContainer1.Orientation = Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            splitContainer1.Panel1.Controls.Add(DgvReporte);
            // 
            // splitContainer1.Panel2
            // 
            splitContainer1.Panel2.Controls.Add(chart);
            splitContainer1.Size = new Size(810, 579);
            splitContainer1.SplitterDistance = 274;
            splitContainer1.TabIndex = 0;
            // 
            // DgvReporte
            // 
            DgvReporte.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            DgvReporte.Location = new Point(12, 12);
            DgvReporte.Name = "DgvReporte";
            DgvReporte.RowHeadersWidth = 51;
            DgvReporte.Size = new Size(786, 247);
            DgvReporte.TabIndex = 0;
            // 
            // chart
            // 
            chartArea1.Name = "ChartArea1";
            chart.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            chart.Legends.Add(legend1);
            chart.Location = new Point(12, 13);
            chart.Name = "chart";
            series1.ChartArea = "ChartArea1";
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            chart.Series.Add(series1);
            chart.Size = new Size(786, 276);
            chart.TabIndex = 0;
            chart.Text = "chart1";
            // 
            // GraficoInscriptosContraCupo
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(810, 579);
            Controls.Add(splitContainer1);
            Name = "GraficoInscriptosContraCupo";
            Text = "Form1";
            splitContainer1.Panel1.ResumeLayout(false);
            splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
            splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)DgvReporte).EndInit();
            ((System.ComponentModel.ISupportInitialize)chart).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private SplitContainer splitContainer1;
        private DataGridView DgvReporte;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart;
    }
}