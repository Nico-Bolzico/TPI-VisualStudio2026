using Microsoft.EntityFrameworkCore;
using Domain.Model;

namespace Data
{
    public class TPIContext : DbContext
    {
        public DbSet<Especialidad> Especialidades { get; set; }
        public DbSet<Plan> Planes { get; set; }
        public DbSet<Comision> Comisiones { get; set; }
        public DbSet<Materia> Materias { get; set; }
        public DbSet<Curso> Cursos { get; set; }
        public DbSet<AlumnoInscripcion> AlumnosInscripciones { get; set; }
        public DbSet<DocenteCurso> DocentesCursos { get; set; }
        public DbSet<Persona> Personas { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Modulo> Modulos { get; set; }
        public DbSet<ModuloUsuario> ModulosUsuarios { get; set; }

        public TPIContext(DbContextOptions<TPIContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Plan>(entity =>
            {
                entity.HasKey(p => p.Id);
                entity.HasMany(p => p.Materias)
                      .WithOne()
                      .HasForeignKey(m => m.IdPlan);
            });
        }
    }
}
