using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using API.Enumerables;

namespace API.Models
{
    public class Vehiculo
    {
        [Key]
        public int Id { get; set; }
        public string? Edificio { get; set; }
        public string? Matricula { get; set; }
        public string? CategoriaVehiculo { get; set; }
        public string? TipoCombustible { get; set; }

        [ForeignKey("Organizacion")]
        public int OrganizacionId { get; set; }
        [JsonIgnore]
        public Organizacion? OrganizacionRef { get; set; }
    }

    public class VehiculoDTO
    {
        public int Id { get; set; }
        public string? Edificio { get; set; }
        public string? Matricula { get; set; }
        public string? CategoriaVehiculo { get; set; }
        public string? TipoCombustible { get; set; }
    }

    public class VehiculoCreateDTO
    {
        public string? Edificio { get; set; }

        [Required(ErrorMessage = "La matrícula del vehículo es obligatoria")]
        public string? Matricula { get; set; }

        [Required(ErrorMessage = "La categoría del vehículo es obligatoria")]
        [RegularExpression("M1|N1|N2|N3|M2|M3|L", ErrorMessage = "la categoría del vehículo es incorrecta")]
        public string? CategoriaVehiculo { get; set; }

        [Required(ErrorMessage = "El tipo de combustible del vehículo es obligatorio")]
        [RegularExpression("E5|E10|E85|E100|E100|B7|B10|B20|B30|B100|LPG|CNG", ErrorMessage = "El Tipo de combustible es incorrecto")]
        public string? TipoCombustible { get; set; }
    }

    public class VehiculoModifyDTO
    {
        public string? Edificio { get; set; }
        public string? Matricula { get; set; }
    }
}
