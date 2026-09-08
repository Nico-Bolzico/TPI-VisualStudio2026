using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using DTOs;

namespace WinForms;

// Mantiene 1 sola instancia de HttpClient para toda la aplicación
public static class ApiSession
{
    public static readonly HttpClient HttpClient = new()
    {
        BaseAddress = new Uri("http://localhost:49967")
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
