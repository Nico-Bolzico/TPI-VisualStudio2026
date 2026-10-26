using Application.Services;
using Data;
using Domain.Model;

namespace WebAPI
{
    // Carga los datos iniciales de la base.
    //
    // Usuarios de prueba (ambos son profesores reales, no existe el rol "administrador"):
    //   lgomez    / lgomez123     -> Laura Gómez: acceso completo a todos los módulos
    //   cmartinez / cmartinez123  -> Carlos Martínez: permisos por defecto de Profesor (ver PermisosPorDefecto)
    public static class DbSeeder
    {
        public static void Seed(TPIContext context)
        {
            if (context.Usuarios.Any())
                return;

            // Estructura académica
            // Se crea en orden (especialidad -> plan -> comisión -> cursos)
            var especialidades = new[]
            {
            new Especialidad(0, "Ingeniería en Sistemas de Información"),
            new Especialidad(0, "Ingeniería en Energía Eléctrica"),
            new Especialidad(0, "Ingeniería Mecánica"),
            new Especialidad(0, "Ingeniería Química"),
            new Especialidad(0, "Ingeniería Civil")
            };
            context.Especialidades.AddRange(especialidades);
            context.SaveChanges();

            var plan = new Plan(0, "Plan 2023 ISI", especialidades[0].Id);

            plan.AgregarMateria(Materia.NuevaParaPlan("Análisis Matemático I", 5, 160));
            plan.AgregarMateria(Materia.NuevaParaPlan("Álgebra y Geometría Analítica", 5, 160));
            plan.AgregarMateria(Materia.NuevaParaPlan("Algoritmos y Estructuras de Datos", 5, 160));
            plan.AgregarMateria(Materia.NuevaParaPlan("Sistemas y Procesos de Negocio", 3, 96));
            plan.AgregarMateria(Materia.NuevaParaPlan("Análisis Matemático II", 5, 160));
            plan.AgregarMateria(Materia.NuevaParaPlan("Física II", 5, 160));
            plan.AgregarMateria(Materia.NuevaParaPlan("Análisis Numérico", 3, 96));
            context.Planes.Add(plan);
            context.SaveChanges();

            var comision1K1 = new Comision(0, "1K1", 1, plan.Id);
            var comision1K2 = new Comision(0, "1K2", 1, plan.Id);
            var comision2K1 = new Comision(0, "2K1", 2, plan.Id);
            context.Comisiones.AddRange(comision1K1, comision1K2, comision2K1);
            context.SaveChanges();

            var algoritmos = plan.Materias.First(m => m.Descripcion == "Algoritmos y Estructuras de Datos");
            var sistemas = plan.Materias.First(m => m.Descripcion == "Sistemas y Procesos de Negocio");
            var am2 = plan.Materias.First(m => m.Descripcion == "Análisis Matemático II");
            var fisica2 = plan.Materias.First(m => m.Descripcion == "Física II");

            // Con 2 inscriptos ya cargados, permite mostrar cómo se rechaza una tercera inscripción por cupo completo.
            var cursoAlgoritmos = new Curso(0, algoritmos.Id, comision1K1.Id, 2026, 35);
            var cursoSistemas = new Curso(0, sistemas.Id, comision1K1.Id, 2026, 2);
            var cursoAM2 = new Curso(0, am2.Id, comision2K1.Id, 2026, 30);
            var cursoFisica2 = new Curso(0, fisica2.Id, comision2K1.Id, 2026, 25);
            context.Cursos.AddRange(cursoAlgoritmos, cursoSistemas, cursoAM2, cursoFisica2);
            context.SaveChanges();

            // Personas (Docentes y Alumnos)
            var personaProfesor = NuevaPersona(1001, "Laura", "Gómez", "laura.gomez@academia.edu",
                new DateTime(1980, 5, 12), TipoPersona.Profesor, null);
            var personaProfesor2 = NuevaPersona(1002, "Carlos", "Martínez", "carlos.martinez@academia.edu",
                new DateTime(1975, 8, 20), TipoPersona.Profesor, null);

            var juan = NuevaPersona(2001, "Juan", "Pérez", "juan.perez@alumnos.academia.edu",
                new DateTime(2005, 3, 14), TipoPersona.Alumno, plan.Id);
            var maria = NuevaPersona(2002, "María", "López", "maria.lopez@alumnos.academia.edu",
                new DateTime(2004, 11, 2), TipoPersona.Alumno, plan.Id);
            var lucas = NuevaPersona(2003, "Lucas", "Fernández", "lucas.fernandez@alumnos.academia.edu",
                new DateTime(2005, 7, 21), TipoPersona.Alumno, plan.Id);
            var sofia = NuevaPersona(2004, "Sofía", "Rodríguez", "sofia.rodriguez@alumnos.academia.edu",
                new DateTime(2004, 6, 18), TipoPersona.Alumno, plan.Id);
            var mateo = NuevaPersona(2005, "Mateo", "Gómez", "mateo.gomez@alumnos.academia.edu",
                new DateTime(2005, 1, 25), TipoPersona.Alumno, plan.Id);
            var valentina = NuevaPersona(2006, "Valentina", "Romero", "valentina.romero@alumnos.academia.edu",
                new DateTime(2004, 9, 30), TipoPersona.Alumno, plan.Id);
            var joaquin = NuevaPersona(2007, "Joaquín", "Díaz", "joaquin.diaz@alumnos.academia.edu",
                new DateTime(2005, 4, 10), TipoPersona.Alumno, plan.Id);
            var camila = NuevaPersona(2008, "Camila", "Benítez", "camila.benitez@alumnos.academia.edu",
                new DateTime(2004, 12, 5), TipoPersona.Alumno, plan.Id);
            var tomas = NuevaPersona(2009, "Tomás", "Morales", "tomas.morales@alumnos.academia.edu",
                new DateTime(2005, 8, 19), TipoPersona.Alumno, plan.Id);
            var lucia = NuevaPersona(2010, "Lucía", "Herrera", "lucia.herrera@alumnos.academia.edu",
                new DateTime(2004, 2, 14), TipoPersona.Alumno, plan.Id);

            context.Personas.AddRange(personaProfesor, personaProfesor2,
                juan, maria, lucas, sofia, mateo, valentina, joaquin, camila, tomas, lucia);
            context.SaveChanges();

            // Docentes e inscripciones 
            // cargo = 1 (Titular)
            context.DocentesCursos.AddRange(
                new DocenteCurso(0, cursoAlgoritmos.Id, personaProfesor.Id, 1),
                new DocenteCurso(0, cursoSistemas.Id, personaProfesor.Id, 1),
                new DocenteCurso(0, cursoAM2.Id, personaProfesor2.Id, 1),
                new DocenteCurso(0, cursoFisica2.Id, personaProfesor2.Id, 1));

            context.AlumnosInscripciones.AddRange(
                new AlumnoInscripcion(0, juan.Id, cursoSistemas.Id),
                new AlumnoInscripcion(0, maria.Id, cursoSistemas.Id),
                new AlumnoInscripcion(0, juan.Id, cursoAlgoritmos.Id),
                new AlumnoInscripcion(0, sofia.Id, cursoAlgoritmos.Id),
                new AlumnoInscripcion(0, mateo.Id, cursoAlgoritmos.Id),
                new AlumnoInscripcion(0, valentina.Id, cursoAlgoritmos.Id),
                new AlumnoInscripcion(0, joaquin.Id, cursoAM2.Id, "Regular", 8),
                new AlumnoInscripcion(0, camila.Id, cursoAM2.Id, "Aprobado", 9),
                new AlumnoInscripcion(0, tomas.Id, cursoAM2.Id, "Regular", 7),
                new AlumnoInscripcion(0, tomas.Id, cursoFisica2.Id),
                new AlumnoInscripcion(0, lucia.Id, cursoFisica2.Id));
            context.SaveChanges();

            //  Usuarios
            var usuarioLaura = new Usuario(0, "lgomez", PasswordHasher.Hash("lgomez123"),
                habilitado: true, cambiaClave: false, idPersona: personaProfesor.Id);
            var usuarioCarlos = new Usuario(0, "cmartinez", PasswordHasher.Hash("cmartinez123"),
                habilitado: true, cambiaClave: false, idPersona: personaProfesor2.Id);
            context.Usuarios.AddRange(usuarioLaura, usuarioCarlos);
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
                // Laura Gómez: acceso completo
                context.ModulosUsuarios.Add(
                    new ModuloUsuario(0, modulo.Id, usuarioLaura.Id, true, true, true, true));

                // Carlos Martínez: recibe los permisos por defecto de un Profesor (igual que una cuenta nueva)
                var porDefecto = PermisosPorDefecto.Para(TipoPersona.Profesor)
                    .FirstOrDefault(p => p.Modulo == modulo.Descripcion);
                if (porDefecto != null)
                {
                    context.ModulosUsuarios.Add(new ModuloUsuario(0, modulo.Id, usuarioCarlos.Id,
                        porDefecto.PuedeAlta, porDefecto.PuedeBaja, porDefecto.PuedeModificar, porDefecto.PuedeConsultar));
                }
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