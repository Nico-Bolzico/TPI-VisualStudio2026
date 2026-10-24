using System;
using System.Net.Http.Json;
using System.Windows.Forms;
using DTOs;

namespace WinForms;

// Perfil propio: el usuario conectado ve y modifica SUS datos personales.
// Usuario, Legajo y Categoría son de solo lectura (los define la facultad).
public partial class FormPerfil : Form
{
    private PerfilDTO? perfil;

    public FormPerfil()
    {
        InitializeComponent();
    }

    private async void FormPerfil_Load(object sender, EventArgs e)
    {
        try
        {
            perfil = await ApiSession.HttpClient.GetFromJsonAsync<PerfilDTO>("perfil");

            if (perfil is null)
            {
                MessageBox.Show("No se pudo cargar el perfil.",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Close();
                return;
            }

            txtUsuario.Text = perfil.NombreUsuario;
            txtLegajo.Text = perfil.Legajo.ToString();
            txtCategoria.Text = perfil.TipoPersona;
            txtNombre.Text = perfil.Nombre;
            txtApellido.Text = perfil.Apellido;
            txtEmail.Text = perfil.Email;
            txtDireccion.Text = perfil.Direccion;
            txtTelefono.Text = perfil.Telefono;
            dtpFechaNacimiento.Value = perfil.FechaNacimiento;
            txtNombre.Select();
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error al cargar el perfil: {ex.Message}",
                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            Close();
        }
    }

    private async void btnCambiarPassword_Click(object sender, EventArgs e)
    {
        using var formPassword = new FormCambiarPassword();

        if (formPassword.ShowDialog(this) != DialogResult.OK)
            return;

        try
        {
            var dto = new CambioPasswordDTO
            {
                PasswordActual = formPassword.PasswordActual,
                PasswordNueva = formPassword.PasswordNueva
            };

            var response = await ApiSession.HttpClient.PutAsJsonAsync("perfil/password", dto);

            if (!response.IsSuccessStatusCode)
            {
                string mensaje = await ErrorApi.LeerMensajeAsync(response);
                MessageBox.Show($"No se pudo cambiar la contraseña: {mensaje}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            MessageBox.Show("Contraseña actualizada con éxito.",
                "Mi perfil", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error al cambiar la contraseña: {ex.Message}",
                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private async void btnGuardar_Click(object sender, EventArgs e)
    {
        if (perfil is null)
            return;

        if (string.IsNullOrWhiteSpace(txtNombre.Text) || string.IsNullOrWhiteSpace(txtApellido.Text))
        {
            MessageBox.Show("Nombre y Apellido son obligatorios.",
                "Datos incompletos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        if (string.IsNullOrWhiteSpace(txtEmail.Text))
        {
            MessageBox.Show("El Email es obligatorio.",
                "Datos incompletos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        perfil.Nombre = txtNombre.Text.Trim();
        perfil.Apellido = txtApellido.Text.Trim();
        perfil.Email = txtEmail.Text.Trim();
        perfil.Direccion = txtDireccion.Text.Trim();
        perfil.Telefono = txtTelefono.Text.Trim();
        perfil.FechaNacimiento = dtpFechaNacimiento.Value;

        try
        {
            btnGuardar.Enabled = false;

            var response = await ApiSession.HttpClient.PutAsJsonAsync("perfil", perfil);

            if (!response.IsSuccessStatusCode)
            {
                string mensaje = await ErrorApi.LeerMensajeAsync(response);
                MessageBox.Show($"No se pudo guardar: {mensaje}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Mantener sincronizados los datos de la sesión
            if (ApiSession.UsuarioActual is not null)
            {
                ApiSession.UsuarioActual.Nombre = perfil.Nombre;
                ApiSession.UsuarioActual.Apellido = perfil.Apellido;
                ApiSession.UsuarioActual.Email = perfil.Email;
            }

            MessageBox.Show("Perfil actualizado con éxito.",
                "Mi perfil", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error al guardar: {ex.Message}",
                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        finally
        {
            btnGuardar.Enabled = true;
        }
    }
}
