using API.Models.Autentificacion;
using API.Models;
using System.Text.Json;
using System.Text;

namespace Web.Services
{
    public interface IInstalacionesFijasServices
    {
        public Task<IEnumerable<InstalacionesFijasDTO>?> GetInstalacionesFijas();
        public Task<string> PostInstalacionesFijas(InstalacionesFijasCreateDTO org);
        public Task<string> DeleteInstalacionesFijas(int id);
        public Task<string> UpdateInstalacionesFijasPorId(int id, InstalacionesFijasModifyDTO org);
    }

    public class InstalacionesFijasServices : IInstalacionesFijasServices
    {
        private readonly HttpClient _httpClient;

        public InstalacionesFijasServices(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<IEnumerable<InstalacionesFijasDTO>?> GetInstalacionesFijas()
        {
            return await JsonSerializer.DeserializeAsync<IEnumerable<InstalacionesFijasDTO>>
                (await _httpClient.GetStreamAsync($"api/Organizacion/InstalacionesFijas"), new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
        }

        public async Task<string> PostInstalacionesFijas(InstalacionesFijasCreateDTO org)
        {
            var orgJson =
                new StringContent(JsonSerializer.Serialize(org), Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("api/Organizacion/InstalacionesFijas", orgJson);
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

        public async Task<string> DeleteInstalacionesFijas(int id)
        {
            var response = await _httpClient.DeleteAsync("api/Organizacion/InstalacionesFijas/" + id);
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
        public async Task<string> UpdateInstalacionesFijasPorId(int id, InstalacionesFijasModifyDTO org)
        {
            var response = await _httpClient.PutAsJsonAsync("$api/Organizacion/InstalacionesFijas/" + id, org);
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

