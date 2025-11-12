using Microsoft.EntityFrameworkCore;
using Actividad5LengProg3.Models.Entities;

namespace Actividad5LengProg3.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Estudiante> Estudiantes { get; set; }
        public DbSet<Carrera> Carreras { get; set; }
        public DbSet<Recinto> Recintos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configuración de Estudiante
            modelBuilder.Entity<Estudiante>(entity =>
            {
                entity.HasKey(e => e.Matricula);

                entity.HasOne(e => e.Carrera)
                    .WithMany(c => c.Estudiantes)
                    .HasForeignKey(e => e.CarreraId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(e => e.Recinto)
                    .WithMany(r => r.Estudiantes)
                    .HasForeignKey(e => e.RecintoId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasIndex(e => e.CorreoInstitucional).IsUnique();
            });

            // Datos iniciales de Carreras
            modelBuilder.Entity<Carrera>().HasData(
                new Carrera { Codigo = 1, Nombre = "Administración de Empresas", CantidadCreditos = 150, CantidadMaterias = 40 },
                new Carrera { Codigo = 2, Nombre = "Contabilidad", CantidadCreditos = 145, CantidadMaterias = 38 },
                new Carrera { Codigo = 3, Nombre = "Derecho", CantidadCreditos = 160, CantidadMaterias = 42 },
                new Carrera { Codigo = 4, Nombre = "Psicología Clínica", CantidadCreditos = 155, CantidadMaterias = 41 },
                new Carrera { Codigo = 5, Nombre = "Orientación Escolar", CantidadCreditos = 148, CantidadMaterias = 39 },
                new Carrera { Codigo = 6, Nombre = "Administración y Supervisión Escolar", CantidadCreditos = 150, CantidadMaterias = 40 },
                new Carrera { Codigo = 7, Nombre = "Enfermería", CantidadCreditos = 165, CantidadMaterias = 44 },
                new Carrera { Codigo = 8, Nombre = "Odontología", CantidadCreditos = 180, CantidadMaterias = 48 },
                new Carrera { Codigo = 9, Nombre = "Ingeniería", CantidadCreditos = 170, CantidadMaterias = 46 },
                new Carrera { Codigo = 10, Nombre = "Ciencias Naturales", CantidadCreditos = 152, CantidadMaterias = 40 }
            );

            // Datos iniciales de Recintos
            modelBuilder.Entity<Recinto>().HasData(
                new Recinto { Codigo = 1, Nombre = "Herrera", Direccion = "Santo Domingo Oeste" },
                new Recinto { Codigo = 2, Nombre = "Metropolitano", Direccion = "Santo Domingo Este" },
                new Recinto { Codigo = 3, Nombre = "Barahona", Direccion = "Barahona" }
            );
        }
    }
}