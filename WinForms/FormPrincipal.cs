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
        this.Text = $"TPI Academia - Conectado como: {ApiSession.UsuarioActual?.NombreUsuario}";
        personasToolStripMenuItem.Visible = ApiSession.TienePermiso("Personas", "Consultar");
        materiasToolStripMenuItem.Visible = ApiSession.TienePermiso("Materias", "Consultar");
        planesToolStripMenuItem.Visible = ApiSession.TienePermiso("Planes", "Consultar");
        comisionesToolStripMenuItem.Visible = ApiSession.TienePermiso("Comisiones", "Consultar");
        cursosToolStripMenuItem.Visible = ApiSession.TienePermiso("Cursos", "Consultar");

        bool puedeVerCursos = ApiSession.TienePermiso("Cursos", "Consultar");
        bool puedeVerPlanes = ApiSession.TienePermiso("Planes", "Consultar");

        reporteCursosPorCupoToolStripMenuItem.Visible = puedeVerCursos;
        reportePlanesDeEstudioToolStripMenuItem.Visible = puedeVerPlanes;
        reportesToolStripMenuItem.Visible = puedeVerCursos || puedeVerPlanes;
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

    private void planesToolStripMenuItem_Click(object sender, EventArgs e)
    {
        using var formPlanes = new FormPlanes();
        formPlanes.ShowDialog(this);
    }

    private void comisionesToolStripMenuItem_Click(object sender, EventArgs e)
    {
        using var formComisiones = new FormComisiones();
        formComisiones.ShowDialog(this);
    }

    private void cursosToolStripMenuItem_Click(object sender, EventArgs e)
    {
        using var formCursos = new FormCursos();
        formCursos.ShowDialog(this);
    }

    private void reporteCursosPorCupoToolStripMenuItem_Click(object sender, EventArgs e)
    {
        using var reporte = new GraficoInscriptosContraCupo();
        reporte.ShowDialog(this);
    }

    private void reportePlanesDeEstudioToolStripMenuItem_Click(object sender, EventArgs e)
    {
        using var reporte = new FormReportePlanes();
        reporte.ShowDialog(this);
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
}
