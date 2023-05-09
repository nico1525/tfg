using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models.Master_Tables
{
    public class FactorEmisionVehiculo
    {
        public string Tipo { get; set; } = null!;
        public string? M1 { get; set; }
        public string? N1 { get; set; }
        public string? N2N3M2M3 { get; set; }
        public string? L { get; set; }
    }
}
