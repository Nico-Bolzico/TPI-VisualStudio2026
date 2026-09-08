using System.Net;
using System.Net.Http.Json;
using DTOs;

namespace WinForms;

public partial class FormLogin : Form
{
    public FormLogin()
    {
        InitializeComponent();
    }

    private void FormLogin_Load(object sender, EventArgs e)
    {
    }

    private async void btnIngresar_Click(object sender, EventArgs e)
    {
        lblError.Text = string.Empty;

        if (string.IsNullOrWhiteSpace(txtUsuario.Text) || string.IsNullOrWhiteSpace(txtPassword.Text))
        {
            lblError.Text = "Debe ingresar usuario y contraseña.";
            return;
        }

        var request = new LoginRequestDTO
        {
            NombreUsuario = txtUsuario.Text.Trim(),
            Password = txtPassword.Text
        };

        btnIngresar.Enabled = false;

        try
        {
            var response = await ApiSession.HttpClient.PostAsJsonAsync("login", request);

            if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                lblError.Text = "Usuario o contraseña incorrectos.";
                return;
            }

            response.EnsureSuccessStatusCode();

            var resultado = await response.Content.ReadFromJsonAsync<LoginResponseDTO>();
            ApiSession.IniciarSesion(resultado!);

            this.DialogResult = DialogResult.OK;
            this.Close();
        }
        catch (HttpRequestException ex)
        {
            lblError.Text = $"No se pudo conectar con el servidor: {ex.Message}";
        }
        finally
        {
            btnIngresar.Enabled = true;
        }
    }
}
