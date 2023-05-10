using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace API.Models.Consumos
{
    public class MaquinariaConsumo
    {
        [Key]
        public int Id { get; set; }
        public string? Edificio { get; set; }
        public int CantidadCombustible { get; set; }
        public string? TipoCombustible { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public float Consumo { get; set; }
        [ForeignKey("Maquinaria")]
        public int MaquinariaId { get; set; }
        [JsonIgnore]
        public Maquinaria? MaquinariaRef { get; set; }
    }

    public class MaquinariaConsumoDTO
    {
        public int Id { get; set; }
        public string? Edificio { get; set; }
        public int CantidadCombustible { get; set; }
        public string? TipoCombustible { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public float Consumo { get; set; }
        public int MaquinariaId { get; set; }
    }

    public class MaquinariaConsumoCreateDTO
    {
        public string? Edificio { get; set; }

        [Required(ErrorMessage = "La cantidad de combustible es obligatoria")]
        public int CantidadCombustible { get; set; }

        [Required(ErrorMessage = "El tipo de combustible es obligatorio")]
        [RegularExpression("gasoleoB|E5|E10|E85|E100|E100|B7|B10|B20|B30|B100", ErrorMessage = "El tipo de  combustible es incorrecto")]
        public string? TipoCombustible { get; set; }

        [Required(ErrorMessage = "La fecha de inicio es obligatoria")]
        public DateTime FechaInicio { get; set; }

        [Required(ErrorMessage = "La fecha de fin es obligatoria")]
        public DateTime FechaFin { get; set; }

        [Required(ErrorMessage = "El Id de la maquinaria a la que asignarle un consumo es obligatorio")]
        public int MaquinariaId { get; set; }
    }

    public class MaquinariaConsumoModifyDTO
    {
        public string? Edificio { get; set; }
        public int CantidadCombustible { get; set; }

        [RegularExpression("gasoleoB|E5|E10|E85|E100|E100|B7|B10|B20|B30|B100", ErrorMessage = "El tipo de  combustible es incorrecto")]
        public string? TipoCombustible { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
    }
}
