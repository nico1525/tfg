using API.Enumerables;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace API.Models
{
    public class Dispositivo
    {
        [Key]
        public int Id { get; set; }
        [NotNull]
        public string? NombreDispositivo { get; set; }
        [NotNull]
        public string? NumSerie { get; set; }
        [NotNull]
        public string? Edificio { get; set; }
        [NotNull]
        public CombustibleVehiculo TipoCombustible { get; set; }
        [ForeignKey("Organizacion")]
        public int OrganizacionId { get; set; }
    }
}
