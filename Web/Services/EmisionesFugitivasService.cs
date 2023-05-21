using API.Models.Autentificacion;
using API.Models;
using System.Text.Json;
using System.Text;

namespace Web.Services
{
    public interface IEmisionesFugitivasServices
    {
        public Task<IEnumerable<EmisionesFugitivasDTO>?> GetEmisionesFugitivas();
        public Task<string> PostEmisionesFugitivas(EmisionesFugitivasCreateDTO org);
        public Task<string> DeleteEmisionesFugitivas(int id);
        public Task<string> UpdateEmisionesFugitivasPorId(int id, EmisionesFugitivasModifyDTO org);
    }

    public class EmisionesFugitivasServices : IEmisionesFugitivasServices
    {
        private readonly HttpClient _httpClient;

        public EmisionesFugitivasServices(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<IEnumerable<EmisionesFugitivasDTO>?> GetEmisionesFugitivas()
        {
            return await JsonSerializer.DeserializeAsync<IEnumerable<EmisionesFugitivasDTO>>
                (await _httpClient.GetStreamAsync($"api/Organizacion/EmisionesFugitivas"), new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
        }

        public async Task<string> PostEmisionesFugitivas(EmisionesFugitivasCreateDTO org)
        {
            var orgJson =
                new StringContent(JsonSerializer.Serialize(org), Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("api/Organizacion/EmisionesFugitivas", orgJson);
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

        public async Task<string> DeleteEmisionesFugitivas(int id)
        {
            var response = await _httpClient.DeleteAsync("api/Organizacion/EmisionesFugitivas/" + id);
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
        public async Task<string> UpdateEmisionesFugitivasPorId(int id, EmisionesFugitivasModifyDTO org)
        {
            var response = await _httpClient.PutAsJsonAsync("$api/Organizacion/EmisionesFugitivas/" + id, org);
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

