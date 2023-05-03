using API.Helpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Mysqlx.Crud;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Net.Mail;

namespace API.Models
{
    public class Organizacion : IOrganizacion
    {
        [Key]
        public int Id { get; set; }
        public string? NombreOrg { get; set; }
        [EmailAddress]
        public string? Email { get; set; }
        public string? Direccion { get; set; }
        public string? Contraseña { get; set; }
        public Role Role { get; set; } = Role.User;
    }

    public class OrganizacionDTO : IOrganizacion
    {
        public string? NombreOrg { get; set; }
        public string? Email { get; set; }
    }

    public class OrganizacionCreateDTO : IOrganizacion
    {
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

    public interface IOrganizacion
    {
    }
}
