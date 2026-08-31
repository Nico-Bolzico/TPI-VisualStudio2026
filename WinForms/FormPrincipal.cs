using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinForms;

public partial class FormPrincipal : Form
{
    public bool DeseaCerrarSesion { get; private set; } = false;

    public FormPrincipal()
    {
        InitializeComponent();
    }

    private void FormPrincipal_Load(object sender, EventArgs e)
    {
        this.Text = $"TPI Academia - Conectado como: {ApiSession.UsuarioActual}";
    }

    private void personasToolStripMenuItem_Click(object sender, EventArgs e)
    {
        using var formPersonas = new FormPersonas();
        formPersonas.ShowDialog(this);
    }

    private void materiasToolStripMenuItem_Click(object sender, EventArgs e)
    {
        using var formMaterias = new FormMaterias();
        formMaterias.ShowDialog(this);
    }

    private void cerrarSesionToolStripMenuItem_Click(object sender, EventArgs e)
    {
        DeseaCerrarSesion = true;
        this.Close();
    }

    private void salirToolStripMenuItem_Click(object sender, EventArgs e)
    {
        DeseaCerrarSesion = false;
        this.Close();
    }

    private void personasToolStripMenuItem1_Click(object sender, EventArgs e)
    {

    }

    private void materiasToolStripMenuItem1_Click(object sender, EventArgs e)
    {

    }
}
