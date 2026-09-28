using System.Net.Http.Headers;
using DTOs;

namespace Blazor.Server.Services;

public class APISession
{
    public HttpClient HttpClient { get; }
    public UsuarioDTO? UsuarioActual { get; private set; }
    public List<PermisoDTO> PermisosActuales { get; private set; } = new();

    public bool EstaAutenticado => UsuarioActual != null;

    // Evento para notificar a la interfaz (como la Navbar o Inicio) cuando cambia el estado de sesión
    public event Action? OnChange;

    public APISession(IConfiguration configuration)
    {
        string apiUrl = configuration["ApiUrl"] 
            ?? throw new InvalidOperationException("No se encontró 'ApiUrl' en appsettings.json.");

        HttpClient = new HttpClient
        {
            BaseAddress = new Uri(apiUrl)
        };
    }

    public void IniciarSesion(LoginResponseDTO respuesta)
    {
        UsuarioActual = respuesta.Usuario;
        PermisosActuales = respuesta.Permisos ?? new List<PermisoDTO>();
        HttpClient.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", respuesta.Token);
        
        OnChange?.Invoke();
    }

    public void CerrarSesion()
    {
        UsuarioActual = null;
        PermisosActuales = new List<PermisoDTO>();
        HttpClient.DefaultRequestHeaders.Authorization = null;

        OnChange?.Invoke();
    }

    public bool TienePermiso(string modulo, string accion)
    {
        var permiso = PermisosActuales.FirstOrDefault(p => p.Modulo.Equals(modulo, StringComparison.OrdinalIgnoreCase));
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
