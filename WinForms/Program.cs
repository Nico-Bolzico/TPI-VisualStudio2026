namespace WinForms;

static class Program
{
    [STAThread]
    static void Main()
    {
        ApplicationConfiguration.Initialize();

        while (true)
        {
            using var formLogin = new FormLogin();

            if (formLogin.ShowDialog() != DialogResult.OK)
            {
                // El usuario cerró la ventana de login sin loguearse -> se termina la app
                break;
            }

            using var formPrincipal = new FormPrincipal();
            Application.Run(formPrincipal);

            if (!formPrincipal.DeseaCerrarSesion)
            {
                // Cerró con "Salir" (o la X) -> se termina la app
                break;
            }

            // Cerró con "Cerrar sesión" -> se limpia la sesión y se vuelve a mostrar el login
            ApiSession.CerrarSesion();
        }
    }
}