using System.ComponentModel;
using System.Net.Http.Json;
using DTOs;

namespace WinForms;

// CRUD Maestro/Detalle
// Cada cambio del detalle se envía de inmediato a la API, porque el servidor valida cada inscripción por separado (cupo, alumno ya inscripto).
public partial class FormCursos : Form
{
    // Filas de las grillas 
    private sealed class CursoFila
    {
        [Browsable(false)] public int Id { get; init; }
        public string Materia { get; init; } = string.Empty;
        public string Comision { get; init; } = string.Empty;
        public int Anio { get; init; }
        public int Cupo { get; init; }
        public string Inscriptos { get; init; } = "-";
        [Browsable(false)] public CursoDTO Dto { get; init; } = new();
    }

    private sealed class InscripcionFila
    {
        [Browsable(false)] public int Id { get; init; }
        public string Alumno { get; init; } = string.Empty;
        public string Condicion { get; init; } = string.Empty;
        public string Nota { get; init; } = "-";
        [Browsable(false)] public AlumnoInscripcionDTO Dto { get; init; } = new();
    }

    private sealed class DocenteFila
    {
        [Browsable(false)] public int Id { get; init; }
        public string Docente { get; init; } = string.Empty;
        public string Cargo { get; init; } = string.Empty;
        [Browsable(false)] public DocenteCursoDTO Dto { get; init; } = new();
    }

    // Datos de referencia para traducir Ids a nombres y para llenar los combos.
    private List<MateriaDTO> _materias = new();
    private List<ComisionDTO> _comisiones = new();
    private List<PersonaDTO> _personas = new();

    // Detalle del curso seleccionado (null = todavía no cargado o sin permiso de consulta).
    private List<InscripcionFila>? _inscripciones;
    private List<DocenteFila>? _docentes;

    private int? _cursoDetalleId;  
    private bool _cargandoGrilla;  

    public FormCursos()
    {
        InitializeComponent();
    }

