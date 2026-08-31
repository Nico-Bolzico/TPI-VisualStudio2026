using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DTOs;

namespace WinForms;

public partial class FormMateriaEdit : Form
{
    public MateriaDTO Materia { get; private set; }

    public FormMateriaEdit()
    {
        InitializeComponent();
        Materia = new MateriaDTO();
        this.Text = "Nueva materia";
    }

    public FormMateriaEdit(MateriaDTO materiaExistente)
    {
        InitializeComponent();
        Materia = materiaExistente;
        this.Text = "Modificar materia";

        txtDescripcion.Text = materiaExistente.Descripcion;
        nudHsSemanales.Value = materiaExistente.HsSemanales;
        nudHsTotales.Value = materiaExistente.HsTotales;
        nudIdPlan.Value = materiaExistente.IdPlan;
    }

    private void FormMateriaEdit_FormClosing(object sender, FormClosingEventArgs e)
    {
        if (this.DialogResult != DialogResult.OK)
            return;

        if (string.IsNullOrWhiteSpace(txtDescripcion.Text))
        {
            MessageBox.Show("La descripción es obligatoria.",
                "Datos incompletos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            e.Cancel = true;
            return;
        }

        if (nudHsTotales.Value < nudHsSemanales.Value)
        {
            MessageBox.Show("Las horas totales no pueden ser menores que las horas semanales.",
                "Dato inválido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            e.Cancel = true;
            return;
        }

        Materia.Descripcion = txtDescripcion.Text.Trim();
        Materia.HsSemanales = (int)nudHsSemanales.Value;
        Materia.HsTotales = (int)nudHsTotales.Value;
        Materia.IdPlan = (int)nudIdPlan.Value;
    }
}
