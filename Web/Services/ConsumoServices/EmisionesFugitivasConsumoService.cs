using API.Models;
using System.Text.Json;
using System.Text;
using API.Models.Consumos;

namespace Web.Services.ConsumoServices
{
    public interface IEmisionesFugitivasConsumoServices
    {
        public Task<IEnumerable<EmisionesFugitivasConsumoDTO>?> GetEmisionesFugitivasConsumo();
        public Task<IEnumerable<EmisionesFugitivasConsumoDTO>?> GetEmisionesFugitivasConsumoByID(int id);
        public Task<string> PostEmisionesFugitivasConsumo(EmisionesFugitivasConsumoCreateDTO org);
        public Task<string> DeleteEmisionesFugitivasConsumo(int id);
        public Task<string> UpdateEmisionesFugitivasConsumoPorId(int id, EmisionesFugitivasConsumoModifyDTO org);
    }

    public class EmisionesFugitivasConsumoServices : IEmisionesFugitivasConsumoServices
    {
        private readonly HttpClient _httpClient;

        public EmisionesFugitivasConsumoServices(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<IEnumerable<EmisionesFugitivasConsumoDTO>?> GetEmisionesFugitivasConsumo()
        {
            return await JsonSerializer.DeserializeAsync<IEnumerable<EmisionesFugitivasConsumoDTO>>
                (await _httpClient.GetStreamAsync($"api/Organizacion/EmisionesFugitivas/Consumo"), new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
        }
        public async Task<IEnumerable<EmisionesFugitivasConsumoDTO>?> GetEmisionesFugitivasConsumoByID(int id)
        {
            return await JsonSerializer.DeserializeAsync<IEnumerable<EmisionesFugitivasConsumoDTO>>
                (await _httpClient.GetStreamAsync($"api/Organizacion/EmisionesFugitivas/Consumo/" + id), new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
        }
        public async Task<string> PostEmisionesFugitivasConsumo(EmisionesFugitivasConsumoCreateDTO org)
        {
            var orgJson =
                new StringContent(JsonSerializer.Serialize(org), Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("api/Organizacion/EmisionesFugitivas/Consumo", orgJson);
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

        public async Task<string> DeleteEmisionesFugitivasConsumo(int id)
        {
            var response = await _httpClient.DeleteAsync("api/Organizacion/EmisionesFugitivas/Consumo/" + id);
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
        public async Task<string> UpdateEmisionesFugitivasConsumoPorId(int id, EmisionesFugitivasConsumoModifyDTO org)
        {
            var response = await _httpClient.PutAsJsonAsync("$api/Organizacion/EmisionesFugitivas/Consumo/" + id, org);
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
