using System.ComponentModel.DataAnnotations;

namespace API.Models
{
    public class Organizacion
    {
        public int Id { get; set; }
        [Required]
        public string? NombreOrg { get; set; }
        [Required]
        [EmailAddress]
        public string? Email { get; set; }
        [Required]
        public string? Direccion { get; set; }
            
    }
}
