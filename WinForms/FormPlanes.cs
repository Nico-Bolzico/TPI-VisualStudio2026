using System.Net.Http.Json;
using System.Web;
using DTOs;

namespace WinForms;

// CRUD Maestro/Detalle: Plan (maestro) / Materias (detalle).
public partial class FormPlanes : Form
{
    private const string Modulo = "Planes";

    public FormPlanes()
    {
        InitializeComponent();
    }

    private async void FormPlanes_Load(object sender, EventArgs e)
    {
        // La visibilidad de las acciones depende de los permisos que vinieron en el token/login.
        btnAlta.Enabled = ApiSession.TienePermiso(Modulo, "Alta");
        btnModificar.Enabled = ApiSession.TienePermiso(Modulo, "Modificar");
        btnBaja.Enabled = ApiSession.TienePermiso(Modulo, "Baja");

        await CargarGrillaAsync();
    }

    private async Task CargarGrillaAsync()
    {
        try
        {
            var planes = await ApiSession.HttpClient.GetFromJsonAsync<IEnumerable<PlanDTO>>("planes");
            MostrarPlanes(planes);
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error al cargar los planes: {ex.Message}",
                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void MostrarPlanes(IEnumerable<PlanDTO>? planes)
    {
        dgvPlanes.DataSource = planes?.ToList();

        if (dgvPlanes.Columns["Materias"] is { } colMaterias) colMaterias.Visible = false;
        if (dgvPlanes.Columns["Descripcion"] is { } colDesc)
        {
            colDesc.HeaderText = "Descripción";
            colDesc.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        }
        if (dgvPlanes.Columns["IdEspecialidad"] is { } colEsp) colEsp.HeaderText = "Id Especialidad";

        MostrarDetalle();
    }

    // Muestra en la grilla inferior las materias del plan seleccionado en la grilla maestra.
    private void MostrarDetalle()
    {
        if (dgvPlanes.CurrentRow?.DataBoundItem is PlanDTO plan)
            dgvDetalle.DataSource = plan.Materias.ToList();
        else
            dgvDetalle.DataSource = null;

        if (dgvDetalle.Columns["Id"] is { } colId) colId.Visible = false;
        if (dgvDetalle.Columns["IdPlan"] is { } colPlan) colPlan.Visible = false;
        if (dgvDetalle.Columns["Descripcion"] is { } colDesc)
        {
            colDesc.HeaderText = "Descripción";
            colDesc.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        }
        if (dgvDetalle.Columns["HsSemanales"] is { } colSem) colSem.HeaderText = "Hs. semanales";
        if (dgvDetalle.Columns["HsTotales"] is { } colTot) colTot.HeaderText = "Hs. totales";
    }

    private void dgvPlanes_SelectionChanged(object sender, EventArgs e)
    {
        MostrarDetalle();
    }

    private async void btnBuscar_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(txtBusqueda.Text))
        {
            await CargarGrillaAsync();
            return;
        }

        try
        {
            string textoCodificado = HttpUtility.UrlEncode(txtBusqueda.Text.Trim());

            var planes = await ApiSession.HttpClient
                .GetFromJsonAsync<IEnumerable<PlanDTO>>($"planes/criteria?texto={textoCodificado}");

            MostrarPlanes(planes);
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error al buscar: {ex.Message}",
                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private async void btnMostrarTodos_Click(object sender, EventArgs e)
    {
        txtBusqueda.Text = string.Empty;
        await CargarGrillaAsync();
    }

    private async void btnAlta_Click(object sender, EventArgs e)
    {
        using var formEdit = new FormPlanEdit();

        if (formEdit.ShowDialog(this) != DialogResult.OK)
            return;

        try
        {
            var response = await ApiSession.HttpClient.PostAsJsonAsync("planes", formEdit.Plan);

            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();
                MessageBox.Show($"No se pudo dar de alta: {error}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            await CargarGrillaAsync();
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error al dar de alta: {ex.Message}",
                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private async void btnModificar_Click(object sender, EventArgs e)
    {
        var plan = ObtenerSeleccionado();
        if (plan is null)
            return;

        using var formEdit = new FormPlanEdit(plan);

        if (formEdit.ShowDialog(this) != DialogResult.OK)
            return;

        try
        {
            var response = await ApiSession.HttpClient.PutAsJsonAsync("planes", formEdit.Plan);

            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();
                MessageBox.Show($"No se pudo modificar: {error}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                // El DTO local fue modificado por el formulario: se recarga desde la API.
            }

            await CargarGrillaAsync();
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error al modificar: {ex.Message}",
                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private async void btnBaja_Click(object sender, EventArgs e)
    {
        var plan = ObtenerSeleccionado();
        if (plan is null)
            return;

        var confirmacion = MessageBox.Show(
            $"¿Confirma eliminar el plan \"{plan.Descripcion}\"?\n" +
            $"Se eliminarán también sus {plan.Materias.Count} materia(s).",
            "Confirmar baja", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

        if (confirmacion != DialogResult.Yes)
            return;

        try
        {
            var response = await ApiSession.HttpClient.DeleteAsync($"planes/{plan.Id}");

            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();
                MessageBox.Show($"No se pudo eliminar: {error}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            await CargarGrillaAsync();
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error al eliminar: {ex.Message}",
                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private PlanDTO? ObtenerSeleccionado()
    {
        if (dgvPlanes.CurrentRow?.DataBoundItem is not PlanDTO plan)
        {
            MessageBox.Show("Debe seleccionar un plan de la grilla.",
                "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return null;
        }

        return plan;
    }
}
