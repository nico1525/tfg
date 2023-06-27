using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace API.Models.Consumos
{
    public class MaquinariaConsumo
    {
        [Key]
        public int Id { get; set; }
        public int CantidadCombustible { get; set; }
        public string? TipoCombustible { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public float Consumo { get; set; }
        public int MaquinariaId { get; set; }
        [ForeignKey("MaquinariaId")]
        public Maquinaria Maquinaria { get; set; }

    }

    public class MaquinariaConsumoDTO
    {
        public int Id { get; set; }
        public int CantidadCombustible { get; set; }
        public string? TipoCombustible { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public float Consumo { get; set; }
        public int MaquinariaId { get; set; }
    }

    public class MaquinariaConsumoCreateDTO
    {

        [Required(ErrorMessage = "La cantidad de combustible es obligatoria")]
        public int CantidadCombustible { get; set; }

        [Required(ErrorMessage = "El tipo de combustible es obligatorio")]
        [RegularExpression("gasoleoB|E5|E85|E100|B7|B20|B30|B100|E10|B10", ErrorMessage = "El tipo de  combustible es incorrecto")]
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
        public int CantidadCombustible { get; set; }

        [RegularExpression("E5|E85|E100|B7|B20|B30|B100|gasoleoB|E10|B10", ErrorMessage = "El tipo de  combustible es incorrecto")]
        public string? TipoCombustible { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
    }
}
