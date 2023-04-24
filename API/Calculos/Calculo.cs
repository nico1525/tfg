using API.Models;
using API.Models.Consumos;
using API.Models.Context;
using CalculoHuellaCarbono;
using Microsoft.EntityFrameworkCore;
using API.Enumerables;

namespace API.Calculos
{
    public class Calculo
    {
        public static float CalculoConsumoVehiculo(Vehiculo vehiculo, VehiculoConsumo consumoVehiculo, DatabaseContext context)
        {
            string catVehiculo = vehiculo.CategoriaVehiculo.ToString();
            string tipo = vehiculo.TipoCombustible.ToString();
            string combustibleVehiculo = vehiculo.TipoCombustible.ToString();
            float cantidad = consumoVehiculo.CantidadCombustible;
            string? sfactor = "0";

            //Mira el valor en la tabla en base de datos el factor de emisión, dados el tipo de combustible y el tipo de vehiculo
            switch (vehiculo.CategoriaVehiculo) {
                case CategoriaVehiculo.M1:
                     sfactor = context.FactorEmisionVehiculo.Where(q => q.Tipo == tipo).FirstOrDefault().M1;
                    break;
                case CategoriaVehiculo.N1:
                     sfactor = context.FactorEmisionVehiculo.Where(q => q.Tipo == tipo).FirstOrDefault().N1;
                    break;
                case CategoriaVehiculo.N2:
                     sfactor = context.FactorEmisionVehiculo.Where(q => q.Tipo == tipo).FirstOrDefault().N2N3M2M3;
                    break;
                case CategoriaVehiculo.L:
                     sfactor = context.FactorEmisionVehiculo.Where(q => q.Tipo == tipo).FirstOrDefault().L;
                    break;
            }

            float factor = float.Parse(sfactor);

            float res = factor * cantidad;

            return res;
        }
    }
}
