using API.Models;
using Newtonsoft.Json;
using System.Text;
using API.Models.Consumos;
using Web.Helpers;

namespace Web.Services.ConsumoServices
{
    public interface IInstalacionesFijasConsumoServices
    {
        public Task<List<InstalacionesFijasConsumoDTO>?> GetInstalacionesFijasConsumo();
        public Task<InstalacionesFijasConsumoDTO?> GetInstalacionesFijasConsumoByID(int id);
        public Task<string> PostInstalacionesFijasConsumo(InstalacionesFijasConsumoCreateDTO org);
        public Task<string> DeleteInstalacionesFijasConsumo(int id);
        public Task<string> UpdateInstalacionesFijasConsumoPorId(int id, InstalacionesFijasConsumoModifyDTO org);
    }

    public class InstalacionesFijasConsumoServices : IInstalacionesFijasConsumoServices
    {
        private readonly HttpClient _httpClient;
        private static readonly string baseUrl = "https://localhost:7011/";

        public InstalacionesFijasConsumoServices(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {Token.token}");
        }
        public async Task<List<InstalacionesFijasConsumoDTO>?> GetInstalacionesFijasConsumo()
        {
            var response = await _httpClient.GetAsync(baseUrl + "api/Organizacion/InstalacionesFijas/Consumo");
            if (response.IsSuccessStatusCode)
            {
                var resultString = await response.Content.ReadAsStringAsync();
                var list = JsonConvert.DeserializeObject<IEnumerable<InstalacionesFijasConsumoDTO>>(resultString);
                List<InstalacionesFijasConsumoDTO> lista = new List<InstalacionesFijasConsumoDTO>();
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
        public async Task<InstalacionesFijasConsumoDTO?> GetInstalacionesFijasConsumoByID(int id)
        {
            var response = await _httpClient.GetAsync(baseUrl + "api/Organizacion/InstalacionesFijas/Consumo/" + id);
            if (response.IsSuccessStatusCode)
            {
                var resultString = await response.Content.ReadAsStringAsync();
                var list = JsonConvert.DeserializeObject<InstalacionesFijasConsumoDTO>(resultString);
                return list;
            }
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception(response.Content.ReadAsStringAsync().Result);
            }
            return null;
        }
        public async Task<string> PostInstalacionesFijasConsumo(InstalacionesFijasConsumoCreateDTO org)
        {
            var orgJson =
                new StringContent(JsonConvert.SerializeObject(org), Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(baseUrl + "api/Organizacion/InstalacionesFijas/Consumo", orgJson);
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

        public async Task<string> DeleteInstalacionesFijasConsumo(int id)
        {
            var response = await _httpClient.DeleteAsync(baseUrl + "api/Organizacion/InstalacionesFijas/Consumo/" + id);
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
        public async Task<string> UpdateInstalacionesFijasConsumoPorId(int id, InstalacionesFijasConsumoModifyDTO org)
        {
            var response = await _httpClient.PutAsJsonAsync(baseUrl + "api/Organizacion/InstalacionesFijas/Consumo/" + id, org);
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
