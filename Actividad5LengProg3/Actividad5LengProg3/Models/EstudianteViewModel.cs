using System.ComponentModel.DataAnnotations;
using Actividad5LengProg3.Models.Entities;

namespace Actividad5LengProg3.Models.ViewModels
{
    public class EstudianteViewModel : IValidatableObject
    {
        [Required(ErrorMessage = "La matrícula es obligatoria")]
        [StringLength(20)]
        [Display(Name = "Matrícula")]
        public string Matricula { get; set; } = string.Empty;

        [Required(ErrorMessage = "El nombre completo es obligatorio")]
        [StringLength(100)]
        [Display(Name = "Nombre completo")]
        public string NombreCompleto { get; set; } = string.Empty;

        [Required(ErrorMessage = "La carrera es obligatoria")]
        [Display(Name = "Carrera")]
        public int CarreraId { get; set; }

        [Required(ErrorMessage = "El recinto es obligatorio")]
        [Display(Name = "Recinto")]
        public int RecintoId { get; set; }

        [Required(ErrorMessage = "El correo institucional es obligatorio")]
        [EmailAddress(ErrorMessage = "Ingrese un correo electrónico válido")]
        [StringLength(100)]
        [Display(Name = "Correo institucional")]
        public string CorreoInstitucional { get; set; } = string.Empty;

        [Required(ErrorMessage = "El celular es obligatorio")]
        [RegularExpression(@"^(\+?1[ \-]?)?(809|829|849)[ \-]?\d{3}[ \-]?\d{4}$",
            ErrorMessage = "Ingrese un celular dominicano válido (809/829/849).")]
        [Display(Name = "Celular")]
        public string Celular { get; set; } = string.Empty;

        [Required(ErrorMessage = "El teléfono es obligatorio")]
        [RegularExpression(@"^(\+?1[ \-]?)?(809|829|849)[ \-]?\d{3}[ \-]?\d{4}$",
            ErrorMessage = "Ingrese un teléfono dominicano válido (809/829/849).")]
        [Display(Name = "Teléfono")]
        public string Telefono { get; set; } = string.Empty;

        [Required(ErrorMessage = "La dirección es obligatoria")]
        [StringLength(200)]
        [Display(Name = "Dirección")]
        public string Direccion { get; set; } = string.Empty;

        [Required(ErrorMessage = "La fecha de nacimiento es obligatoria")]
        [DataType(DataType.Date)]
        [Display(Name = "Fecha de nacimiento")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime FechaNacimiento { get; set; }

        [Required(ErrorMessage = "El género es obligatorio")]
        [Display(Name = "Género")]
        public Sexo Genero { get; set; }

        [Required(ErrorMessage = "El turno es obligatorio")]
        [Display(Name = "Turno")]
        public Turno Turno { get; set; }

        [Display(Name = "¿Está becado?")]
        public bool Becado { get; set; }

        [Range(0, 100, ErrorMessage = "El porcentaje debe estar entre 0 y 100")]
        [Display(Name = "Porcentaje de beca")]
        public int? PorcentajeBeca { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (Becado && (PorcentajeBeca is null || PorcentajeBeca < 0 || PorcentajeBeca > 100))
                yield return new ValidationResult(
                    "Debe indicar un porcentaje entre 0 y 100 para estudiantes becados.",
                    new[] { nameof(PorcentajeBeca) }
                );

            if (!Becado && PorcentajeBeca.HasValue && PorcentajeBeca > 0)
                yield return new ValidationResult(
                    "El porcentaje de beca debe ser 0 si el estudiante no está becado.",
                    new[] { nameof(PorcentajeBeca) }
                );
        }
    }
}