using Microsoft.AspNetCore.Authorization;

namespace WebAPI.Authorization
{
    public class PermisoRequirement : IAuthorizationRequirement
    {
        public string Modulo { get; }
        public string Accion { get; } // "Alta", "Baja", "Modificar", "Consultar"

        public PermisoRequirement(string modulo, string accion)
        {
            Modulo = modulo;
            Accion = accion;
        }
    }
}
