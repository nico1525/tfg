using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace API.Models.Consumos
{
    public class EmisionesFugitivasConsumo
    {
        [Key]
        public int Id { get; set; }
        public string? Edificio { get; set; }
        public string? Gas { get; set; }
        public int Recarga { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public float Consumo { get; set; }
        [ForeignKey("EmisionesFugitivas")]
        public int EmisionesFugitivasId { get; set; }
        [JsonIgnore]
        public Transporte? EmisionesFugitivasRef { get; set; }
    }

    public class EmisionesFugitivasConsumoCreateDTO
    {
        public string? Edificio { get; set; }

        [RegularExpression("E5|qq", ErrorMessage = "El Tipo de Gas es incorrecto")] ///////////////////////////////////////////////////////////////////////////////////
        public string? Gas { get; set; }

        [Required(ErrorMessage = "La cantidad de recarga o uso del gas es obligatorio")]
        public int Recarga { get; set; }

        [Required(ErrorMessage = "La fecha de inicio es obligatoria")]
        public DateTime FechaInicio { get; set; }

        [Required(ErrorMessage = "La fecha de fin es obligatoria")]
        public DateTime FechaFin { get; set; }

        [Required(ErrorMessage = "El Id del equipo al que asignarle un consumo es obligatorio")]
        public int EmisionesFugitivasId { get; set; }
    }

    public class EmisionesFugitivasConsumoDTO
    {
        public int Id { get; set; }
        public string? Edificio { get; set; }
        public string? Gas { get; set; }
        public int Recarga { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public float Consumo { get; set; }
        public int EmisionesFugitivasId { get; set; }
    }

    public class EmisionesFugitivasConsumoModifyDTO
    {
        public string? Edificio { get; set; }
        public int Recarga { get; set; }

        [RegularExpression("E5|qq", ErrorMessage = "El Tipo de Gas es incorrecto")] ///////////////////////////////////////////////////////////////////////////////////
        public string? Gas { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
    }
}
