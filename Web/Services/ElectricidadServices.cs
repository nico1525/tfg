using API.Models.Autentificacion;
using API.Models;
using Newtonsoft.Json;
using System.Text;
using Web.Helpers;

namespace Web.Services
{
    public interface IElectricidadServices
    {
        public Task<List<ElectricidadDTO>?> GetElectricidad();
        public Task<ElectricidadDTO?> GetElectricidadById(int id);
        public Task<string> PostElectricidad(ElectricidadCreateDTO org);
        public Task<string> DeleteElectricidad(int id);
        public Task<string> UpdateElectricidadPorId(int id, ElectricidadModifyDTO org);
    }

    public class ElectricidadServices : IElectricidadServices
    {
        private readonly HttpClient _httpClient;
        private static readonly string baseUrl = "https://localhost:7011/";

        public ElectricidadServices(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.DefaultRequestHeaders.Clear();
            _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {Storage.token}");
        }
        public async Task<List<ElectricidadDTO>?> GetElectricidad()
        {
            var response = await _httpClient.GetAsync(baseUrl + "api/Organizacion/Electricidad");
            if (response.IsSuccessStatusCode)
            {
                var resultString = await response.Content.ReadAsStringAsync();
                var list = JsonConvert.DeserializeObject<IEnumerable<ElectricidadDTO>>(resultString);
                List<ElectricidadDTO> lista = new List<ElectricidadDTO>();
                foreach (var veh in list)
                {
                    lista.Add(veh);
                }
                return lista;
            }
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception(response.Content.ReadAsStringAsync().Result);
            }
            return null;
        }
        public async Task<ElectricidadDTO?> GetElectricidadById(int id)
        {
            var response = await _httpClient.GetAsync(baseUrl + "api/Organizacion/Electricidad/" + id);

            if (response.IsSuccessStatusCode)
            {
                var resultString = await response.Content.ReadAsStringAsync();
                var Electricidad = JsonConvert.DeserializeObject<ElectricidadDTO>(resultString);
                return Electricidad;
            }
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception(response.Content.ReadAsStringAsync().Result);
            }
            return null;
        }
        public async Task<string> PostElectricidad(ElectricidadCreateDTO org)
        {
            var orgJson =
                new StringContent(JsonConvert.SerializeObject(org), Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(baseUrl + "api/Organizacion/Electricidad", orgJson);
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

        public async Task<string> DeleteElectricidad(int id)
        {
            var response = await _httpClient.DeleteAsync(baseUrl + "api/Organizacion/Electricidad/" + id);
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
        public async Task<string> UpdateElectricidadPorId(int id, ElectricidadModifyDTO org)
        {
            var response = await _httpClient.PutAsJsonAsync(baseUrl + "api/Organizacion/Electricidad/" + id, org);
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
