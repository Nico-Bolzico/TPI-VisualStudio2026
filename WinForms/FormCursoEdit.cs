using DTOs;

namespace WinForms;

public partial class FormCursoEdit : Form
{
    // Siempre es una copia: el CursoDTO original de la grilla no se modifica
    // hasta que la API confirma el cambio.
    public CursoDTO Curso { get; private set; }

    public FormCursoEdit(IEnumerable<MateriaDTO> materias, IEnumerable<ComisionDTO> comisiones,
        CursoDTO? existente = null)
    {
        InitializeComponent();

        cmbMateria.DisplayMember = nameof(MateriaDTO.Descripcion);
        cmbMateria.ValueMember = nameof(MateriaDTO.Id);
        cmbMateria.DataSource = materias.OrderBy(m => m.Descripcion).ToList();

        cmbComision.DisplayMember = nameof(ComisionDTO.Descripcion);
        cmbComision.ValueMember = nameof(ComisionDTO.Id);
        cmbComision.DataSource = comisiones.OrderBy(c => c.Descripcion).ToList();

        if (existente is null)
        {
            Curso = new CursoDTO();
            Text = "Nuevo curso";

            cmbMateria.SelectedIndex = -1;
            cmbComision.SelectedIndex = -1;
            nudAnio.Value = Math.Clamp(DateTime.Now.Year, (int)nudAnio.Minimum, (int)nudAnio.Maximum);
            nudCupo.Value = 30;
        }
        else
        {
            Curso = new CursoDTO
            {
                Id = existente.Id,
                IdMateria = existente.IdMateria,
                IdComision = existente.IdComision,
                AnioCalendario = existente.AnioCalendario,
                Cupo = existente.Cupo
            };
            Text = "Modificar curso";

            cmbMateria.SelectedValue = existente.IdMateria;
            cmbComision.SelectedValue = existente.IdComision;
            nudAnio.Value = Math.Clamp(existente.AnioCalendario, (int)nudAnio.Minimum, (int)nudAnio.Maximum);
            nudCupo.Value = Math.Clamp(existente.Cupo, (int)nudCupo.Minimum, (int)nudCupo.Maximum);
        }
    }

    private void FormCursoEdit_FormClosing(object sender, FormClosingEventArgs e)
    {
        if (this.DialogResult != DialogResult.OK)
            return;

        if (cmbMateria.SelectedValue is not int idMateria)
        {
            MessageBox.Show("Debe seleccionar una materia.",
                "Datos incompletos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            e.Cancel = true;
            return;
        }

        if (cmbComision.SelectedValue is not int idComision)
        {
            MessageBox.Show("Debe seleccionar una comisión.",
                "Datos incompletos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            e.Cancel = true;
            return;
        }

        Curso.IdMateria = idMateria;
        Curso.IdComision = idComision;
        Curso.AnioCalendario = (int)nudAnio.Value;
        Curso.Cupo = (int)nudCupo.Value;
    }
}
