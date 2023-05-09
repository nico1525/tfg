using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace API.Models.Consumos
{
    public class VehiculoConsumo
    {
        [Key]
        public int Id { get; set; }
        public string? Edificio { get; set; }
        public int CantidadCombustible { get; set; }
        public string? TipoCombustible { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public float Consumo { get; set; }
        [ForeignKey("Vehiculo")]
        public int VehiculoId { get; set; }
        [JsonIgnore]
        public Vehiculo? VehiculoRef { get; set; }

    }
    public class VehiculoConsumoCreateDTO
    { 
        public string? Edificio { get; set; }

        [Required(ErrorMessage = "La cantidad de combustible es obligatoria")]
        public int CantidadCombustible { get; set; }

        [Required(ErrorMessage = "El tipo de combustible del vehículo es obligatorio")]
        [RegularExpression("E5|E10|E85|E100|E100|B7|B10|B20|B30|B100|LPG|CNG", ErrorMessage = "El Tipo de combustible es incorrecto")]
        public string? TipoCombustible { get; set; }

        [Required(ErrorMessage = "La fecha de inicio es obligatoria")]
        public DateTime FechaInicio { get; set; }

        [Required(ErrorMessage = "La fecha de fin es obligatoria")]
        public DateTime FechaFin { get; set; }

        [Required(ErrorMessage = "El Id del vehiculo al que asignarle un consumo es obligatorio")]
        public int VehiculoId { get; set; }
    }
    public class VehiculoConsumoDTO
    {
        public int Id { get; set; }
        public string? Edificio { get; set; }
        public int CantidadCombustible { get; set; }
        public string? TipoCombustible { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public float Consumo { get; set; }
        public int VehiculoId { get; set; }
    }
    public class VehiculoConsumoModifyDTO
    {
        public string? Edificio { get; set; }
        public int CantidadCombustible { get; set; }
        [RegularExpression("E5|E10|E85|E100|E100|B7|B10|B20|B30|B100|LPG|CNG", ErrorMessage = "El Tipo de combustible es incorrecto")]
        public string? TipoCombustible { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
    }
}
