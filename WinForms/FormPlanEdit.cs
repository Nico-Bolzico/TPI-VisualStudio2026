using System.ComponentModel;
using DTOs;

namespace WinForms;

// Formulario Maestro/Detalle: datos del Plan (maestro) + grilla de Materias (detalle).
// El detalle se edita en memoria; recién se envía a la API cuando FormPlanes recibe DialogResult.OK.
public partial class FormPlanEdit : Form
{
    public PlanDTO Plan { get; private set; }

    private readonly BindingList<MateriaDTO> materias = new();

    public FormPlanEdit()
    {
        InitializeComponent();
        Plan = new PlanDTO();
        this.Text = "Nuevo plan";
        ConfigurarGrilla();
    }

    public FormPlanEdit(PlanDTO planExistente)
    {
        InitializeComponent();
        Plan = planExistente;
        this.Text = "Modificar plan";

        txtDescripcion.Text = planExistente.Descripcion;
        nudIdEspecialidad.Value = planExistente.IdEspecialidad;

        // Se trabaja sobre copias: si el usuario cancela, el plan original queda intacto.
        foreach (var m in planExistente.Materias)
        {
            materias.Add(new MateriaDTO
            {
                Id = m.Id,
                Descripcion = m.Descripcion,
                HsSemanales = m.HsSemanales,
                HsTotales = m.HsTotales,
                IdPlan = m.IdPlan
            });
        }

        ConfigurarGrilla();
    }

    private void ConfigurarGrilla()
    {
        dgvMaterias.AutoGenerateColumns = true;
        dgvMaterias.DataSource = materias;

        if (dgvMaterias.Columns["Id"] is { } colId) colId.Visible = false;
        if (dgvMaterias.Columns["IdPlan"] is { } colPlan) colPlan.Visible = false;
        if (dgvMaterias.Columns["Descripcion"] is { } colDesc)
        {
            colDesc.HeaderText = "Descripción";
            colDesc.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        }
        if (dgvMaterias.Columns["HsSemanales"] is { } colSem) colSem.HeaderText = "Hs. semanales";
        if (dgvMaterias.Columns["HsTotales"] is { } colTot) colTot.HeaderText = "Hs. totales";
    }

    private void btnAgregarMateria_Click(object sender, EventArgs e)
    {
        using var formMateria = new FormMateriaEdit();
        formMateria.OcultarPlan();

        if (formMateria.ShowDialog(this) == DialogResult.OK)
        {
            materias.Add(formMateria.Materia);
        }
    }

    private void btnEditarMateria_Click(object sender, EventArgs e)
    {
        if (dgvMaterias.CurrentRow?.DataBoundItem is not MateriaDTO materia)
        {
            MessageBox.Show("Debe seleccionar una materia de la grilla.",
                "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        using var formMateria = new FormMateriaEdit(materia);
        formMateria.OcultarPlan();

        if (formMateria.ShowDialog(this) == DialogResult.OK)
        {
            // FormMateriaEdit modificó la misma instancia: se refresca la grilla.
            materias.ResetBindings();
        }
    }

    private void btnQuitarMateria_Click(object sender, EventArgs e)
    {
        if (dgvMaterias.CurrentRow?.DataBoundItem is not MateriaDTO materia)
        {
            MessageBox.Show("Debe seleccionar una materia de la grilla.",
                "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        var confirmacion = MessageBox.Show(
            $"¿Quitar la materia \"{materia.Descripcion}\" del plan?",
            "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

        if (confirmacion == DialogResult.Yes)
        {
            materias.Remove(materia);
        }
    }

    private void FormPlanEdit_FormClosing(object sender, FormClosingEventArgs e)
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

        if (nudIdEspecialidad.Value <= 0)
        {
            MessageBox.Show("El Id de especialidad debe ser mayor que 0.",
                "Dato inválido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            e.Cancel = true;
            return;
        }

        var duplicada = materias
            .GroupBy(m => m.Descripcion.Trim(), StringComparer.OrdinalIgnoreCase)
            .FirstOrDefault(g => g.Count() > 1);

        if (duplicada is not null)
        {
            MessageBox.Show($"La materia \"{duplicada.Key}\" está repetida en el plan.",
                "Dato inválido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            e.Cancel = true;
            return;
        }

        Plan.Descripcion = txtDescripcion.Text.Trim();
        Plan.IdEspecialidad = (int)nudIdEspecialidad.Value;
        Plan.Materias = materias.ToList();
    }
}
