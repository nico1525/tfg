namespace API.Models.Master_Tables
{
    public class FactorEmisionTransporte
    {
        public string Tipo { get; set; } = null!;
        public string? ferrocarril { get; set; }
        public string? maritimo { get; set; }
        public string? aereo { get; set; }   
    }
}
