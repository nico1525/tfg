using API.Models.Consumos;
using API.Models.Context;
using API.Models;

namespace API.Calculos
{
    public class CalculoInstalacionesFijas
    {
        public static float CalculoConsumoInstalacionesFijas(InstalacionesFijasConsumo consumoinstalaciones, DatabaseContext context)
        {
            string? tipo = consumoinstalaciones.TipoCombustible;
            float cantidad = consumoinstalaciones.CantidadCombustible;
            string? sfactor = "0";

            sfactor = context.FactorInstalacionesFijas.Where(q => q.Tipo == tipo).FirstOrDefault().factor;

            float factor = float.Parse(sfactor);

            float res = factor * cantidad;

            return res;
        }
    }
}
