using Application.Services;
using Data;
using Domain.Model;

namespace WebAPI
{
    // Carga los datos iniciales de la base.
    //
    // Usuarios de prueba:
    //   admin    / admin123     -> acceso total
    //   profesor / profesor123  -> solo consulta, salvo Inscripciones
    public static class DbSeeder
    {
        public static void Seed(TPIContext context)
        {
            if (context.Usuarios.Any())
                return;

            // Estructura académica
            // Se crea en orden (especialidad -> plan -> comisión -> cursos)
            var especialidad = new Especialidad(0, "Ingeniería en Sistemas de Información");
            context.Especialidades.Add(especialidad);
            context.SaveChanges();

            var plan = new Plan(0, "Plan 2023", especialidad.Id);
            plan.AgregarMateria(Materia.NuevaParaPlan("Análisis Matemático I", 5, 160));
            plan.AgregarMateria(Materia.NuevaParaPlan("Álgebra y Geometría Analítica", 5, 160));
            plan.AgregarMateria(Materia.NuevaParaPlan("Algoritmos y Estructuras de Datos", 5, 160));
            plan.AgregarMateria(Materia.NuevaParaPlan("Sistemas y Procesos de Negocio", 3, 96));
            context.Planes.Add(plan);
            context.SaveChanges();

            var comision = new Comision(0, "1K1", 1, plan.Id);
            context.Comisiones.Add(comision);
            context.SaveChanges();

            var algoritmos = plan.Materias.First(m => m.Descripcion == "Algoritmos y Estructuras de Datos");
            var sistemas = plan.Materias.First(m => m.Descripcion == "Sistemas y Procesos de Negocio");

            // Con 2 inscriptos ya cargados, permite mostrar cómo se rechaza una tercera inscripción por cupo completo.
            var cursoAlgoritmos = new Curso(0, algoritmos.Id, comision.Id, 2026, 35);
            var cursoSistemas = new Curso(0, sistemas.Id, comision.Id, 2026, 2);
            context.Cursos.AddRange(cursoAlgoritmos, cursoSistemas);
            context.SaveChanges();

            // Personas
            var personaAdmin = NuevaPersona(1, "Administrador", "Sistema", "admin@academia.edu",
                new DateTime(1990, 1, 1), TipoPersona.Profesor, null);
            var personaProfesor = NuevaPersona(1001, "Laura", "Gómez", "laura.gomez@academia.edu",
                new DateTime(1980, 5, 12), TipoPersona.Profesor, null);
            var juan = NuevaPersona(2001, "Juan", "Pérez", "juan.perez@alumnos.academia.edu",
                new DateTime(2005, 3, 14), TipoPersona.Alumno, plan.Id);
            var maria = NuevaPersona(2002, "María", "López", "maria.lopez@alumnos.academia.edu",
                new DateTime(2004, 11, 2), TipoPersona.Alumno, plan.Id);
            var lucas = NuevaPersona(2003, "Lucas", "Fernández", "lucas.fernandez@alumnos.academia.edu",
                new DateTime(2005, 7, 21), TipoPersona.Alumno, plan.Id);

            context.Personas.AddRange(personaAdmin, personaProfesor, juan, maria, lucas);
            context.SaveChanges();

            // Docentes e inscripciones 
            // cargo = 1
            context.DocentesCursos.AddRange(
                new DocenteCurso(0, cursoAlgoritmos.Id, personaProfesor.Id, 1),
                new DocenteCurso(0, cursoSistemas.Id, personaProfesor.Id, 1));

            context.AlumnosInscripciones.AddRange(
                new AlumnoInscripcion(0, juan.Id, cursoSistemas.Id),
                new AlumnoInscripcion(0, maria.Id, cursoSistemas.Id),
                new AlumnoInscripcion(0, juan.Id, cursoAlgoritmos.Id));
            context.SaveChanges();

            //  Usuarios
            var admin = new Usuario(0, "admin", PasswordHasher.Hash("admin123"),
                habilitado: true, cambiaClave: false, idPersona: personaAdmin.Id);
            var profesor = new Usuario(0, "profesor", PasswordHasher.Hash("profesor123"),
                habilitado: true, cambiaClave: false, idPersona: personaProfesor.Id);
            context.Usuarios.AddRange(admin, profesor);
            context.SaveChanges();

            // Módulos y permisos
            var modulos = new[]
            {
                "Personas", "Materias", "Planes", "Especialidades",
                "Comisiones", "Cursos", "Inscripciones", "DocentesCursos"
            }.Select(nombre => new Modulo(0, nombre, true)).ToList();

            context.Modulos.AddRange(modulos);
            context.SaveChanges();

            foreach (var modulo in modulos)
            {
                // admin: control total
                context.ModulosUsuarios.Add(
                    new ModuloUsuario(0, modulo.Id, admin.Id, true, true, true, true));

                bool puedeModificar = modulo.Descripcion == "Inscripciones";
                context.ModulosUsuarios.Add(
                    new ModuloUsuario(0, modulo.Id, profesor.Id, false, false, puedeModificar, true));
            }
            context.SaveChanges();
        }

        private static Persona NuevaPersona(int legajo, string nombre, string apellido, string email,
            DateTime fechaNacimiento, TipoPersona tipo, int? idPlan)
        {
            return new Persona(
                id: 0,
                legajo: legajo,
                nombre: nombre,
                apellido: apellido,
                direccion: "Sin especificar",
                email: email,
                telefono: "Sin especificar",
                fechaNacimiento: fechaNacimiento,
                tipoPersona: tipo,
                idPlan: idPlan);
        }
    }
}