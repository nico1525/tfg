using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models
{
    public class Maquinaria
    {
        [Key]
        public int Id { get; set; }
        public string? Edificio { get; set; }
        public string? NombreMaquinaria { get; set; }
        public string? TipoMaquinaria { get; set; }
        [ForeignKey("Organizacion")]
        public int OrganizacionId { get; set; }
        public Organizacion? OrganizacionRef { get; set; }
    }

    public class MaquinariaDTO
    {
        public int Id { get; set; }
        public string? Edificio { get; set; }
        public string? NombreMaquinaria { get; set; }
        public string? TipoMaquinaria { get; set; }
    }

    public class MaquinariaCreateDTO
    {
        public string? Edificio { get; set; }
        public string? NombreMaquinaria { get; set; }

        [Required(ErrorMessage = "El tipo de maquinaria es obligatorio")]
        [RegularExpression("agricola|forestal|industrial", ErrorMessage = "El tipo de maquinaria es incorrecto")]
        public string? TipoMaquinaria { get; set; }
    }

    public class MaquinariaModifyDTO
    {
        public string? Edificio { get; set; }
        public string? NombreMaquinaria { get; set; }
    }
}
