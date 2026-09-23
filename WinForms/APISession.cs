using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using DTOs;

namespace WinForms;

// Mantiene 1 sola instancia de HttpClient para toda la aplicación
public static class ApiSession
{
    private static string ObtenerApiUrl()
    {
        string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "appsettings.json");
        if (!File.Exists(path))
        {
            throw new FileNotFoundException($"No se encontró el archivo de configuración en: {path}");
        }

        string json = File.ReadAllText(path);
        using var doc = JsonDocument.Parse(json);
        if (doc.RootElement.TryGetProperty("ApiUrl", out var urlProp) && !string.IsNullOrWhiteSpace(urlProp.GetString()))
        {
            return urlProp.GetString()!.Trim();
        }

        throw new InvalidOperationException("No se encontró o está vacía la configuración 'ApiUrl' en appsettings.json.");
    }

    public static readonly HttpClient HttpClient = new()
    {
        BaseAddress = new Uri(ObtenerApiUrl())
    };

    public static UsuarioDTO? UsuarioActual { get; set; }
    public static List<PermisoDTO> PermisosActuales { get; private set; } = new();

    public static void IniciarSesion(LoginResponseDTO respuesta)
    {
        UsuarioActual = respuesta.Usuario;
        PermisosActuales = respuesta.Permisos;
        HttpClient.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", respuesta.Token);
    }

    public static void CerrarSesion()
    {
        UsuarioActual = null;
        PermisosActuales = new List<PermisoDTO>();
        HttpClient.DefaultRequestHeaders.Authorization = null;
    }

    public static bool TienePermiso(string modulo, string accion)
    {
        var permiso = PermisosActuales.FirstOrDefault(p => p.Modulo == modulo);
        if (permiso is null) return false;

        return accion switch
        {
            "Alta" => permiso.PuedeAlta,
            "Baja" => permiso.PuedeBaja,
            "Modificar" => permiso.PuedeModificar,
            "Consultar" => permiso.PuedeConsultar,
            _ => false
        };
    }
}
