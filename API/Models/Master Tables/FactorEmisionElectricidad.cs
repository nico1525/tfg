namespace API.Models.Master_Tables
{
    public class FactorEmisionElectricidad
    {
        public int Id { get; set; } 
        public string Comercializadora { get; set; } = null!;
        public string? factor { get; set; }
        
    }
}