    private async void FormCursos_Load(object sender, EventArgs e)
    {
        btnAlta.Enabled = ApiSession.TienePermiso("Cursos", "Alta");
        btnModificar.Enabled = ApiSession.TienePermiso("Cursos", "Modificar");
        btnBaja.Enabled = ApiSession.TienePermiso("Cursos", "Baja");
        ActualizarBotonesDetalle();

        try
        {
            await CargarReferenciasAsync();
            await CargarCursosAsync();
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error al cargar los cursos: {ex.Message}",
                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    //  Carga de datos

    private static async Task<List<T>> ObtenerListaAsync<T>(string url)
    {
        try
        {
            return await ApiSession.HttpClient.GetFromJsonAsync<List<T>>(url) ?? new List<T>();
        }
        catch (HttpRequestException)
        {
            return new List<T>();
        }
    }

    private async Task CargarReferenciasAsync()
    {
        var materias = ObtenerListaAsync<MateriaDTO>("materias");
        var comisiones = ObtenerListaAsync<ComisionDTO>("comisiones");
        var personas = ObtenerListaAsync<PersonaDTO>("personas");

        await Task.WhenAll(materias, comisiones, personas);

        _materias = await materias;
        _comisiones = await comisiones;
        _personas = await personas;
    }

    private string NombreMateria(int id) =>
        _materias.FirstOrDefault(m => m.Id == id)?.Descripcion ?? $"Materia N° {id}";

    private string NombreComision(int id) =>
        _comisiones.FirstOrDefault(c => c.Id == id)?.Descripcion ?? $"Comisión N° {id}";

    private string NombrePersona(int id, string rotulo)
    {
        var persona = _personas.FirstOrDefault(p => p.Id == id);
        return persona is null ? $"{rotulo} N° {id}" : $"{persona.Apellido}, {persona.Nombre} (Legajo {persona.Legajo})";
    }

    private async Task CargarCursosAsync()
    {
        int? seleccionado = CursoSeleccionado()?.Id;

        var cursos = await ApiSession.HttpClient.GetFromJsonAsync<List<CursoDTO>>("cursos") ?? new List<CursoDTO>();

        Dictionary<int, int>? inscriptosPorCurso = null;
        try
        {
            var todas = await ApiSession.HttpClient.GetFromJsonAsync<List<AlumnoInscripcionDTO>>("inscripciones") ?? new List<AlumnoInscripcionDTO>();
            inscriptosPorCurso = todas.GroupBy(i => i.IdCurso).ToDictionary(g => g.Key, g => g.Count());
        }
        catch (HttpRequestException)
        {
        }

        if (IsDisposed) return;

        var filas = cursos
            .Select(c => new CursoFila
            {
                Id = c.Id,
                Materia = NombreMateria(c.IdMateria),
                Comision = NombreComision(c.IdComision),
                Anio = c.AnioCalendario,
                Cupo = c.Cupo,
                Inscriptos = inscriptosPorCurso is null
                    ? "-"
                    : (inscriptosPorCurso.TryGetValue(c.Id, out var n) ? n : 0).ToString(),
                Dto = c
            })
            .OrderBy(f => f.Materia).ThenBy(f => f.Comision)
            .ToList();

        _cargandoGrilla = true;
        try
        {
            dgvCursos.DataSource = filas;
            Encabezado(dgvCursos, nameof(CursoFila.Materia), "Materia", llenar: true);
            Encabezado(dgvCursos, nameof(CursoFila.Comision), "Comisión");
            Encabezado(dgvCursos, nameof(CursoFila.Anio), "Año");
            Encabezado(dgvCursos, nameof(CursoFila.Cupo), "Cupo");
            Encabezado(dgvCursos, nameof(CursoFila.Inscriptos), "Inscriptos");

            // Restaura la selección anterior (si el curso sigue existiendo)
            if (seleccionado.HasValue)
            {
                foreach (DataGridViewRow fila in dgvCursos.Rows)
                {
                    if (fila.DataBoundItem is CursoFila f && f.Id == seleccionado.Value)
                    {
                        dgvCursos.CurrentCell = fila.Cells[0];
                        break;
                    }
                }
            }
        }
        finally
        {
            _cargandoGrilla = false;
        }

        await MostrarDetalleAsync(CursoSeleccionado());
    }

    private async void dgvCursos_SelectionChanged(object sender, EventArgs e)
    {
        if (_cargandoGrilla)
            return;

        try
        {
            await MostrarDetalleAsync(CursoSeleccionado());
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error al cargar el detalle del curso: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private async Task MostrarDetalleAsync(CursoFila? curso)
    {
        _cursoDetalleId = curso?.Id;
        _inscripciones = null;
        _docentes = null;
        ActualizarBotonesDetalle();

        if (curso is null)
        {
            lblCursoSeleccionado.Text = "Seleccione un curso";
            dgvInscripciones.DataSource = null;
            dgvDocentes.DataSource = null;
            return;
        }

        lblCursoSeleccionado.Text = $"Curso: {curso.Materia} - Comisión {curso.Comision} - {curso.Anio}";

        await CargarInscripcionesAsync(curso.Id);
        await CargarDocentesAsync(curso.Id);
    }

    private async Task CargarInscripcionesAsync(int idCurso)
    {
        List<AlumnoInscripcionDTO> lista;
        try
        {
            lista = await ApiSession.HttpClient.GetFromJsonAsync<List<AlumnoInscripcionDTO>>($"inscripciones/curso/{idCurso}") ?? new List<AlumnoInscripcionDTO>();
        }
        catch (HttpRequestException)
        {
            if (!IsDisposed && _cursoDetalleId == idCurso)
            {
                _inscripciones = null;
                dgvInscripciones.DataSource = null;
            }
            return;
        }

        if (IsDisposed || _cursoDetalleId != idCurso)
            return;

        _inscripciones = lista
            .Select(i => new InscripcionFila
            {
                Id = i.Id,
                Alumno = NombrePersona(i.IdAlumno, "Alumno"),
                Condicion = i.Condicion,
                Nota = i.Nota?.ToString() ?? "-",
                Dto = i
            })
            .OrderBy(f => f.Alumno)
            .ToList();

        dgvInscripciones.DataSource = _inscripciones;
        Encabezado(dgvInscripciones, nameof(InscripcionFila.Alumno), "Alumno", llenar: true);
        Encabezado(dgvInscripciones, nameof(InscripcionFila.Condicion), "Condición");
        Encabezado(dgvInscripciones, nameof(InscripcionFila.Nota), "Nota");
    }

    private async Task CargarDocentesAsync(int idCurso)
    {
        List<DocenteCursoDTO> lista;
        try
        {
            lista = await ApiSession.HttpClient
                .GetFromJsonAsync<List<DocenteCursoDTO>>($"docentes-cursos/curso/{idCurso}")
                ?? new List<DocenteCursoDTO>();
        }
        catch (HttpRequestException)
        {
            if (!IsDisposed && _cursoDetalleId == idCurso)
            {
                _docentes = null;
                dgvDocentes.DataSource = null;
            }
            return;
        }

        if (IsDisposed || _cursoDetalleId != idCurso)
            return;

        _docentes = lista
            .Select(d => new DocenteFila
            {
                Id = d.Id,
                Docente = NombrePersona(d.IdDocente, "Docente"),
                Cargo = CargoDocenteDTO.Nombre(d.Cargo),
                Dto = d
            })
            .OrderBy(f => f.Docente)
            .ToList();

        dgvDocentes.DataSource = _docentes;
        Encabezado(dgvDocentes, nameof(DocenteFila.Docente), "Docente", llenar: true);
        Encabezado(dgvDocentes, nameof(DocenteFila.Cargo), "Cargo");
    }

    private static void Encabezado(DataGridView grilla, string columna, string texto, bool llenar = false)
    {
        if (grilla.Columns[columna] is not { } col)
            return;

        col.HeaderText = texto;
        if (llenar)
            col.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
    }

    // Cada botón se habilita según el permiso del usuario 
    private void ActualizarBotonesDetalle()
    {
        bool hayCurso = _cursoDetalleId.HasValue;

        btnInscribir.Enabled = hayCurso && ApiSession.TienePermiso("Inscripciones", "Alta");
        btnModificarInscripcion.Enabled = hayCurso && ApiSession.TienePermiso("Inscripciones", "Modificar");
        btnQuitarInscripcion.Enabled = hayCurso && ApiSession.TienePermiso("Inscripciones", "Baja");

        btnAsignarDocente.Enabled = hayCurso && ApiSession.TienePermiso("DocentesCursos", "Alta");
        btnModificarDocente.Enabled = hayCurso && ApiSession.TienePermiso("DocentesCursos", "Modificar");
        btnQuitarDocente.Enabled = hayCurso && ApiSession.TienePermiso("DocentesCursos", "Baja");
    }

    //  Maestro: Cursos

    private async void btnAlta_Click(object sender, EventArgs e)
    {
        if (_materias.Count == 0 || _comisiones.Count == 0)
        {
            MessageBox.Show("Para crear un curso hacen falta materias y comisiones cargadas, " +
                "y permiso de consulta sobre ambas.",
                "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        using var form = new FormCursoEdit(_materias, _comisiones);
        if (form.ShowDialog(this) != DialogResult.OK)
            return;

        try
        {
            var respuesta = await ApiSession.HttpClient.PostAsJsonAsync("cursos", form.Curso);
            if (!respuesta.IsSuccessStatusCode)
            {
                MessageBox.Show(await ErrorApi.LeerMensajeAsync(respuesta),
                    "No se pudo crear el curso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            await CargarCursosAsync();
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error al crear el curso: {ex.Message}",
                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private async void btnModificar_Click(object sender, EventArgs e)
    {
        var curso = CursoSeleccionado();
        if (curso is null)
        {
            AvisarSinSeleccion("un curso");
            return;
        }

        if (_materias.Count == 0 || _comisiones.Count == 0)
        {
            MessageBox.Show("Para modificar un curso hacen falta materias y comisiones cargadas, " +
                "y permiso de consulta sobre ambas.",
                "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        using var form = new FormCursoEdit(_materias, _comisiones, curso.Dto);
        if (form.ShowDialog(this) != DialogResult.OK)
            return;

        try
        {
            var respuesta = await ApiSession.HttpClient.PutAsJsonAsync("cursos", form.Curso);
            if (!respuesta.IsSuccessStatusCode)
            {
                MessageBox.Show(await ErrorApi.LeerMensajeAsync(respuesta),
                    "No se pudo modificar el curso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            await CargarCursosAsync();
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error al modificar el curso: {ex.Message}",
                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private async void btnBaja_Click(object sender, EventArgs e)
    {
        var curso = CursoSeleccionado();
        if (curso is null)
        {
            AvisarSinSeleccion("un curso");
            return;
        }

        int inscriptos = _inscripciones?.Count ?? 0;
        int docentes = _docentes?.Count ?? 0;
        if (inscriptos > 0 || docentes > 0)
        {
            MessageBox.Show($"No se puede eliminar el curso: tiene {inscriptos} alumno(s) inscripto(s) " +
                $"y {docentes} docente(s) asignado(s).\n\nQuite primero esos registros en las pestañas de abajo.",
                "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        var confirmacion = MessageBox.Show(
            $"¿Confirma eliminar el curso \"{curso.Materia}\" - Comisión {curso.Comision} - {curso.Anio}?",
            "Confirmar baja", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
        if (confirmacion != DialogResult.Yes)
            return;

        try
        {
            var respuesta = await ApiSession.HttpClient.DeleteAsync($"cursos/{curso.Id}");
            if (!respuesta.IsSuccessStatusCode)
            {
                MessageBox.Show(await ErrorApi.LeerMensajeAsync(respuesta),
                    "No se pudo eliminar el curso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            await CargarCursosAsync();
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error al eliminar el curso: {ex.Message}",
                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private async void btnRefrescar_Click(object sender, EventArgs e)
    {
        try
        {
            await CargarReferenciasAsync();
            await CargarCursosAsync();
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error al actualizar: {ex.Message}",
                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    //  Detalle 1: Alumnos inscriptos

    private async void btnInscribir_Click(object sender, EventArgs e)
    {
        var curso = CursoSeleccionado();
        if (curso is null)
            return;

        var alumnos = _personas.Where(p => p.TipoPersona == "Alumno").ToList();
        if (alumnos.Count == 0)
        {
            MessageBox.Show("No hay alumnos para elegir (o no tiene permiso de consulta sobre Personas).",
                "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        using var form = new FormInscripcionEdit(alumnos, curso.Id);
        if (form.ShowDialog(this) != DialogResult.OK)
            return;

        try
        {
            var respuesta = await ApiSession.HttpClient.PostAsJsonAsync("inscripciones", form.Inscripcion);
            if (!respuesta.IsSuccessStatusCode)
            {
                // Acá aparecen las reglas del servidor: cupo agotado, alumno ya inscripto, etc.
                MessageBox.Show(await ErrorApi.LeerMensajeAsync(respuesta),
                    "No se pudo inscribir", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            await CargarCursosAsync();
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error al inscribir: {ex.Message}",
                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private async void btnModificarInscripcion_Click(object sender, EventArgs e)
    {
        var fila = dgvInscripciones.CurrentRow?.DataBoundItem as InscripcionFila;
        if (fila is null)
        {
            AvisarSinSeleccion("una inscripción");
            return;
        }

        using var form = new FormInscripcionEdit(fila.Dto, fila.Alumno);
        if (form.ShowDialog(this) != DialogResult.OK)
            return;

        try
        {
            var respuesta = await ApiSession.HttpClient.PutAsJsonAsync("inscripciones", form.Inscripcion);
            if (!respuesta.IsSuccessStatusCode)
            {
                MessageBox.Show(await ErrorApi.LeerMensajeAsync(respuesta),
                    "No se pudo modificar la inscripción", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            await CargarCursosAsync();
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error al modificar la inscripción: {ex.Message}",
                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private async void btnQuitarInscripcion_Click(object sender, EventArgs e)
    {
        var fila = dgvInscripciones.CurrentRow?.DataBoundItem as InscripcionFila;
        if (fila is null)
        {
            AvisarSinSeleccion("una inscripción");
            return;
        }

        var confirmacion = MessageBox.Show(
            $"¿Confirma quitar la inscripción de {fila.Alumno}?",
            "Confirmar baja", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
        if (confirmacion != DialogResult.Yes)
            return;

        try
        {
            var respuesta = await ApiSession.HttpClient.DeleteAsync($"inscripciones/{fila.Id}");
            if (!respuesta.IsSuccessStatusCode)
            {
                MessageBox.Show(await ErrorApi.LeerMensajeAsync(respuesta),
                    "No se pudo quitar la inscripción", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            await CargarCursosAsync();
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error al quitar la inscripción: {ex.Message}",
                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    //  Detalle 2: Docentes

    private async void btnAsignarDocente_Click(object sender, EventArgs e)
    {
        var curso = CursoSeleccionado();
        if (curso is null)
            return;

        var profesores = _personas.Where(p => p.TipoPersona == "Profesor").ToList();
        if (profesores.Count == 0)
        {
            MessageBox.Show("No hay docentes para elegir (o no tiene permiso de consulta sobre Personas).",
                "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        using var form = new FormDocenteCursoEdit(profesores, curso.Id);
        if (form.ShowDialog(this) != DialogResult.OK)
            return;

        try
        {
            var respuesta = await ApiSession.HttpClient.PostAsJsonAsync("docentes-cursos", form.Asignacion);
            if (!respuesta.IsSuccessStatusCode)
            {
                MessageBox.Show(await ErrorApi.LeerMensajeAsync(respuesta),
                    "No se pudo asignar el docente", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            await CargarCursosAsync();
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error al asignar el docente: {ex.Message}",
                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private async void btnModificarDocente_Click(object sender, EventArgs e)
    {
        var fila = dgvDocentes.CurrentRow?.DataBoundItem as DocenteFila;
        if (fila is null)
        {
            AvisarSinSeleccion("un docente");
            return;
        }

        using var form = new FormDocenteCursoEdit(fila.Dto, fila.Docente);
        if (form.ShowDialog(this) != DialogResult.OK)
            return;

        try
        {
            var respuesta = await ApiSession.HttpClient.PutAsJsonAsync("docentes-cursos", form.Asignacion);
            if (!respuesta.IsSuccessStatusCode)
            {
                MessageBox.Show(await ErrorApi.LeerMensajeAsync(respuesta),
                    "No se pudo modificar el cargo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            await CargarCursosAsync();
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error al modificar el cargo: {ex.Message}",
                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private async void btnQuitarDocente_Click(object sender, EventArgs e)
    {
        var fila = dgvDocentes.CurrentRow?.DataBoundItem as DocenteFila;
        if (fila is null)
        {
            AvisarSinSeleccion("un docente");
            return;
        }

        var confirmacion = MessageBox.Show(
            $"¿Confirma quitar a {fila.Docente} de este curso?",
            "Confirmar baja", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
        if (confirmacion != DialogResult.Yes)
            return;

        try
        {
            var respuesta = await ApiSession.HttpClient.DeleteAsync($"docentes-cursos/{fila.Id}");
            if (!respuesta.IsSuccessStatusCode)
            {
                MessageBox.Show(await ErrorApi.LeerMensajeAsync(respuesta),
                    "No se pudo quitar al docente", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            await CargarCursosAsync();
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error al quitar al docente: {ex.Message}",
                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private CursoFila? CursoSeleccionado() => dgvCursos.CurrentRow?.DataBoundItem as CursoFila;

    private static void AvisarSinSeleccion(string que)
    {
        MessageBox.Show($"Debe seleccionar {que} de la grilla.",
            "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
    }
}
