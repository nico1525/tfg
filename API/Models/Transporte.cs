using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace API.Models
{
    public class Transporte
    {
        [Key]
        public int Id { get; set; }
        public string? Edificio { get; set; }
        public string? TipoTransporte { get; set; }
        public string? CombustibleTransporte { get; set; }
        [ForeignKey("Organizacion")]
        public int OrganizacionId { get; set; }
        public Organizacion? OrganizacionRef { get; set; }
    }
    public class TransporteCreateDTO
    {
        public string? Edificio { get; set; }

        [Required(ErrorMessage = "El tipo de transporte es obligatorio")]
        [RegularExpression("ferrocarril|maritimo|aereo", ErrorMessage = "El tipo de transporte es incorrecto")]
        public string? TipoTransporte { get; set; }

        [Required(ErrorMessage = "El combustible del transporte es obligatorio")]
        [RegularExpression("gasoleo|fueloleo|queroseno|gasolinaaviacion", ErrorMessage = "El combustible del transporte es incorrecto")]
        public string? CombustibleTransporte { get; set; }
    }
    public class TransporteDTO
    {
        public int Id { get; set; }
        public string? Edificio { get; set; }
        public string? TipoTransporte { get; set; }
        public string? CombustibleTransporte { get; set; }
    }
    public class TransporteModifyDTO
    {
        public string? Edificio { get; set; }
    }
}
