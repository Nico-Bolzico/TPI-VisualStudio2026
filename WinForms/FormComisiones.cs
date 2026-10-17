using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Web;
using System.Windows.Forms;
using DTOs;

namespace WinForms;

public partial class FormComisiones : Form
{
    private sealed class ComisionFila
    {
        public int Id { get; init; }
        public string Comision { get; init; } = string.Empty;
        public int Anio { get; init; }
        public string Plan { get; init; } = string.Empty;
        [Browsable(false)] public ComisionDTO Dto { get; init; } = new();
    }

    private List<PlanDTO> _planes = new();

    public FormComisiones()
    {
        InitializeComponent();
    }

    private async void FormComisiones_Load(object sender, EventArgs e)
    {
        btnAlta.Enabled = ApiSession.TienePermiso("Comisiones", "Alta");
        btnModificar.Enabled = ApiSession.TienePermiso("Comisiones", "Modificar");
        btnBaja.Enabled = ApiSession.TienePermiso("Comisiones", "Baja");

        await CargarPlanesAsync();
        await CargarGrillaAsync();
    }

    private async Task CargarPlanesAsync()
    {
        try
        {
            _planes = await ApiSession.HttpClient.GetFromJsonAsync<List<PlanDTO>>("planes") ?? new();
        }
        catch
        {
            _planes = new List<PlanDTO>();
        }
    }

    private async Task CargarGrillaAsync(string? textoBusqueda = null)
    {
        try
        {
            List<ComisionDTO>? lista;

            if (string.IsNullOrWhiteSpace(textoBusqueda))
            {
                lista = await ApiSession.HttpClient.GetFromJsonAsync<List<ComisionDTO>>("comisiones");
            }
            else
            {
                string encoded = HttpUtility.UrlEncode(textoBusqueda.Trim());
                lista = await ApiSession.HttpClient.GetFromJsonAsync<List<ComisionDTO>>($"comisiones/criteria?texto={encoded}");
            }

            var filas = (lista ?? new List<ComisionDTO>())
                .Select(c => new ComisionFila
                {
                    Id = c.Id,
                    Comision = c.Descripcion,
                    Anio = c.AnioEspecialidad,
                    Plan = _planes.FirstOrDefault(p => p.Id == c.IdPlan)?.Descripcion ?? $"Plan N° {c.IdPlan}",
                    Dto = c
                })
                .OrderBy(f => f.Anio)
                .ThenBy(f => f.Comision)
                .ToList();

            dgvComisiones.DataSource = filas;

            if (dgvComisiones.Columns[nameof(ComisionFila.Comision)] is { } colComision)
                colComision.HeaderText = "Comisión";

            if (dgvComisiones.Columns[nameof(ComisionFila.Anio)] is { } colAnio)
                colAnio.HeaderText = "Año";
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error al cargar las comisiones: {ex.Message}",
                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private async void btnBuscar_Click(object sender, EventArgs e)
    {
        await CargarGrillaAsync(txtBusqueda.Text);
    }

    private async void btnMostrarTodos_Click(object sender, EventArgs e)
    {
        txtBusqueda.Text = string.Empty;
        await CargarGrillaAsync();
    }

    private async void btnAlta_Click(object sender, EventArgs e)
    {
        if (_planes.Count == 0)
            await CargarPlanesAsync();

        using var formEdit = new FormComisionEdit(_planes);

        if (formEdit.ShowDialog(this) == DialogResult.OK)
        {
            try
            {
                var response = await ApiSession.HttpClient.PostAsJsonAsync("comisiones", formEdit.Comision);

                if (!response.IsSuccessStatusCode)
                {
                    string mensaje = await ErrorApi.LeerMensajeAsync(response);
                    MessageBox.Show($"No se pudo dar de alta la comisión: {mensaje}",
                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                await CargarGrillaAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error de conexión al dar de alta: {ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }

    private async void btnModificar_Click(object sender, EventArgs e)
    {
        var seleccionada = ObtenerSeleccionada();
        if (seleccionada is null) return;

        if (_planes.Count == 0)
            await CargarPlanesAsync();

        using var formEdit = new FormComisionEdit(seleccionada.Dto, _planes);

        if (formEdit.ShowDialog(this) == DialogResult.OK)
        {
            try
            {
                var response = await ApiSession.HttpClient.PutAsJsonAsync("comisiones", formEdit.Comision);

                if (!response.IsSuccessStatusCode)
                {
                    string mensaje = await ErrorApi.LeerMensajeAsync(response);
                    MessageBox.Show($"No se pudo modificar la comisión: {mensaje}",
                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                await CargarGrillaAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error de conexión al modificar: {ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }

    private async void btnBaja_Click(object sender, EventArgs e)
    {
        var seleccionada = ObtenerSeleccionada();
        if (seleccionada is null) return;

        var confirmacion = MessageBox.Show(
            $"¿Confirma eliminar la comisión \"{seleccionada.Comision}\"?",
            "Confirmar baja", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

        if (confirmacion != DialogResult.Yes) return;

        try
        {
            var response = await ApiSession.HttpClient.DeleteAsync($"comisiones/{seleccionada.Id}");

            if (!response.IsSuccessStatusCode)
            {
                string mensaje = await ErrorApi.LeerMensajeAsync(response);
                MessageBox.Show($"No se pudo eliminar la comisión: {mensaje}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            await CargarGrillaAsync();
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error de conexión al eliminar: {ex.Message}",
                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private ComisionFila? ObtenerSeleccionada()
    {
        if (dgvComisiones.CurrentRow?.DataBoundItem is not ComisionFila fila)
        {
            MessageBox.Show("Debe seleccionar una comisión de la grilla.",
                "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return null;
        }

        return fila;
    }
}
