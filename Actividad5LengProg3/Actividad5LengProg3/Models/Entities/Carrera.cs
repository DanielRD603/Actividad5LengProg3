using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Actividad5LengProg3.Models.Entities
{
    public class Carrera
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Codigo { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio")]
        [StringLength(100)]
        public string Nombre { get; set; } = string.Empty;

        [Required(ErrorMessage = "La cantidad de créditos es obligatoria")]
        public int CantidadCreditos { get; set; }

        [Required(ErrorMessage = "La cantidad de materias es obligatoria")]
        public int CantidadMaterias { get; set; }

        // Navegación
        public virtual ICollection<Estudiante> Estudiantes { get; set; } = new List<Estudiante>();
    }
}