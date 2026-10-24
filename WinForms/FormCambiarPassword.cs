using System;
using System.Windows.Forms;

namespace WinForms;

// Pide la contraseña actual y la nueva (dos veces). La llamada a la API la hace quien lo abre.
public partial class FormCambiarPassword : Form
{
    public string PasswordActual { get; private set; } = string.Empty;
    public string PasswordNueva { get; private set; } = string.Empty;

    public FormCambiarPassword()
    {
        InitializeComponent();
    }

    private void FormCambiarPassword_FormClosing(object? sender, FormClosingEventArgs e)
    {
        if (this.DialogResult != DialogResult.OK)
            return;

        if (string.IsNullOrEmpty(txtActual.Text))
        {
            Rechazar("Ingrese su contraseña actual.", e);
            return;
        }

        if (txtNueva.Text.Length < 8)
        {
            Rechazar("La contraseña nueva debe tener al menos 8 caracteres.", e);
            return;
        }

        if (txtNueva.Text != txtConfirmar.Text)
        {
            Rechazar("Las contraseñas nuevas no coinciden.", e);
            return;
        }

        PasswordActual = txtActual.Text;
        PasswordNueva = txtNueva.Text;
    }

    private static void Rechazar(string mensaje, FormClosingEventArgs e)
    {
        MessageBox.Show(mensaje, "Dato inválido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        e.Cancel = true;
    }
}
