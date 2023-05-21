using API.Models;
using System.Text.Json;
using System.Text;
using API.Models.Consumos;

namespace Web.Services.ConsumoServices
{
    public interface ITransporteConsumoServices
    {
        public Task<IEnumerable<TransporteConsumoDTO>?> GetTransporteConsumo();
        public Task<IEnumerable<TransporteConsumoDTO>?> GetTransporteConsumoByID(int id);
        public Task<string> PostTransporteConsumo(TransporteConsumoCreateDTO org);
        public Task<string> DeleteTransporteConsumo(int id);
        public Task<string> UpdateTransporteConsumoPorId(int id, TransporteConsumoModifyDTO org);
    }

    public class TransporteConsumoServices : ITransporteConsumoServices
    {
        private readonly HttpClient _httpClient;

        public TransporteConsumoServices(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<IEnumerable<TransporteConsumoDTO>?> GetTransporteConsumo()
        {
            return await JsonSerializer.DeserializeAsync<IEnumerable<TransporteConsumoDTO>>
                (await _httpClient.GetStreamAsync($"api/Organizacion/Transporte/Consumo"), new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
        }
        public async Task<IEnumerable<TransporteConsumoDTO>?> GetTransporteConsumoByID(int id)
        {
            return await JsonSerializer.DeserializeAsync<IEnumerable<TransporteConsumoDTO>>
                (await _httpClient.GetStreamAsync($"api/Organizacion/Transporte/Consumo/" + id), new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
        }
        public async Task<string> PostTransporteConsumo(TransporteConsumoCreateDTO org)
        {
            var orgJson =
                new StringContent(JsonSerializer.Serialize(org), Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("api/Organizacion/Transporte/Consumo", orgJson);
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

        public async Task<string> DeleteTransporteConsumo(int id)
        {
            var response = await _httpClient.DeleteAsync("api/Organizacion/Transporte/Consumo/" + id);
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
        public async Task<string> UpdateTransporteConsumoPorId(int id, TransporteConsumoModifyDTO org)
        {
            var response = await _httpClient.PutAsJsonAsync("$api/Organizacion/Transporte/Consumo/" + id, org);
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

