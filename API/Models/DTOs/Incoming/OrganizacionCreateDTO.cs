using System.ComponentModel.DataAnnotations;

namespace API.Models.DTOs.Incoming
{
    public class OrganizacionCreateDTO
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "El Nombre de Organización es obligatorio")]
        public string? NombreOrg { get; set; }

        [Required(ErrorMessage = "El Email es obligatorio")]
        [EmailAddress(ErrorMessage = "Debe introducir un Email válido")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "La Dirección es obligatoria")]
        public string? Direccion { get; set; }

        [Required(ErrorMessage = "El Contraseña es obligatorio")]
        public string? Contraseña { get; set; }
    }
}
