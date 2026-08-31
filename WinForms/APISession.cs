using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinForms;

// Mantiene 1 sola instancia de HttpClient para toda la aplicación
public static class ApiSession
{
    public static readonly HttpClient HttpClient = new()
    {
        BaseAddress = new Uri("http://localhost:49967")
    };

    public static string? UsuarioActual { get; set; }

    public static void CerrarSesion()
    {
        UsuarioActual = null;
    }
}
