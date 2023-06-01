using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace API.Models
{
    public class Vehiculo
    {
        [Key]
        public int Id { get; set; }
        public string? Edificio { get; set; }
        public string? Matricula { get; set; }
        public string? CategoriaVehiculo { get; set; }

        [ForeignKey("Organizacion")]
        public int OrganizacionId { get; set; }
    }

    public class VehiculoDTO
    {
        public int Id { get; set; }
        public string? Edificio { get; set; }
        public string? Matricula { get; set; }
        public string? CategoriaVehiculo { get; set; }
    }

    public class VehiculoCreateDTO
    {
        public string? Edificio { get; set; }

        [Required(ErrorMessage = "La matrícula del vehículo es obligatoria")]
        public string? Matricula { get; set; }

        [Required(ErrorMessage = "La categoría del vehículo es obligatoria")]
        [RegularExpression("M1|N1|N2|N3|M2|M3|L", ErrorMessage = "la categoría del vehículo es incorrecta")]
        public string? CategoriaVehiculo { get; set; }
    }

    public class VehiculoModifyDTO
    {
        public string? Edificio { get; set; }
        public string? Matricula { get; set; }
    }
}
