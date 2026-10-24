namespace Domain.Model
{
    // Permisos que se asignan automáticamente cuando se crea la cuenta de una persona.
    public static class PermisosPorDefecto
    {
        private static readonly string[] Modulos =
        {
            "Personas", "Materias", "Planes", "Especialidades",
            "Comisiones", "Cursos", "Inscripciones", "DocentesCursos"
        };

        // Profesor: consulta general sobre todos los módulos
        // Puede modificar Inscripciones (carga de notas).
        private static readonly IReadOnlyList<PermisoUsuarioInfo> Profesor =
            Modulos.Select(m => new PermisoUsuarioInfo(
                Modulo: m,
                PuedeAlta: false,
                PuedeBaja: false,
                PuedeModificar: m == "Inscripciones",
                PuedeConsultar: true)).ToList();

        // Alumno: solo consulta del catálogo académico.
        // No recibe Personas, Cursos, Inscripciones ni DocentesCursos
        private static readonly IReadOnlyList<PermisoUsuarioInfo> Alumno =
            new[] { "Materias", "Planes", "Especialidades", "Comisiones" }
                .Select(m => new PermisoUsuarioInfo(m, false, false, false, true)).ToList();

        public static IReadOnlyList<PermisoUsuarioInfo> Para(TipoPersona tipoPersona) => tipoPersona switch
        {
            TipoPersona.Profesor => Profesor,
            TipoPersona.Alumno => Alumno,
            _ => Array.Empty<PermisoUsuarioInfo>()
        };
    }
}
