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

public partial class FormPersonas : Form
{
    public FormPersonas()
    {
        InitializeComponent();
    }

    private async void FormPersonas_Load(object sender, EventArgs e)
    {
        await CargarGrillaAsync();
    }

    private async Task CargarGrillaAsync()
    {
        try
        {
            var personas = await ApiSession.HttpClient.GetFromJsonAsync<IEnumerable<PersonaDTO>>("personas");
            dgvPersonas.DataSource = personas?.ToList();
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error al cargar las personas: {ex.Message}",
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
            // HttpUtility.UrlEncode evita romper la URL si el usuario escribe caracteres especiales
            string textoCodificado = HttpUtility.UrlEncode(txtBusqueda.Text.Trim());

            var personas = await ApiSession.HttpClient
                .GetFromJsonAsync<IEnumerable<PersonaDTO>>($"personas/criteria?texto={textoCodificado}");

            dgvPersonas.DataSource = personas?.ToList();
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
        using var formEdit = new FormPersonaEdit();

        if (formEdit.ShowDialog(this) == DialogResult.OK)
        {
            try
            {
                var response = await ApiSession.HttpClient.PostAsJsonAsync("personas", formEdit.Persona);

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
        var persona = ObtenerSeleccionada();
        if (persona is null)
            return;

        using var formEdit = new FormPersonaEdit(persona);

        if (formEdit.ShowDialog(this) == DialogResult.OK)
        {
            try
            {
                var response = await ApiSession.HttpClient.PutAsJsonAsync("personas", formEdit.Persona);

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
        var persona = ObtenerSeleccionada();
        if (persona is null)
            return;

        var confirmacion = MessageBox.Show(
            $"¿Confirma eliminar a {persona.Apellido}, {persona.Nombre}?",
            "Confirmar baja", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

        if (confirmacion != DialogResult.Yes)
            return;

        try
        {
            await ApiSession.HttpClient.DeleteAsync($"personas/{persona.Id}");
            await CargarGrillaAsync();
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error al eliminar: {ex.Message}",
                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private PersonaDTO? ObtenerSeleccionada()
    {
        if (dgvPersonas.CurrentRow?.DataBoundItem is not PersonaDTO persona)
        {
            MessageBox.Show("Debe seleccionar una persona de la grilla.",
                "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return null;
        }

        return persona;
    }
}
