using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace API.Models
{
    public class InstalacionesFijas
    {
        [Key]
        public int Id { get; set; }
        public string? Edificio { get; set; }
        public string? Nombre { get; set; }
        [ForeignKey("Organizacion")]
        public int OrganizacionId { get; set; }
    }

    public class InstalacionesFijasDTO
    {
        public int Id { get; set; }
        public string? Edificio { get; set; }
        public string? Nombre { get; set; }
    }

    public class InstalacionesFijasCreateDTO
    {
        public string? Edificio { get; set; }

        [Required(ErrorMessage = "Debes poner un nombre a la instalación fija que vas a añadir")]
        public string? Nombre { get; set; }
    }

    public class InstalacionesFijasModifyDTO
    {
        public string? Edificio { get; set; }
        public string? Nombre { get; set; }
    }
}
