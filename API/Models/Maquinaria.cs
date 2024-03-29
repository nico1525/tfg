﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models
{
    public class Maquinaria
    {
        [Key]
        public int Id { get; set; }
        public string? Edificio { get; set; }
        public string? Nombre { get; set; }
        public string? TipoMaquinaria { get; set; }
        public int OrganizacionId { get; set; }
        [ForeignKey("OrganizacionId")]
        public Organizacion Organizacion { get; set; }
    }

    public class MaquinariaDTO
    {
        public int Id { get; set; }
        public string? Edificio { get; set; }
        public string? Nombre { get; set; }
        public string? TipoMaquinaria { get; set; }
    }

    public class MaquinariaCreateDTO
    {
        public string? Edificio { get; set; }
        public string? Nombre { get; set; }

        [Required(ErrorMessage = "El tipo de maquinaria es obligatorio")]
        [RegularExpression("agricola|forestal|industrial", ErrorMessage = "El tipo de maquinaria es incorrecto")]
        public string? TipoMaquinaria { get; set; }
    }

    public class MaquinariaModifyDTO
    {
        public string? Edificio { get; set; }
        public string? Nombre { get; set; }
    }
}
