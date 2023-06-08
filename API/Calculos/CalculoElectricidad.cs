using API.Models.Consumos;
using API.Models.Context;

namespace API.Calculos
{
    public class CalculoElectricidad
    {
        public static float CalculoConsumoElectricidad(ElectricidadConsumo ConsumoElectricidad, DatabaseContext context)
        {
            int id = ConsumoElectricidad.ComercializadoraId;
            float cantidad = ConsumoElectricidad.Kwh;
            string? sfactor = "0";

            sfactor = context.FactorEmisionElectricidad.Where(q => q.Id == id).FirstOrDefault().factor;

            float factor = float.Parse(sfactor);

            float res = factor * cantidad;

            return res;
        }
        
        public static string GetComercializadora(ElectricidadConsumo ConsumoElectricidad, DatabaseContext context)
        {
            int id = ConsumoElectricidad.ComercializadoraId;

            string comercializadora = context.FactorEmisionElectricidad.Where(q => q.Id == id).FirstOrDefault().Comercializadora;

            return comercializadora;
        }
    }
}
