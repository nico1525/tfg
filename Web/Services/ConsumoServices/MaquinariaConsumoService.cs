using API.Models;
using System.Text.Json;
using System.Text;
using API.Models.Consumos;

namespace Web.Services.ConsumoServices
{
    public interface IMaquinariaConsumoServices
    {
        public Task<IEnumerable<MaquinariaConsumoDTO>?> GetMaquinariaConsumo();
        public Task<IEnumerable<MaquinariaConsumoDTO>?> GetMaquinariaConsumoByID(int id);
        public Task<string> PostMaquinariaConsumo(MaquinariaConsumoCreateDTO org);
        public Task<string> DeleteMaquinariaConsumo(int id);
        public Task<string> UpdateMaquinariaConsumoPorId(int id, MaquinariaConsumoModifyDTO org);
    }

    public class MaquinariaConsumoServices : IMaquinariaConsumoServices
    {
        private readonly HttpClient _httpClient;

        public MaquinariaConsumoServices(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<IEnumerable<MaquinariaConsumoDTO>?> GetMaquinariaConsumo()
        {
            return await JsonSerializer.DeserializeAsync<IEnumerable<MaquinariaConsumoDTO>>
                (await _httpClient.GetStreamAsync($"api/Organizacion/Maquinaria/Consumo"), new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
        }
        public async Task<IEnumerable<MaquinariaConsumoDTO>?> GetMaquinariaConsumoByID(int id)
        {
            return await JsonSerializer.DeserializeAsync<IEnumerable<MaquinariaConsumoDTO>>
                (await _httpClient.GetStreamAsync($"api/Organizacion/Maquinaria/Consumo/" + id), new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
        }
        public async Task<string> PostMaquinariaConsumo(MaquinariaConsumoCreateDTO org)
        {
            var orgJson =
                new StringContent(JsonSerializer.Serialize(org), Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("api/Organizacion/Maquinaria/Consumo", orgJson);
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

        public async Task<string> DeleteMaquinariaConsumo(int id)
        {
            var response = await _httpClient.DeleteAsync("api/Organizacion/Maquinaria/Consumo/" + id);
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
        public async Task<string> UpdateMaquinariaConsumoPorId(int id, MaquinariaConsumoModifyDTO org)
        {
            var response = await _httpClient.PutAsJsonAsync("$api/Organizacion/Maquinaria/Consumo/" + id, org);
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
