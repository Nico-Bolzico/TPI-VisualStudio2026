using System.Collections.Generic;

namespace DTOs
{
    // El cargo de un docente en un curso se guarda como entero (DocenteCursoDTO.Cargo).
    // Esta tabla centraliza el significado de cada código para que todos los clientes
    // (WinForms, Blazor) muestren lo mismo. Es una convención propia del proyecto:
    // si la cátedra define otros cargos, se cambian solo acá.
    public static class CargoDocenteDTO
    {
        public static readonly IReadOnlyDictionary<int, string> Todos = new Dictionary<int, string>
        {
            { 1, "Titular" },
            { 2, "Adjunto" },
            { 3, "Jefe de trabajos prácticos" },
            { 4, "Ayudante" }
        };

        public static string Nombre(int cargo) =>
            Todos.TryGetValue(cargo, out var nombre) ? nombre : $"Cargo {cargo}";
    }
}
