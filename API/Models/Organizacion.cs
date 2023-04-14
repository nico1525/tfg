using Microsoft.EntityFrameworkCore;
using Mysqlx.Crud;
using System.ComponentModel.DataAnnotations;
using System.Net.Mail;

namespace API.Models
{
    public class Organizacion
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "El campo Nombre de Organización es obligatorio")]
        public string? NombreOrg { get; set; }

        [Required(ErrorMessage = "El campo Email es obligatorio")]
        [EmailAddress(ErrorMessage = "Debe introducir un Email válido")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "El campo Dirección es obligatorio")]
        public string? Direccion { get; set; }

        [Required(ErrorMessage = "El campo Contraseña es obligatorio")]
        public string? Contraseña { get; set; }
            
    }
}
