using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Http.Json;
using System.Web;
using DTOs;

namespace WinForms;

public partial class FormMaterias : Form
{
    public FormMaterias()
    {
        InitializeComponent();
    }

    private async void FormMaterias_Load(object sender, EventArgs e)
    {
        await CargarGrillaAsync();
    }

    private async Task CargarGrillaAsync()
    {
        try
        {
            var materias = await ApiSession.HttpClient.GetFromJsonAsync<IEnumerable<MateriaDTO>>("materias");
            dgvMaterias.DataSource = materias?.ToList();
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error al cargar las materias: {ex.Message}",
                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
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

            var materias = await ApiSession.HttpClient
                .GetFromJsonAsync<IEnumerable<MateriaDTO>>($"materias/criteria?texto={textoCodificado}");

            dgvMaterias.DataSource = materias?.ToList();
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
        using var formEdit = new FormMateriaEdit();

        if (formEdit.ShowDialog(this) == DialogResult.OK)
        {
            try
            {
                var response = await ApiSession.HttpClient.PostAsJsonAsync("materias", formEdit.Materia);

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
    }

    private async void btnModificar_Click(object sender, EventArgs e)
    {
        var materia = ObtenerSeleccionada();
        if (materia is null)
            return;

        using var formEdit = new FormMateriaEdit(materia);

        if (formEdit.ShowDialog(this) == DialogResult.OK)
        {
            try
            {
                var response = await ApiSession.HttpClient.PutAsJsonAsync("materias", formEdit.Materia);

                if (!response.IsSuccessStatusCode)
                {
                    var error = await response.Content.ReadAsStringAsync();
                    MessageBox.Show($"No se pudo modificar: {error}",
                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                await CargarGrillaAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al modificar: {ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }

    private async void btnBaja_Click(object sender, EventArgs e)
    {
        var materia = ObtenerSeleccionada();
        if (materia is null)
            return;

        var confirmacion = MessageBox.Show(
            $"¿Confirma eliminar la materia \"{materia.Descripcion}\"?",
            "Confirmar baja", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

        if (confirmacion != DialogResult.Yes)
            return;

        try
        {
            await ApiSession.HttpClient.DeleteAsync($"materias/{materia.Id}");
            await CargarGrillaAsync();
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error al eliminar: {ex.Message}",
                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private MateriaDTO? ObtenerSeleccionada()
    {
        if (dgvMaterias.CurrentRow?.DataBoundItem is not MateriaDTO materia)
        {
            MessageBox.Show("Debe seleccionar una materia de la grilla.",
                "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return null;
        }

        return materia;
    }
}
