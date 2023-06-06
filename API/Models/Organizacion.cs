using API.Helpers;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace API.Models
{
    public class Organizacion
    {
        [Key]
        [JsonIgnore]
        public int Id { get; set; }
        public string? NombreOrg { get; set; }
        [EmailAddress]
        public string? Email { get; set; }
        public string? Direccion { get; set; }
        public string? Contraseña { get; set; }
    }

    public class OrganizacionDTO
    {
        public string? NombreOrg { get; set; }
        public string? Email { get; set; }
        public string? Direccion { get; set; }
        public string? Contraseña { get; set; }

    }

    public class OrganizacionCreateDTO
    {
        [Required(ErrorMessage = "El Nombre de Organización es obligatorio")]
        public string? NombreOrg { get; set; }

        [Required(ErrorMessage = "El Email es obligatorio")]
        [EmailAddress(ErrorMessage = "Debe introducir un Email válido")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "La Dirección es obligatoria")]
        public string? Direccion { get; set; }

        [Required(ErrorMessage = "La Contraseña es obligatoria")]
        public string? Contraseña { get; set; }
    }
    public class OrganizacionModifyDTO
    {
        public string? NombreOrg { get; set; }

        [EmailAddress(ErrorMessage = "Debe introducir un Email válido")]
        public string? Email { get; set; }

        public string? Direccion { get; set; }

        public string? Contraseña { get; set; }
    }
}
