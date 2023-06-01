using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace API.Models.Consumos
{
    public class TransporteConsumo
    {
        [Key]
        public int Id { get; set; }
        public string? Edificio { get; set; }
        public int CantidadCombustible { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public float Consumo { get; set; }
        [ForeignKey("Transporte")]
        public int TransporteId { get; set; }
    }

    public class TransporteConsumoCreateDTO
    {
        public string? Edificio { get; set; }
        [Required(ErrorMessage = "La cantidad de combustible es obligatoria")]
        public int CantidadCombustible { get; set; }
        [Required(ErrorMessage = "La fecha de inicio es obligatoria")]
        public DateTime FechaInicio { get; set; }
        [Required(ErrorMessage = "La fecha de fin es obligatoria")]
        public DateTime FechaFin { get; set; }
        [Required(ErrorMessage = "El Id del transporte al que asignarle un consumo es obligatorio")]
        public int TransporteId { get; set; }
    }

    public class TransporteConsumoDTO
    {
        public int Id { get; set; }
        public string? Edificio { get; set; }
        public int CantidadCombustible { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public float Consumo { get; set; }
        public int TransporteId { get; set; }
    }
    public class TransporteConsumoModifyDTO
    {
        public string? Edificio { get; set; }
        public int CantidadCombustible { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
    }
}
