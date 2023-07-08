using API.Helpers;
using API.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using Web.Helpers;

namespace Web.Services.InformesServices
{
    public interface IInformeMaquinariaService
    {
        public Task<ConsumoCombustibleId> AllMaquinariaFechas(DateTime fechaini, DateTime fechafin);
        public Task<ConsumoCombustibleId> MaquinariasFechaByID(DateTime fechaini, DateTime fechafin, int id);
        public Task<List<ConsumoMes>> MaquinariasFechaByIDporMes(DateTime fechaini, DateTime fechafin, int id);

    }
    public class InformeMaquinariaService : IInformeMaquinariaService
    {
        private readonly HttpClient _httpClient;
        private static readonly string baseUrl = "https://localhost:7011/";

        public InformeMaquinariaService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.DefaultRequestHeaders.Clear();
            _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {Storage.token}");
        }
        public async Task<ConsumoCombustibleId> AllMaquinariaFechas(DateTime fechaini, DateTime fechafin)
        {
            var response = await _httpClient.GetAsync(baseUrl + $"api/Informes/Maquinaria?fechaini={fechaini.Year}-{fechaini.Month}-{fechaini.Day}&fechafin={fechafin.Year}-{fechafin.Month}-{fechafin.Day}");
            if (response.IsSuccessStatusCode)
            {
                var resultString = await response.Content.ReadAsStringAsync();
                var consumo = JsonConvert.DeserializeObject<ConsumoCombustibleId>(resultString);
                return consumo;
            }
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception(response.Content.ReadAsStringAsync().Result);
            }
            return null;
        }
        public async Task<ConsumoCombustibleId> MaquinariasFechaByID(DateTime fechaini, DateTime fechafin, int id)
        {
            var response = await _httpClient.GetAsync(baseUrl + $"api/Informes/Maquinaria/{id}?fechaini={fechaini.Year}-{fechaini.Month}-{fechaini.Day}&fechafin={fechafin.Year}-{fechafin.Month}-{fechafin.Day}");

            if (response.IsSuccessStatusCode)
            {
                var resultString = await response.Content.ReadAsStringAsync();
                var consumo = JsonConvert.DeserializeObject<ConsumoCombustibleId>(resultString);
                return consumo;
            }
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception(response.Content.ReadAsStringAsync().Result);
            }
            return null;
        }
        public async Task<List<ConsumoMes>> MaquinariasFechaByIDporMes(DateTime fechaini, DateTime fechafin, int id)
        {
            var response = await _httpClient.GetAsync(baseUrl + $"api/Informes/Maquinaria/{id}/mes?fechaini={fechaini.Year}-{fechaini.Month}-{fechaini.Day}&fechafin={fechafin.Year}-{fechafin.Month}-{fechafin.Day}");

            if (response.IsSuccessStatusCode)
            {
                var resultString = await response.Content.ReadAsStringAsync();
                var consumo = JsonConvert.DeserializeObject<IEnumerable<ConsumoMes>>(resultString);
                List<ConsumoMes> lista = new();
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
