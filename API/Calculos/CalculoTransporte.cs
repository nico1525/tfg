using API.Models.Consumos;
using API.Models.Context;
using API.Models;

namespace API.Calculos
{
    public class CalculoTransporte
    {
        public static float CalculoConsumoTransporte(Transporte Transporte, TransporteConsumo ConsumoTransporte, DatabaseContext context)
        {
            string? tipo = Transporte.CombustibleTransporte;
            float cantidad = ConsumoTransporte.CantidadCombustible;
            string? sfactor = "0";

            //Mira el valor en la tabla en base de datos el factor de emisión, dados el tipo de combustible y el tipo de vehiculo
            switch (Transporte.TipoTransporte)
            {
                case "ferrocarril":
                    sfactor = context.FactorEmisionTransporte.Where(q => q.Tipo == tipo).FirstOrDefault().ferrocarril;
                    break;
                case "maritimo":
                    sfactor = context.FactorEmisionTransporte.Where(q => q.Tipo == tipo).FirstOrDefault().maritimo;
                    break;
                case "aereo":
                    sfactor = context.FactorEmisionTransporte.Where(q => q.Tipo == tipo).FirstOrDefault().aereo;
                    break;
            }

            float factor = float.Parse(sfactor);

            float res = factor * cantidad;

            return res;
        }
    }
}
