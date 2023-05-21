using API.Models;
using System.Text.Json;
using System.Text;
using API.Models.Consumos;

namespace Web.Services.ConsumoServices
{
    public interface IOtrosConsumosServices
    {
        public Task<IEnumerable<OtrosConsumosDTO>?> GetOtrosConsumos();
        public Task<IEnumerable<OtrosConsumosDTO>?> GetOtrosConsumosByID(int id);
        public Task<string> PostOtrosConsumos(OtrosConsumosCreateDTO org);
        public Task<string> DeleteOtrosConsumos(int id);
        public Task<string> UpdateOtrosConsumosPorId(int id, OtrosConsumosModifyDTO org);
    }

    public class OtrosConsumosServices : IOtrosConsumosServices
    {
        private readonly HttpClient _httpClient;

        public OtrosConsumosServices(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<IEnumerable<OtrosConsumosDTO>?> GetOtrosConsumos()
        {
            return await JsonSerializer.DeserializeAsync<IEnumerable<OtrosConsumosDTO>>
                (await _httpClient.GetStreamAsync($"api/Organizacion/OtrosConsumos"), new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
        }
        public async Task<IEnumerable<OtrosConsumosDTO>?> GetOtrosConsumosByID(int id)
        {
            return await JsonSerializer.DeserializeAsync<IEnumerable<OtrosConsumosDTO>>
                (await _httpClient.GetStreamAsync($"api/Organizacion/OtrosConsumos/" + id), new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
        }
        public async Task<string> PostOtrosConsumos(OtrosConsumosCreateDTO org)
        {
            var orgJson =
                new StringContent(JsonSerializer.Serialize(org), Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("api/Organizacion/OtrosConsumos", orgJson);
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
            var response = await _httpClient.DeleteAsync("api/Organizacion/OtrosConsumos/" + id);
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
            var response = await _httpClient.PutAsJsonAsync("$api/Organizacion/OtrosConsumos/" + id, org);
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
