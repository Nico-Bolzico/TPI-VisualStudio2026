using DTOs;

namespace WinForms;

public partial class FormInscripcionEdit : Form
{
    private sealed record OpcionAlumno(int Id, string Texto);

    // Siempre es una copia: la fila original de la grilla no se toca
    // hasta que la API confirma el cambio.
    public AlumnoInscripcionDTO Inscripcion { get; private set; }

    private readonly bool modoEdicion;

    // ALTA: se elige el alumno entre los recibidos; el curso ya viene definido.
    public FormInscripcionEdit(IEnumerable<PersonaDTO> alumnos, int idCurso)
    {
        InitializeComponent();
        modoEdicion = false;
        Text = "Nueva inscripción";
        Inscripcion = new AlumnoInscripcionDTO { IdCurso = idCurso };

        var opciones = alumnos
            .OrderBy(a => a.Apellido).ThenBy(a => a.Nombre)
            .Select(a => new OpcionAlumno(a.Id, $"{a.Apellido}, {a.Nombre} (Legajo {a.Legajo})"))
            .ToList();

        CargarAlumnos(opciones);
        cmbAlumno.SelectedIndex = -1;
        cmbCondicion.Text = "Inscripto";
    }

    // MODIFICACIÓN: el alumno y el curso no cambian; solo la condición y la nota.
    public FormInscripcionEdit(AlumnoInscripcionDTO existente, string nombreAlumno)
    {
        InitializeComponent();
        modoEdicion = true;
        Text = "Modificar inscripción";
        Inscripcion = new AlumnoInscripcionDTO
        {
            Id = existente.Id,
            IdAlumno = existente.IdAlumno,
            IdCurso = existente.IdCurso,
            Condicion = existente.Condicion,
            Nota = existente.Nota
        };

        CargarAlumnos(new List<OpcionAlumno> { new(existente.IdAlumno, nombreAlumno) });
        cmbAlumno.Enabled = false;

        cmbCondicion.Text = existente.Condicion;

        if (existente.Nota.HasValue)
        {
            chkConNota.Checked = true;
            nudNota.Value = Math.Clamp(existente.Nota.Value, (int)nudNota.Minimum, (int)nudNota.Maximum);
        }
    }

    private void CargarAlumnos(List<OpcionAlumno> opciones)
    {
        cmbAlumno.DisplayMember = nameof(OpcionAlumno.Texto);
        cmbAlumno.ValueMember = nameof(OpcionAlumno.Id);
        cmbAlumno.DataSource = opciones;
    }

    private void chkConNota_CheckedChanged(object sender, EventArgs e)
    {
        nudNota.Enabled = chkConNota.Checked;
    }

    private void FormInscripcionEdit_FormClosing(object sender, FormClosingEventArgs e)
    {
        if (this.DialogResult != DialogResult.OK)
            return;

        int idAlumno = Inscripcion.IdAlumno;

        if (!modoEdicion)
        {
            if (cmbAlumno.SelectedValue is not int seleccionado)
            {
                MessageBox.Show("Debe seleccionar un alumno.",
                    "Datos incompletos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                e.Cancel = true;
                return;
            }

            idAlumno = seleccionado;
        }

        string condicion = cmbCondicion.Text.Trim();
        if (condicion.Length == 0)
        {
            MessageBox.Show("La condición es obligatoria.",
                "Datos incompletos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            e.Cancel = true;
            return;
        }

        Inscripcion.IdAlumno = idAlumno;
        Inscripcion.Condicion = condicion;
        Inscripcion.Nota = chkConNota.Checked ? (int)nudNota.Value : (int?)null;
    }
}
