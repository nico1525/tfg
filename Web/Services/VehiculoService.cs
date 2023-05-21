using API.Models.Autentificacion;
using API.Models;
using System.Text.Json;
using System.Text;

namespace Web.Services
{
    public interface IVehiculoServices
    {
        public Task<IEnumerable<VehiculoDTO>?> GetVehiculo();
        public Task<string> PostVehiculo(VehiculoCreateDTO org);
        public Task<string> DeleteVehiculo(int id);
        public Task<string> UpdateVehiculoPorId(int id, VehiculoModifyDTO org);
    }

    public class VehiculoServices : IVehiculoServices
    {
        private readonly HttpClient _httpClient;

        public VehiculoServices(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<IEnumerable<VehiculoDTO>?> GetVehiculo()
        {
            return await JsonSerializer.DeserializeAsync<IEnumerable<VehiculoDTO>>
                (await _httpClient.GetStreamAsync($"api/Organizacion/Vehiculo"), new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
        }

        public async Task<string> PostVehiculo(VehiculoCreateDTO org)
        {
            var orgJson =
                new StringContent(JsonSerializer.Serialize(org), Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("api/Organizacion/Vehiculo", orgJson);
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

        public async Task<string> DeleteVehiculo(int id)
        {
            var response = await _httpClient.DeleteAsync("api/Organizacion/Vehiculo/" + id);
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
        public async Task<string> UpdateVehiculoPorId(int id, VehiculoModifyDTO org)
        {
            var response = await _httpClient.PutAsJsonAsync("$api/Organizacion/Vehiculo/" + id, org);
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
