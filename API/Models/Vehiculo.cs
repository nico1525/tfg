using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using API.Enumerables;
using Microsoft.AspNetCore.Components;

namespace API.Models
{
    public class Vehiculo
    {
        [Key]
        public int Id { get; set; }
        public string? Edificio { get; set; }
        public string? Matricula { get; set; }
        public CategoriaVehiculo CategoriaVehiculo { get; set; }
        public CombustibleVehiculo TipoCombustible { get; set; }
        [ForeignKey("Organizacion")]
        public int OrganizacionId { get; set; }
        public Organizacion? OrganizacionRef { get; set; }

    }
}
