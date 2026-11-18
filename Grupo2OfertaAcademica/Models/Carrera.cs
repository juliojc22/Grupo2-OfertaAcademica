using System.ComponentModel.DataAnnotations;

namespace Grupo2OfertaAcademica.Models
{
    public class Carrera
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre de la carrera es obligatorio.")]
        [StringLength(150, ErrorMessage = "El nombre no puede superar los 150 caracteres.")]
        [Display(Name = "Nombre de la carrera")]
        public string Nombre { get; set; } = string.Empty;

        [Required(ErrorMessage = "El código es obligatorio.")]
        [StringLength(20, ErrorMessage = "El código no puede superar los 20 caracteres.")]
        [Display(Name = "Código de carrera")]
        public string Codigo { get; set; } = string.Empty;

        [Required(ErrorMessage = "La duración es obligatoria.")]
        [Range(1, 12, ErrorMessage = "La duración debe estar entre 1 y 12 semestres.")]
        [Display(Name = "Duración en semestres")]
        public int DuracionSemestres { get; set; }
    }
}