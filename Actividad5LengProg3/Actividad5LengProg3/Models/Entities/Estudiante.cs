using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Actividad5LengProg3.Models.Entities
{
    public enum Sexo { Masculino = 1, Femenino = 2, Otro = 3 }
    public enum Turno { Mañana = 1, Tarde = 2, Noche = 3 }

    public class Estudiante : IValidatableObject
    {
        [Key]
        [StringLength(20)]
        public string Matricula { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        public string NombreCompleto { get; set; } = string.Empty;

        [Required]
        [ForeignKey(nameof(Carrera))]
        public int CarreraId { get; set; }

        [Required]
        [ForeignKey(nameof(Recinto))]
        public int RecintoId { get; set; }

        [Required]
        [EmailAddress]
        [StringLength(100)]
        public string CorreoInstitucional { get; set; } = string.Empty;

        [Required]
        [StringLength(20)]
        public string Celular { get; set; } = string.Empty;

        [Required]
        [StringLength(20)]
        public string Telefono { get; set; } = string.Empty;

        [Required]
        [StringLength(200)]
        public string Direccion { get; set; } = string.Empty;

        [Required]
        [DataType(DataType.Date)]
        public DateTime FechaNacimiento { get; set; }

        [Required]
        public Sexo Genero { get; set; }

        [Required]
        public Turno TurnoEstudiante { get; set; }

        public bool Becado { get; set; }

        [Range(0, 100)]
        public int? PorcentajeBeca { get; set; }

        // Navegación
        public virtual Carrera? Carrera { get; set; }
        public virtual Recinto? Recinto { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (Becado && (PorcentajeBeca is null || PorcentajeBeca < 0 || PorcentajeBeca > 100))
                yield return new ValidationResult("Debe indicar un porcentaje entre 0 y 100.", new[] { nameof(PorcentajeBeca) });
        }
    }
}