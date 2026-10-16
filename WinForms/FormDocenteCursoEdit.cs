using DTOs;

namespace WinForms;

public partial class FormDocenteCursoEdit : Form
{
    private sealed record OpcionDocente(int Id, string Texto);

    public DocenteCursoDTO Asignacion { get; private set; }

    private readonly bool modoEdicion;

    
    public FormDocenteCursoEdit(IEnumerable<PersonaDTO> profesores, int idCurso)
    {
        InitializeComponent();
        modoEdicion = false;
        Text = "Asignar docente";
        Asignacion = new DocenteCursoDTO { IdCurso = idCurso };

        var opciones = profesores
            .OrderBy(p => p.Apellido).ThenBy(p => p.Nombre)
            .Select(p => new OpcionDocente(p.Id, $"{p.Apellido}, {p.Nombre} (Legajo {p.Legajo})"))
            .ToList();

        CargarDocentes(opciones);
        cmbDocente.SelectedIndex = -1;
        CargarCargos(null);
        cmbCargo.SelectedIndex = -1;
    }

    
    public FormDocenteCursoEdit(DocenteCursoDTO existente, string nombreDocente)
    {
        InitializeComponent();
        modoEdicion = true;
        Text = "Modificar cargo";
        Asignacion = new DocenteCursoDTO
        {
            Id = existente.Id,
            IdCurso = existente.IdCurso,
            IdDocente = existente.IdDocente,
            Cargo = existente.Cargo
        };

        CargarDocentes(new List<OpcionDocente> { new(existente.IdDocente, nombreDocente) });
        cmbDocente.Enabled = false;

        CargarCargos(existente.Cargo);
        cmbCargo.SelectedValue = existente.Cargo;
    }

    private void CargarDocentes(List<OpcionDocente> opciones)
    {
        cmbDocente.DisplayMember = nameof(OpcionDocente.Texto);
        cmbDocente.ValueMember = nameof(OpcionDocente.Id);
        cmbDocente.DataSource = opciones;
    }

    private void CargarCargos(int? cargoActual)
    {
        var cargos = CargoDocenteDTO.Todos.ToList();

        if (cargoActual.HasValue && !cargos.Any(c => c.Key == cargoActual.Value))
            cargos.Add(new KeyValuePair<int, string>(cargoActual.Value, CargoDocenteDTO.Nombre(cargoActual.Value)));

        cmbCargo.DisplayMember = "Value";
        cmbCargo.ValueMember = "Key";
        cmbCargo.DataSource = cargos;
    }

    private void FormDocenteCursoEdit_FormClosing(object sender, FormClosingEventArgs e)
    {
        if (this.DialogResult != DialogResult.OK)
            return;

        int idDocente = Asignacion.IdDocente;

        if (!modoEdicion)
        {
            if (cmbDocente.SelectedValue is not int seleccionado)
            {
                MessageBox.Show("Debe seleccionar un docente.",
                    "Datos incompletos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                e.Cancel = true;
                return;
            }

            idDocente = seleccionado;
        }

        if (cmbCargo.SelectedValue is not int cargo)
        {
            MessageBox.Show("Debe seleccionar un cargo.",
                "Datos incompletos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            e.Cancel = true;
            return;
        }

        Asignacion.IdDocente = idDocente;
        Asignacion.Cargo = cargo;
    }

    private void btnAceptar_Click(object sender, EventArgs e)
    {

    }
}
