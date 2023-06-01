using API.Models;
using Newtonsoft.Json;
using System.Text;
using API.Models.Consumos;
using Web.Helpers;

namespace Web.Services.ConsumoServices
{
    public interface IOtrosConsumosServices
    {
        public Task<List<OtrosConsumosDTO>?> GetOtrosConsumos();
        public Task<OtrosConsumosDTO?> GetConsumoByID(int id);
        public Task<OtrosConsumosDTO?> GetOtrosConsumosByID(int id);
        public Task<string> PostOtrosConsumos(OtrosConsumosCreateDTO org);
        public Task<string> DeleteOtrosConsumos(int id);
        public Task<string> UpdateOtrosConsumosPorId(int id, OtrosConsumosModifyDTO org);
    }

    public class OtrosConsumosServices : IOtrosConsumosServices
    {
        private readonly HttpClient _httpClient;
        private static readonly string baseUrl = "https://localhost:7011/";


        public OtrosConsumosServices(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {Token.token}");
        }
        public async Task<List<OtrosConsumosDTO>?> GetOtrosConsumos()
        {
            var response = await _httpClient.GetAsync(baseUrl + "api/Organizacion/OtrosConsumos");
            if (response.IsSuccessStatusCode)
            {
                var resultString = await response.Content.ReadAsStringAsync();
                var list = JsonConvert.DeserializeObject<IEnumerable<OtrosConsumosDTO>>(resultString);
                List<OtrosConsumosDTO> lista = new List<OtrosConsumosDTO>();
                foreach (var veh in list)
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

        public async Task<OtrosConsumosDTO?> GetConsumoByID(int id)
        {
            var response = await _httpClient.GetAsync(baseUrl + "api/Organizacion/OtrosConsumo/Consumo/id/" + id);
            if (response.IsSuccessStatusCode)
            {
                var resultString = await response.Content.ReadAsStringAsync();
                var consumo = JsonConvert.DeserializeObject<OtrosConsumosDTO>(resultString);
                return consumo;
            }
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception(response.Content.ReadAsStringAsync().Result);
            }
            return null;
        }
        public async Task<OtrosConsumosDTO?> GetOtrosConsumosByID(int id)
        {
            var response = await _httpClient.GetAsync(baseUrl + "api/Organizacion/OtrosConsumos/" + id);
            if (response.IsSuccessStatusCode)
            {
                var resultString = await response.Content.ReadAsStringAsync();
                var list = JsonConvert.DeserializeObject<OtrosConsumosDTO>(resultString);
                return list;
            }
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception(response.Content.ReadAsStringAsync().Result);
            }
            return null;
        }
        public async Task<string> PostOtrosConsumos(OtrosConsumosCreateDTO org)
        {
            var orgJson =
                new StringContent(JsonConvert.SerializeObject(org), Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(baseUrl + "api/Organizacion/OtrosConsumos", orgJson);
            if (response.IsSuccessStatusCode)
            {
                return response.Content.ReadAsStringAsync().Result;
            }
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception(response.Content.ReadAsStringAsync().Result);
            }

            return null;
        }

        public async Task<string> DeleteOtrosConsumos(int id)
        {
            var response = await _httpClient.DeleteAsync(baseUrl + "api/Organizacion/OtrosConsumos/" + id);
            if (response.IsSuccessStatusCode)
            {
                return response.Content.ReadAsStringAsync().Result;
            }
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception(response.Content.ReadAsStringAsync().Result);
            }
            return null;
        }
        public async Task<string> UpdateOtrosConsumosPorId(int id, OtrosConsumosModifyDTO org)
        {
            var response = await _httpClient.PutAsJsonAsync(baseUrl + "api/Organizacion/OtrosConsumos/" + id, org);
            if (response.IsSuccessStatusCode)
            {
                return response.Content.ReadAsStringAsync().Result;
            }
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception(response.Content.ReadAsStringAsync().Result);
            }
            return null;
        }
    }
}
