using API.Models.Autentificacion;
using API.Models;
using Newtonsoft.Json;
using System.Text;
using Web.Helpers;

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
        private static readonly string baseUrl = "https://localhost:7011/";

        public InstalacionesFijasServices(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {Token.token}");
        }
        public async Task<IEnumerable<InstalacionesFijasDTO>?> GetInstalacionesFijas()
        {
            var response = await _httpClient.GetAsync(baseUrl + "api/Organizacion/InstalacionesFijas");
            if (response.IsSuccessStatusCode)
            {
                var resultString = await response.Content.ReadAsStringAsync();
                var list = JsonConvert.DeserializeObject<IEnumerable<InstalacionesFijasDTO>>(resultString);
                List<InstalacionesFijasDTO> lista = new List<InstalacionesFijasDTO>();
                foreach (var veh in lista)
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

        public async Task<string> PostInstalacionesFijas(InstalacionesFijasCreateDTO org)
        {
            var orgJson =
                new StringContent(JsonConvert.SerializeObject(org), Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(baseUrl + "api/Organizacion/InstalacionesFijas", orgJson);
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
            var response = await _httpClient.DeleteAsync(baseUrl + "api/Organizacion/InstalacionesFijas/" + id);
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
            var response = await _httpClient.PutAsJsonAsync(baseUrl + "$api/Organizacion/InstalacionesFijas/" + id, org);
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

