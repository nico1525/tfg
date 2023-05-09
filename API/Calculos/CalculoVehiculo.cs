using API.Models;
using API.Models.Consumos;
using API.Models.Context;
namespace API.Calculos
{
    public class CalculoVehiculo
    {
        public static float CalculoConsumoVehiculo(Vehiculo vehiculo, VehiculoConsumo consumoVehiculo, DatabaseContext context)
        {
            string? tipo = consumoVehiculo.TipoCombustible;
            float cantidad = consumoVehiculo.CantidadCombustible;
            string? sfactor = "0";

            //Mira el valor en la tabla en base de datos el factor de emisión, dados el tipo de combustible y el tipo de vehiculo
            switch (vehiculo.CategoriaVehiculo) {
                case "M1":
                     sfactor = context.FactorEmisionVehiculo.Where(q => q.Tipo == tipo).FirstOrDefault().M1;
                    break;
                case "N1":
                     sfactor = context.FactorEmisionVehiculo.Where(q => q.Tipo == tipo).FirstOrDefault().N1;
                    break;
                case "N2":
                case "N3":
                case "M2":
                case "M3":
                     sfactor = context.FactorEmisionVehiculo.Where(q => q.Tipo == tipo).FirstOrDefault().N2N3M2M3;
                    break;
                case "L":
                     sfactor = context.FactorEmisionVehiculo.Where(q => q.Tipo == tipo).FirstOrDefault().L;
                    break;
            }

            float factor = float.Parse(sfactor);

            float res = factor * cantidad;

            return res;
        }
    }
}
