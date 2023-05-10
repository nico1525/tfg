using API.Models.Consumos;
using API.Models.Context;
using API.Models;

namespace API.Calculos
{
    public class CalculoMaquinaria
    {
        public static float CalculoConsumoMaquinaria(Maquinaria maquinaria, MaquinariaConsumo consumoMaquinaria, DatabaseContext context)
        {
            string? tipo = consumoMaquinaria.TipoCombustible;
            float cantidad = consumoMaquinaria.CantidadCombustible;
            string? sfactor = "0";

            //Mira el valor en la tabla en base de datos el factor de emisión, dados el tipo de combustible y el tipo de vehiculo
            switch (maquinaria.TipoMaquinaria)
            {
                case "agricola":
                    sfactor = context.FactorEmisionMaquinaria.Where(q => q.Tipo == tipo).FirstOrDefault().agricola;
                    break;
                case "forestal":
                    sfactor = context.FactorEmisionMaquinaria.Where(q => q.Tipo == tipo).FirstOrDefault().forestal;
                    break;
                case "industrial":
                    sfactor = context.FactorEmisionMaquinaria.Where(q => q.Tipo == tipo).FirstOrDefault().industrial;
                    break;
            }

            float factor = float.Parse(sfactor);

            float res = factor * cantidad;

            return res;
        }
    }
}
