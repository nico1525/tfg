using System.ComponentModel.DataAnnotations;

namespace API.Models.Autentificacion
{
    public class LoginRequest
    {
        [Required]
        [EmailAddress]
        public string? Email { get; set; }
        [Required]
        public string? Contraseña { get; set; }
    }
}
