namespace WinForms
{
    partial class FormPrincipal
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
            menuStrip1 = new MenuStrip();
            gestionToolStripMenuItem = new ToolStripMenuItem();
            personasToolStripMenuItem = new ToolStripMenuItem();
            materiasToolStripMenuItem = new ToolStripMenuItem();
            sesionToolStripMenuItem = new ToolStripMenuItem();
            cerrarSesionToolStripMenuItem = new ToolStripMenuItem();
            salirToolStripMenuItem = new ToolStripMenuItem();
            menuStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // menuStrip1
            // 
            menuStrip1.ImageScalingSize = new Size(20, 20);
            menuStrip1.Items.AddRange(new ToolStripItem[] { gestionToolStripMenuItem, sesionToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(800, 28);
            menuStrip1.TabIndex = 0;
            menuStrip1.Text = "menuStrip1";
            // 
            // gestionToolStripMenuItem
            // 
            gestionToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { personasToolStripMenuItem, materiasToolStripMenuItem });
            gestionToolStripMenuItem.Name = "gestionToolStripMenuItem";
            gestionToolStripMenuItem.Size = new Size(73, 24);
            gestionToolStripMenuItem.Text = "Gestión";
            // 
            // personasToolStripMenuItem
            // 
            personasToolStripMenuItem.Name = "personasToolStripMenuItem";
            personasToolStripMenuItem.Size = new Size(224, 26);
            personasToolStripMenuItem.Text = "Personas";
            personasToolStripMenuItem.Click += personasToolStripMenuItem_Click;
            // 
            // materiasToolStripMenuItem
            // 
            materiasToolStripMenuItem.Name = "materiasToolStripMenuItem";
            materiasToolStripMenuItem.Size = new Size(224, 26);
            materiasToolStripMenuItem.Text = "Materias";
            materiasToolStripMenuItem.Click += materiasToolStripMenuItem_Click;
            // 
            // sesionToolStripMenuItem
            // 
            sesionToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { cerrarSesionToolStripMenuItem, salirToolStripMenuItem });
            sesionToolStripMenuItem.Name = "sesionToolStripMenuItem";
            sesionToolStripMenuItem.Size = new Size(66, 24);
            sesionToolStripMenuItem.Text = "Sesión";
            // 
            // cerrarSesionToolStripMenuItem
            // 
            cerrarSesionToolStripMenuItem.Name = "cerrarSesionToolStripMenuItem";
            cerrarSesionToolStripMenuItem.Size = new Size(179, 26);
            cerrarSesionToolStripMenuItem.Text = "Cerrar Sesión";
            cerrarSesionToolStripMenuItem.Click += cerrarSesionToolStripMenuItem_Click;
            // 
            // salirToolStripMenuItem
            // 
            salirToolStripMenuItem.Name = "salirToolStripMenuItem";
            salirToolStripMenuItem.Size = new Size(179, 26);
            salirToolStripMenuItem.Text = "Salir";
            salirToolStripMenuItem.Click += salirToolStripMenuItem_Click;
            // 
            // FormPrincipal
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(menuStrip1);
            MainMenuStrip = menuStrip1;
            Name = "FormPrincipal";
            Text = "Form1";
            Load += FormPrincipal_Load;
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private MenuStrip menuStrip1;
        private ToolStripMenuItem gestionToolStripMenuItem;
        private ToolStripMenuItem personasToolStripMenuItem;
        private ToolStripMenuItem materiasToolStripMenuItem;
        private ToolStripMenuItem sesionToolStripMenuItem;
        private ToolStripMenuItem cerrarSesionToolStripMenuItem;
        private ToolStripMenuItem salirToolStripMenuItem;
    }
}