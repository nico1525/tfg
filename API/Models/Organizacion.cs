using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Mysqlx.Crud;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Net.Mail;

namespace API.Models
{
    public class Organizacion
    {
        [Key]
        public int Id { get; set; }
        public string? NombreOrg { get; set; }
        [EmailAddress]
        public string? Email { get; set; }
        public string? Direccion { get; set; }
        public string? Contraseña { get; set; }
        public bool IsAdmin { get; set; } = false;
        //public virtual ICollection<Dispositivo> ListaDispositivos { get; set;} 
            
    }

}
