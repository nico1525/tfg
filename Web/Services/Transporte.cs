using API.Models.Autentificacion;
using API.Models;
using System.Text.Json;
using System.Text;

namespace Web.Services
{
    public interface ITransporteServices
    {
        public Task<IEnumerable<TransporteDTO>?> GetTransporte();
        public Task<string> PostTransporte(TransporteCreateDTO org);
        public Task<string> DeleteTransporte(int id);
        public Task<string> UpdateTransportePorId(int id, TransporteModifyDTO org);
    }

    public class TransporteServices : ITransporteServices
    {
        private readonly HttpClient _httpClient;
        private static readonly string baseUrl = "https://localhost:7011/";

        public TransporteServices(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<IEnumerable<TransporteDTO>?> GetTransporte()
        {
            return await JsonSerializer.DeserializeAsync<IEnumerable<TransporteDTO>>
                (await _httpClient.GetStreamAsync(baseUrl + "api/Organizacion/Transporte"), new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
        }

        public async Task<string> PostTransporte(TransporteCreateDTO org)
        {
            var orgJson =
                new StringContent(JsonSerializer.Serialize(org), Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(baseUrl + "api/Organizacion/Transporte", orgJson);
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

        public async Task<string> DeleteTransporte(int id)
        {
            var response = await _httpClient.DeleteAsync(baseUrl + "api/Organizacion/Transporte/" + id);
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
        public async Task<string> UpdateTransportePorId(int id, TransporteModifyDTO org)
        {
            var response = await _httpClient.PutAsJsonAsync(baseUrl + "api/Organizacion/Transporte/" + id, org);
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
