using System.ComponentModel.DataAnnotations;

namespace Grupo2OfertaAcademica.Models
{
    public class Facultad
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre de la facultad es obligatorio.")]
        [StringLength(150)]
        [Display(Name = "Nombre de la facultad")]
        public string Nombre { get; set; } = string.Empty;

        [StringLength(150)]
        [Display(Name = "Decano")]
        public string? Decano { get; set; }

        public ICollection<Carrera> Carreras { get; set; } = new List<Carrera>();
    }
}