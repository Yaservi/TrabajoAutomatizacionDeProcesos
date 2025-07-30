using Microsoft.EntityFrameworkCore;

namespace ProyectoEscuela.Server.Models
{
    public class AppDBContext: DbContext
    {
        public AppDBContext(DbContextOptions<AppDBContext> options) : base(options) { }
        #region DbSets
        public DbSet<Alumno> Alumnos { get; set; }
        public DbSet<Materia> Materias { get; set; }
        public DbSet<Asistencias> Asistencias { get; set; }
        public DbSet<Calificacion> Calificaciones { get; set; }
        public DbSet<Maestro> Maestros { get; set; }
        #endregion
        #region OnModelCreating
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
           base.OnModelCreating(modelBuilder);
            #region Tables
            modelBuilder.Entity<Alumno>().ToTable("Alumnos");
            modelBuilder.Entity<Asistencias>().ToTable("Asistencias");
            modelBuilder.Entity<Calificacion>().ToTable("Calificaciones");
            modelBuilder.Entity<Maestro>().ToTable("Maestros");
            modelBuilder.Entity<Materia>().ToTable("Materias");
            #endregion
            #region Primary key
            modelBuilder.Entity<Alumno>()
                .HasKey(k => k.Id)
                .HasName("PKAlumno");
            modelBuilder.Entity<Asistencias>()
              .HasKey(k => k.Id)
              .HasName("PKAsistencia");
            modelBuilder.Entity<Calificacion>()
              .HasKey(k => k.Id)
              .HasName("PKCalificacion");
            modelBuilder.Entity<Maestro>()
              .HasKey(k => k.Id)
              .HasName("PKMaestro");
            modelBuilder.Entity<Materia>()
              .HasKey(k => k.Id)
              .HasName("PKMateria");
            #endregion
            #region Properties

            modelBuilder.Entity<Alumno>(entity =>
            {
                entity.Property(k => k.Id).IsRequired();
                entity.Property(k => k.Nombre).HasMaxLength(50);
                entity.Property(k => k.Apellido).HasMaxLength(50);
                entity.Property(k => k.Direccion).HasMaxLength(50);
                entity.Property(k => k.Email).HasMaxLength(50);
                entity.Property(k => k.FechaNacimiento).HasMaxLength(50).IsRequired();
            });

            modelBuilder.Entity<Maestro>(entity =>
            {
                entity.Property(k => k.Id).IsRequired();
                entity.Property(k => k.Nombre).HasMaxLength(50);
                entity.Property(k => k.Apellido).HasMaxLength(50);
                entity.Property(k => k.Direccion).HasMaxLength(50);
                entity.Property(k => k.Email).HasMaxLength(50);
                entity.Property(k => k.FechaNacimiento).HasMaxLength(50).IsRequired();
            });

            modelBuilder.Entity<Calificacion>(entity =>
            {
                entity.Property(k => k.Id).IsRequired();
                entity.Property(k => k.IdAlumno).IsRequired();
                entity.Property(k => k.IdMateria).IsRequired();
            });

            modelBuilder.Entity<Materia>(entity =>
            {
                entity.Property(k => k.Id).IsRequired();
                entity.Property(k => k.MaestroId).IsRequired();
                entity.Property(k => k.NombreMateria).HasMaxLength(150).IsRequired();
                entity.Property(k => k.Descripcion).HasMaxLength(850);
            });

            modelBuilder.Entity<Asistencias>(entity =>
            {
                entity.Property(k => k.Id).IsRequired();
                entity.Property(k => k.MateriaId).IsRequired();
                entity.Property(k => k.AlumnoId).IsRequired();
                entity.Property(k => k.Estado).HasMaxLength(100).IsRequired();
            });
            #endregion
            #region Relacion
            modelBuilder.Entity<Materia>()
                .HasOne(m => m.Maestro)
                .WithMany(ma => ma.Materia)
                .HasForeignKey(m => m.MaestroId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Materia>()
                .HasMany(m => m.Alumno)
                .WithMany(a => a.Materia);

            modelBuilder.Entity<Asistencias>()
                .HasOne(a => a.Alumno)
                .WithMany(al => al.Asistencias)
                .HasForeignKey(a => a.AlumnoId);

            modelBuilder.Entity<Asistencias>()
                .HasOne(a => a.Materia)
                .WithMany(m => m.Asistencias)
                .HasForeignKey(a => a.MateriaId);

            modelBuilder.Entity<Calificacion>()
                .HasOne(c => c.Alumno)
                .WithMany(a => a.Calificacion)
                .HasForeignKey(c => c.IdAlumno);

            modelBuilder.Entity<Calificacion>()
                .HasOne(c => c.Materia)
                .WithMany(m => m.Calificacion)
                .HasForeignKey(c => c.IdMateria);
            #endregion

        }
        #endregion
    }
}
