using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models.Consumos
{
    public class OtrosConsumos
    {
        [Key]
        public int Id { get; set; }
        public string? Edificio { get; set; }
        public string? Nombre { get; set; }
        public string? CategoriaActividad{ get; set; }
        public int CantidadConsumo { get; set; }
        public float FactorEmision { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public float Consumo { get; set; }
        [ForeignKey("Organizacion")]
        public int OrganizacionId { get; set; }
    }

    public class OtrosConsumosDTO
    {
        public int Id { get; set; }
        public string? Edificio { get; set; }
        public string? Nombre { get; set; }
        public string? CategoriaActividad { get; set; }
        public int CantidadConsumo { get; set; }
        public float FactorEmision { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public float Consumo { get; set; }
        public int OrganizacionId { get; set; }
    }

    public class OtrosConsumosCreateDTO
    {
        public string? Edificio { get; set; }
        public string? Nombre { get; set; }
        public string? CategoriaActividad { get; set; }

        [Required(ErrorMessage = "La cantidad consumida es obligatoria")]
        public int CantidadConsumo { get; set; }

        [Required(ErrorMessage = "El factor de emisión en CO2e es obligatorio")]
        public float FactorEmision { get; set; }

        [Required(ErrorMessage = "La fecha de inicio es obligatoria")]
        public DateTime FechaInicio { get; set; }

        [Required(ErrorMessage = "La fecha de fin es obligatoria")]
        public DateTime FechaFin { get; set; }

    }

    public class OtrosConsumosModifyDTO
    {
        public string? Edificio { get; set; }
        public string? Nombre { get; set; }
        public string? CategoriaActividad { get; set; }
        public int CantidadConsumo { get; set; }
        public float FactorEmision { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
    }
}
