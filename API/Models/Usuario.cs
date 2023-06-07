using API.Helpers;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace API.Models
{
    public class Usuario
    {
        [Key]
        [JsonIgnore]
        public int Id { get; set; }
        public string? NombreApellidos { get; set; }
        [EmailAddress]
        public string? Email { get; set; }
        public string? Contraseña { get; set; }
        public int OrganizacionId { get; set; }
        public Role Role { get; set; } = Role.User;
    }

    public class UsuarioDTO
    {
        public int Id { get; set; }
        public string? NombreApellidos { get; set; }
        public string? Email { get; set; }
        public string? Contraseña { get; set; }
        public Role Role { get; set; }

    }

    public class UsuarioCreateDTO
    {
        [Required(ErrorMessage = "El Nombre y apellidos son obligatorios")]
        public string? NombreApellidos { get; set; }

        [Required(ErrorMessage = "El Email es obligatorio")]
        [EmailAddress(ErrorMessage = "Debe introducir un Email válido")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "La Contraseña es obligatoria")]
        public string? Contraseña { get; set; }
    }
    public class UsuarioModifyDTO
    {
        public string? NombreApellidos { get; set; }

        [EmailAddress(ErrorMessage = "Debe introducir un Email válido")]
        public string? Email { get; set; }
        public string? Contraseña { get; set; }
        public Role Role { get; set; }

    }
}
