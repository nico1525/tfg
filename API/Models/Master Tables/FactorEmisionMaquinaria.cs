namespace API.Models.Master_Tables
{
    public class FactorEmisionMaquinaria
    {
        public string Tipo { get; set; } = null!;
        public string? agricola { get; set; }
        public string? forestal { get; set; }
        public string? industrial { get; set; }
    }
}
