using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace API.Models.Consumos
{
    public class ElectricidadConsumo
    {
        [Key]
        public int Id { get; set; }
        public int Kwh { get; set; }
        public int ComercializadoraId { get; set; }
        public string? Comercializadora { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public float Consumo { get; set; }
        public int ElectricidadId { get; set; }
        [ForeignKey("ElectricidadId")]
        public Electricidad Electricidad { get; set; }

    }

    public class ElectricidadConsumoCreateDTO
    {

        [Required(ErrorMessage = "La cantidad de Kilovatios es obligatoria")]
        public int Kwh { get; set; }
        [Required(ErrorMessage = "El identificador de la comercializadora de electricidad es obligatorio")]
        public int ComercializadoraId { get; set; }

        [Required(ErrorMessage = "La fecha de inicio es obligatoria")]
        public DateTime FechaInicio { get; set; }

        [Required(ErrorMessage = "La fecha de fin es obligatoria")]
        public DateTime FechaFin { get; set; }

        [Required(ErrorMessage = "El Id del dispositivo al que asignarle un consumo es obligatorio")]
        public int ElectricidadId { get; set; }
    }

    public class ElectricidadConsumoDTO
    {
        public int Id { get; set; }
        public int Kwh { get; set; }
        public int ComercializadoraId { get; set; }
        public string? Comercializadora { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public float Consumo { get; set; }
        public int ElectricidadId { get; set; }
    }

    public class ElectricidadConsumoModifyDTO
    {
        public int Kwh { get; set; }
        public int ComercializadoraId { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
    }
}
