using API.Enumerables;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace API.Models.Consumos
{
    public class VehiculoConsumo
    {
        [Key]
        public int Id { get; set; }
        public string? Edificio { get; set; }
        public int CantidadCombustible { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public float Consumo { get; set; }
        [ForeignKey("Vehiculo")]
        public int VehiculoId { get; set; }
    }
}
