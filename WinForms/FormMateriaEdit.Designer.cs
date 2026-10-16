namespace WinForms
{
    partial class FormMateriaEdit
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
            btnCancelar = new Button();
            btnAceptar = new Button();
            label1 = new Label();
            txtDescripcion = new TextBox();
            label2 = new Label();
            nudHsSemanales = new NumericUpDown();
            label3 = new Label();
            nudHsTotales = new NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)nudHsSemanales).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudHsTotales).BeginInit();
            SuspendLayout();
            // 
            // btnCancelar
            // 
            btnCancelar.DialogResult = DialogResult.Cancel;
            btnCancelar.Location = new Point(121, 152);
            btnCancelar.Name = "btnCancelar";
            btnCancelar.Size = new Size(94, 29);
            btnCancelar.TabIndex = 39;
            btnCancelar.Text = "Cancelar";
            btnCancelar.UseVisualStyleBackColor = true;
            // 
            // btnAceptar
            // 
            btnAceptar.DialogResult = DialogResult.OK;
            btnAceptar.Location = new Point(12, 151);
            btnAceptar.Name = "btnAceptar";
            btnAceptar.Size = new Size(94, 29);
            btnAceptar.TabIndex = 38;
            btnAceptar.Text = "Aceptar";
            btnAceptar.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 19);
            label1.Name = "label1";
            label1.Size = new Size(87, 20);
            label1.TabIndex = 25;
            label1.Text = "Descripcion";
            // 
            // txtDescripcion
            // 
            txtDescripcion.Location = new Point(12, 40);
            txtDescripcion.Name = "txtDescripcion";
            txtDescripcion.Size = new Size(583, 27);
            txtDescripcion.TabIndex = 20;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(12, 79);
            label2.Name = "label2";
            label2.Size = new Size(136, 20);
            label2.TabIndex = 41;
            label2.Text = "Nro. Hs. Semanales";
            // 
            // nudHsSemanales
            // 
            nudHsSemanales.Location = new Point(12, 101);
            nudHsSemanales.Name = "nudHsSemanales";
            nudHsSemanales.Size = new Size(150, 27);
            nudHsSemanales.TabIndex = 40;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(218, 79);
            label3.Name = "label3";
            label3.Size = new Size(112, 20);
            label3.TabIndex = 43;
            label3.Text = "Nro. Hs. Totales";
            // 
            // nudHsTotales
            // 
            nudHsTotales.Location = new Point(218, 101);
            nudHsTotales.Maximum = new decimal(new int[] { 10000, 0, 0, 0 });
            nudHsTotales.Name = "nudHsTotales";
            nudHsTotales.Size = new Size(150, 27);
            nudHsTotales.TabIndex = 42;
            // 
            // FormMateriaEdit
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(label3);
            Controls.Add(nudHsTotales);
            Controls.Add(label2);
            Controls.Add(nudHsSemanales);
            Controls.Add(btnCancelar);
            Controls.Add(btnAceptar);
            Controls.Add(label1);
            Controls.Add(txtDescripcion);
            Name = "FormMateriaEdit";
            Text = "FormMateriaEdit";
            FormClosing += FormMateriaEdit_FormClosing;
            ((System.ComponentModel.ISupportInitialize)nudHsSemanales).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudHsTotales).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnCancelar;
        private Button btnAceptar;
        private Label label1;
        private TextBox txtDescripcion;
        private Label label2;
        private NumericUpDown nudHsSemanales;
        private Label label3;
        private NumericUpDown nudHsTotales;
    }
}