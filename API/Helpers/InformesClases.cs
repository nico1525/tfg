namespace API.Helpers
{
    public class ConsumoMes
    {
        public int Mes { get; set; }
        public float Consumo_mes { get; set; }
        public float Combustible_mes { get; set; }
        public string Meses { get; set; }
    }
    public class ConsumoCombustibleId
    {
        public float Total_consumido { get; set; }
        public float Total_combustible { get; set; }
    }
    public class ConsumoElectricidadId
    {
        public float Total_consumido { get; set; }
        public float Total_Kilovatios { get; set; }
    }
    public class ConsumoEmisionesFugitivasId
    {
        public float Total_consumido { get; set; }
        public float Total_gases { get; set; }
    }
    public class ConsumoOtrosConsumosId
    {
        public float Total_consumido { get; set; }
        public float Total_cantidad_consumida { get; set; }
    }
    public class ConsumoMesEmisionesFugitivas
    {
        public int Mes { get; set; }
        public float Consumo_mes { get; set; }
        public float Gases_mes { get; set; }
        public string Meses { get; set; }

    }
    public class ConsumoMesOtrosConsumos
    {
        public int Mes { get; set; }
        public float Consumo_mes { get; set; }
        public float Cantidad_consumida_mes { get; set; }
        public string Meses { get; set; }

    }
    public class ConsumoMesElectricidad
    {
        public int Mes { get; set; }
        public float Consumo_mes { get; set; }
        public float Kilovatios_mes { get; set; }
        public string Meses { get; set; }

    }
}
