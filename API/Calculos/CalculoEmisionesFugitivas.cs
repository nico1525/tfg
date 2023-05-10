using API.Models.Consumos;
using API.Models.Context;

namespace API.Calculos
{
    public class CalculoEmisionesFugitivas
    {
        public static float CalculoConsumoEmisionesFugitivas(EmisionesFugitivasConsumo ConsumoEmisiones, DatabaseContext context)
        {
            string? tipo = ConsumoEmisiones.Gas;
            float cantidad = ConsumoEmisiones.Recarga;
            string? sfactor = "0";

            sfactor = context.FactorEmisionesFugitivas.Where(q => q.Nombre == tipo || q.FormulaQuimica == tipo).FirstOrDefault().PCA;     

            float factor = float.Parse(sfactor);

            float res = factor * cantidad;

            return res;
        }
    }
}
