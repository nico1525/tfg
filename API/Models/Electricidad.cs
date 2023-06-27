using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace API.Models
{
    public class Electricidad
    {
        [Key]
        public int Id { get; set; }
        public string? Edificio { get; set; }
        public string? Dispositivo { get; set; }
        public string? Descripcion { get; set; }
        public int OrganizacionId { get; set; }
        [ForeignKey("OrganizacionId")]
        public Organizacion Organizacion { get; set; }
    }

    public class ElectricidadCreateDTO
    {
        public string? Edificio { get; set; }

        [Required(ErrorMessage = "El nombre del dispositivo es obligatorio")]
        public string? Dispositivo { get; set; }
        public string? Descripcion { get; set; }
    }

    public class ElectricidadDTO
    {
        public int Id { get; set; }
        public string? Edificio { get; set; }
        public string? Dispositivo { get; set; }
        public string? Descripcion { get; set; }

    }

    public class ElectricidadModifyDTO
    {
        public string? Edificio { get; set; }
        public string? Dispositivo { get; set; }
        public string? Descripcion { get; set; }

    }
}
