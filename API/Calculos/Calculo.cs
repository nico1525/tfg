using API.Models;
using API.Models.Consumos;
using API.Models.Context;
using CalculoHuellaCarbono;

namespace API.Calculos
{
    public class Calculo
    {
        public static float CalculoConsumoVehiculo(Vehiculo vehiculo, VehiculoConsumo consumoVehiculo, DatabaseContext context)
        {
            string catVehiculo = vehiculo.CategoriaVehiculo.ToString();
            string combustibleVehiculo = vehiculo.TipoCombustible.ToString();
            float cantidad = consumoVehiculo.CantidadCombustible;

            

            return 0;
        }
    }
}
