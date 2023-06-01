using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace API.Models.Consumos
{
    public class InstalacionesFijasConsumo
    {
        [Key]
        public int Id { get; set; }
        public string? Edificio { get; set; }
        public int CantidadCombustible { get; set; }
        public string? TipoCombustible { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public float Consumo { get; set; }
        [ForeignKey("InstalacionesFijas")]
        public int InstalacionesFijasId { get; set; }
    }

    public class InstalacionesFijasConsumoDTO
    {
        public int Id { get; set; }
        public string? Edificio { get; set; }
        public int CantidadCombustible { get; set; }
        public string? TipoCombustible { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public float Consumo { get; set; }
        public int InstalacionesFijasId { get; set; }
    }

    public class InstalacionesFijasConsumoCreateDTO
    {
        public string? Edificio { get; set; }

        [Required(ErrorMessage = "La cantidad de combustible es obligatoria")]
        public int CantidadCombustible { get; set; }

        [Required(ErrorMessage = "El tipo de combustible es obligatorio")]
        public string? TipoCombustible { get; set; }

        [Required(ErrorMessage = "La fecha de inicio es obligatoria")]
        public DateTime FechaInicio { get; set; }

        [Required(ErrorMessage = "La fecha de fin es obligatoria")]
        public DateTime FechaFin { get; set; }

        [Required(ErrorMessage = "El Id de la instalación fija a la que asignarle un consumo es obligatorio")]
        public int InstalacionesFijasId { get; set; }
    }

    public class InstalacionesFijasConsumoModifyDTO
    {
        public string? Edificio { get; set; }
        public int CantidadCombustible { get; set; }
        public string? TipoCombustible { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
    }
}
