using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace API.Models
{
    public class EmisionesFugitivas
    {
        [Key]
        public int Id { get; set; }
        public string? Edificio { get; set; }
        public string? NombreEquipo { get; set; }

        [ForeignKey("Organizacion")]
        public int OrganizacionId { get; set; }
        public Organizacion? OrganizacionRef { get; set; }
    }

    public class EmisionesFugitivasCreateDTO
    {
        public string? Edificio { get; set; }

        [Required(ErrorMessage = "El nombre del equipo de refrigeración/climatización o fuga es obligatorio")]
        public string? NombreEquipo { get; set; }
    }

    public class EmisionesFugitivasDTO
    {
        public int Id { get; set; }
        public string? Edificio { get; set; }
        public string? NombreEquipo { get; set; }
    }

    public class EmisionesFugitivasModifyDTO
    {
        public string? Edificio { get; set; }
        public string? NombreEquipo { get; set; }
    }

}
