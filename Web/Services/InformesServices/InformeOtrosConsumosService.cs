using API.Helpers;
using API.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using Web.Helpers;

namespace Web.Services.InformesServices
{
    public interface IInformeOtrosConsumosService
    {
        public Task<ConsumoOtrosConsumosId> AllOtrosConsumosFechas(DateTime fechaini, DateTime fechafin);
        public Task<ConsumoOtrosConsumosId> OtrosConsumosFechaByID(DateTime fechaini, DateTime fechafin, int id);
        public Task<List<ConsumoMesOtrosConsumos>> OtrosConsumosFechaByIDporMes(DateTime fechaini, DateTime fechafin, int id);

    }
    public class InformeOtrosConsumosService : IInformeOtrosConsumosService
    {
        private readonly HttpClient _httpClient;
        private static readonly string baseUrl = "https://localhost:7011/";

        public InformeOtrosConsumosService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.DefaultRequestHeaders.Clear();
            _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {Storage.token}");
        }
        public async Task<ConsumoOtrosConsumosId> AllOtrosConsumosFechas(DateTime fechaini, DateTime fechafin)
        {
            var response = await _httpClient.GetAsync(baseUrl + $"api/Informes/OtrosConsumos?fechaini={fechaini.Year}-{fechaini.Month}-{fechaini.Day}&fechafin={fechafin.Year}-{fechafin.Month}-{fechafin.Day}");
            if (response.IsSuccessStatusCode)
            {
                var resultString = await response.Content.ReadAsStringAsync();
                var consumo = JsonConvert.DeserializeObject<ConsumoOtrosConsumosId>(resultString);
                return consumo;
            }
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception(response.Content.ReadAsStringAsync().Result);
            }
            return null;
        }
        public async Task<ConsumoOtrosConsumosId> OtrosConsumosFechaByID(DateTime fechaini, DateTime fechafin, int id)
        {
            var response = await _httpClient.GetAsync(baseUrl + $"api/Informes/OtrosConsumos/{id}?fechaini={fechaini.Year}-{fechaini.Month}-{fechaini.Day}&fechafin={fechafin.Year}-{fechafin.Month}-{fechafin.Day}");

            if (response.IsSuccessStatusCode)
            {
                var resultString = await response.Content.ReadAsStringAsync();
                var consumo = JsonConvert.DeserializeObject<ConsumoOtrosConsumosId>(resultString);
                return consumo;
            }
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception(response.Content.ReadAsStringAsync().Result);
            }
            return null;
        }
        public async Task<List<ConsumoMesOtrosConsumos>> OtrosConsumosFechaByIDporMes(DateTime fechaini, DateTime fechafin, int id)
        {
            var response = await _httpClient.GetAsync(baseUrl + $"api/Informes/OtrosConsumos/{id}/mes?fechaini={fechaini.Year}-{fechaini.Month}-{fechaini.Day}&fechafin={fechafin.Year}-{fechafin.Month}-{fechafin.Day}");

            if (response.IsSuccessStatusCode)
            {
                var resultString = await response.Content.ReadAsStringAsync();
                var consumo = JsonConvert.DeserializeObject<IEnumerable<ConsumoMesOtrosConsumos>>(resultString);
                List<ConsumoMesOtrosConsumos> lista = new();
                foreach (var veh in consumo)
                {
                    lista.Add(veh);
                }
                return lista;
            }
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception(response.Content.ReadAsStringAsync().Result);
            }
            return null;
        }
    }
}
