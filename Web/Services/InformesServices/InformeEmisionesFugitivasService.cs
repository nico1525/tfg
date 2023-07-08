using API.Helpers;
using API.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using Web.Helpers;

namespace Web.Services.InformesServices
{
    public interface IInformeEmisionesFugitivasService
    {
        public Task<ConsumoEmisionesFugitivasId> AllEmisionesFugitivasFechas(DateTime fechaini, DateTime fechafin);
        public Task<ConsumoEmisionesFugitivasId> EmisionesFugitivasFechaByID(DateTime fechaini, DateTime fechafin, int id);
        public Task<List<ConsumoMesEmisionesFugitivas>> EmisionesFugitivasFechaByIDporMes(DateTime fechaini, DateTime fechafin, int id);

    }
    public class InformeEmisionesFugitivasService : IInformeEmisionesFugitivasService
    {
        private readonly HttpClient _httpClient;
        private static readonly string baseUrl = "https://localhost:7011/";

        public InformeEmisionesFugitivasService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.DefaultRequestHeaders.Clear();
            _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {Storage.token}");
        }
        public async Task<ConsumoEmisionesFugitivasId> AllEmisionesFugitivasFechas(DateTime fechaini, DateTime fechafin)
        {
            var response = await _httpClient.GetAsync(baseUrl + $"api/Informes/EmisionesFugitivas?fechaini={fechaini.Year}-{fechaini.Month}-{fechaini.Day}&fechafin={fechafin.Year}-{fechafin.Month}-{fechafin.Day}");
            if (response.IsSuccessStatusCode)
            {
                var resultString = await response.Content.ReadAsStringAsync();
                var consumo = JsonConvert.DeserializeObject<ConsumoEmisionesFugitivasId>(resultString);
                return consumo;
            }
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception(response.Content.ReadAsStringAsync().Result);
            }
            return null;
        }
        public async Task<ConsumoEmisionesFugitivasId> EmisionesFugitivasFechaByID(DateTime fechaini, DateTime fechafin, int id)
        {
            var response = await _httpClient.GetAsync(baseUrl + $"api/Informes/EmisionesFugitivas/{id}?fechaini={fechaini.Year}-{fechaini.Month}-{fechaini.Day}&fechafin={fechafin.Year}-{fechafin.Month}-{fechafin.Day}");

            if (response.IsSuccessStatusCode)
            {
                var resultString = await response.Content.ReadAsStringAsync();
                var consumo = JsonConvert.DeserializeObject<ConsumoEmisionesFugitivasId>(resultString);
                return consumo;
            }
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception(response.Content.ReadAsStringAsync().Result);
            }
            return null;
        }
        public async Task<List<ConsumoMesEmisionesFugitivas>> EmisionesFugitivasFechaByIDporMes(DateTime fechaini, DateTime fechafin, int id)
        {
            var response = await _httpClient.GetAsync(baseUrl + $"api/Informes/EmisionesFugitivas/{id}/mes?fechaini={fechaini.Year}-{fechaini.Month}-{fechaini.Day}&fechafin={fechafin.Year}-{fechafin.Month}-{fechafin.Day}");

            if (response.IsSuccessStatusCode)
            {
                var resultString = await response.Content.ReadAsStringAsync();
                var consumo = JsonConvert.DeserializeObject<IEnumerable<ConsumoMesEmisionesFugitivas>>(resultString);
                List<ConsumoMesEmisionesFugitivas> lista = new();
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
