using API.Helpers;
using static API.Controllers.InformesControllers.InformesVehiculoController;

namespace Web.Helpers
{
    public class Storage

    {
        public static string? token { get; set; }
        public static Role? role { get; set; }
        public static ConsumoVehiculoId consumoVehiculoId { get; set; }
        public static List<ConsumoMes> consumoMes { get; set; }
    }
}
