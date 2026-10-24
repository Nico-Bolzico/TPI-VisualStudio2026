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
                entity.HasOne<Especialidad>()
                      .WithMany()
                      .HasForeignKey(p => p.IdEspecialidad)
                      .OnDelete(DeleteBehavior.Restrict);
                entity.HasMany(p => p.Materias)
                      .WithOne()
                      .HasForeignKey(m => m.IdPlan);
            });

            modelBuilder.Entity<Comision>()
                .HasOne<Plan>()
                .WithMany()
                .HasForeignKey(c => c.IdPlan)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Curso>(entity =>
            {
                entity.HasOne<Materia>()
                      .WithMany()
                      .HasForeignKey(c => c.IdMateria)
                      .OnDelete(DeleteBehavior.Restrict);
                entity.HasOne<Comision>()
                      .WithMany()
                      .HasForeignKey(c => c.IdComision)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<Persona>()
                .HasOne<Plan>()
                .WithMany()
                .HasForeignKey(p => p.IdPlan)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<AlumnoInscripcion>(entity =>
            {
                entity.HasOne<Persona>()
                      .WithMany()
                      .HasForeignKey(ai => ai.IdAlumno)
                      .OnDelete(DeleteBehavior.Restrict);
                entity.HasOne<Curso>()
                      .WithMany()
                      .HasForeignKey(ai => ai.IdCurso)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<DocenteCurso>(entity =>
            {
                entity.HasOne<Curso>()
                      .WithMany()
                      .HasForeignKey(dc => dc.IdCurso)
                      .OnDelete(DeleteBehavior.Restrict);
                entity.HasOne<Persona>()
                      .WithMany()
                      .HasForeignKey(dc => dc.IdDocente)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.HasOne<Persona>()
                      .WithMany()
                      .HasForeignKey(u => u.IdPersona)
                      .OnDelete(DeleteBehavior.Restrict);
                entity.Property(u => u.NombreUsuario).HasMaxLength(50);
                entity.HasIndex(u => u.NombreUsuario).IsUnique();
                entity.HasIndex(u => u.IdPersona).IsUnique();
            });

            modelBuilder.Entity<ModuloUsuario>(entity =>
            {
                entity.HasOne<Modulo>()
                      .WithMany()
                      .HasForeignKey(mu => mu.IdModulo)
                      .OnDelete(DeleteBehavior.Restrict);
                entity.HasOne<Usuario>()
                      .WithMany()
                      .HasForeignKey(mu => mu.IdUsuario)
                      .OnDelete(DeleteBehavior.Restrict);
            });
        }
    }
}
