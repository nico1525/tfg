using API.Models;
using System.Text.Json;
using System.Text;
using API.Models.Consumos;

namespace Web.Services.ConsumoServices
{
    public interface IVehiculoConsumoServices
    {
        public Task<IEnumerable<VehiculoConsumoDTO>?> GetVehiculoConsumo();
        public Task<IEnumerable<VehiculoConsumoDTO>?> GetVehiculoConsumoByID(int id);
        public Task<string> PostVehiculoConsumo(VehiculoConsumoCreateDTO org);
        public Task<string> DeleteVehiculoConsumo(int id);
        public Task<string> UpdateVehiculoConsumoPorId(int id, VehiculoConsumoModifyDTO org);
    }

    public class VehiculoConsumoServices : IVehiculoConsumoServices
    {
        private readonly HttpClient _httpClient;
        private static readonly string baseUrl = "https://localhost:7011/";

        public VehiculoConsumoServices(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<IEnumerable<VehiculoConsumoDTO>?> GetVehiculoConsumo()
        {
            return await JsonSerializer.DeserializeAsync<IEnumerable<VehiculoConsumoDTO>>
                (await _httpClient.GetStreamAsync(baseUrl + "api/Organizacion/Vehiculo/Consumo"), new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
        }
        public async Task<IEnumerable<VehiculoConsumoDTO>?> GetVehiculoConsumoByID(int id)
        {
            return await JsonSerializer.DeserializeAsync<IEnumerable<VehiculoConsumoDTO>>
                (await _httpClient.GetStreamAsync(baseUrl + "api/Organizacion/Vehiculo/Consumo/" + id), new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
        }
        public async Task<string> PostVehiculoConsumo(VehiculoConsumoCreateDTO org)
        {
            var orgJson =
                new StringContent(JsonSerializer.Serialize(org), Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(baseUrl + "api/Organizacion/Vehiculo/Consumo", orgJson);
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

        public async Task<string> DeleteVehiculoConsumo(int id)
        {
            var response = await _httpClient.DeleteAsync(baseUrl + "api/Organizacion/Vehiculo/Consumo/" + id);
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
        public async Task<string> UpdateVehiculoConsumoPorId(int id, VehiculoConsumoModifyDTO org)
        {
            var response = await _httpClient.PutAsJsonAsync(baseUrl + "api/Organizacion/Vehiculo/Consumo/" + id, org);
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
