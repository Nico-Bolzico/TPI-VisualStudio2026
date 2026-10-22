using System.ComponentModel;
using System.Net.Http.Json;
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

    private async void FormPlanEdit_Load(object sender, EventArgs e)
    {
        try
        {
            var especialidades = await ApiSession.HttpClient.GetFromJsonAsync<List<EspecialidadDTO>>("especialidades") ?? new();
            cmbEspecialidades.DataSource = especialidades;
            cmbEspecialidades.DisplayMember = nameof(EspecialidadDTO.Descripcion);
            cmbEspecialidades.ValueMember = nameof(EspecialidadDTO.Id);

            if (Plan.IdEspecialidad > 0)
            {
                cmbEspecialidades.SelectedValue = Plan.IdEspecialidad;
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error al cargar especialidades: {ex.Message}",
                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
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

        if (cmbEspecialidades.SelectedValue is not int idEspecialidad || idEspecialidad <= 0)
        {
            MessageBox.Show("Debe seleccionar una especialidad.",
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
        Plan.IdEspecialidad = idEspecialidad;
        Plan.Materias = materias.ToList();
    }
}
