namespace WinForms;

public partial class FormLogin : Form
{
    // Credenciales fijas
    // A futuro reemplazar por una llamada a la Web API (endpoint POST /login)
    private const string UsuarioValido = "admin";
    private const string PasswordValido = "admin123";

    public FormLogin()
    {
        InitializeComponent();
    }

    private void btnIngresar_Click(object sender, EventArgs e)
    {
        lblError.Text = string.Empty;

        if (string.IsNullOrWhiteSpace(txtUsuario.Text) || string.IsNullOrWhiteSpace(txtPassword.Text))
        {
            lblError.Text = "Debe ingresar usuario y contraseña.";
            return;
        }

        bool credencialesValidas =
            txtUsuario.Text.Trim() == UsuarioValido &&
            txtPassword.Text == PasswordValido;

        if (!credencialesValidas)
        {
            lblError.Text = "Usuario o contraseña incorrectos.";
            return;
        }

        ApiSession.UsuarioActual = txtUsuario.Text.Trim();

        this.DialogResult = DialogResult.OK;
        this.Close();
    }

    private void label3_Click(object sender, EventArgs e)
    {

    }
}
