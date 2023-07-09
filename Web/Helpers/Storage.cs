using API.Helpers;
using static API.Controllers.InformesControllers.InformesVehiculoController;

namespace Web.Helpers
{
    public class Storage

    {
        public static string? token { get; set; }
        public static Role? role { get; set; }
        public static ConsumoCombustibleId consumoVehiculoId { get; set; }
        public static ConsumoEmisionesFugitivasId ConsumoEmisionesFugitivas { get; set; }
        public static ConsumoElectricidadId ConsumoElectricidad { get; set; }
        public static ConsumoOtrosConsumosId ConsumoOtrosConsumos { get; set; }

        public static List<ConsumoMes> consumoMes { get; set; }
        public static List<ConsumoMesEmisionesFugitivas> ConsumoMesEmisionesFugitivas { get; set; }
        public static List<ConsumoMesElectricidad> ConsumoMesElectricidad { get; set; }
        public static List<ConsumoMesOtrosConsumos> ConsumoMesOtrosConsumos { get; set; }

    }
}
