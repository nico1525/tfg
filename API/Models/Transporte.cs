using API.Enumerables;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace API.Models
{
    public class Transporte
    {
        [Key]
        public int Id { get; set; }
        public string? Sede { get; set; }
        public TipoTransporte TipoTransporte { get; set; }
        public CombustibleTransporte CombustibleTransporte { get; set; }
        [ForeignKey("Organizacion")]
        public int OrganizacionId { get; set; }
    }
}
