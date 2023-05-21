using API.Models.Autentificacion;
using API.Models;
using System.Text.Json;
using System.Text;

namespace Web.Services
{
    public interface IMaquinariaServices
    {
        public Task<IEnumerable<MaquinariaDTO>?> GetMaquinaria();
        public Task<string> PostMaquinaria(MaquinariaCreateDTO org);
        public Task<string> DeleteMaquinaria(int id);
        public Task<string> UpdateMaquinariaPorId(int id, MaquinariaModifyDTO org);
    }

    public class MaquinariaServices : IMaquinariaServices
    {
        private readonly HttpClient _httpClient;

        public MaquinariaServices(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<IEnumerable<MaquinariaDTO>?> GetMaquinaria()
        {
            return await JsonSerializer.DeserializeAsync<IEnumerable<MaquinariaDTO>>
                (await _httpClient.GetStreamAsync($"api/Organizacion/Maquinaria"), new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
        }

        public async Task<string> PostMaquinaria(MaquinariaCreateDTO org)
        {
            var orgJson =
                new StringContent(JsonSerializer.Serialize(org), Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("api/Organizacion/Maquinaria", orgJson);
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

        public async Task<string> DeleteMaquinaria(int id)
        {
            var response = await _httpClient.DeleteAsync("api/Organizacion/Maquinaria/" + id);
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
        public async Task<string> UpdateMaquinariaPorId(int id, MaquinariaModifyDTO org)
        {
            var response = await _httpClient.PutAsJsonAsync("$api/Organizacion/Maquinaria/" + id, org);
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

