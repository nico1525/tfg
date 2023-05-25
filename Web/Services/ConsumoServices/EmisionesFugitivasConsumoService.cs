using API.Models;
using Newtonsoft.Json;
using System.Text;
using API.Models.Consumos;
using Web.Helpers;

namespace Web.Services.ConsumoServices
{
    public interface IEmisionesFugitivasConsumoServices
    {
        public Task<IEnumerable<EmisionesFugitivasConsumoDTO>?> GetEmisionesFugitivasConsumo();
        public Task<IEnumerable<EmisionesFugitivasConsumoDTO>?> GetEmisionesFugitivasConsumoByID(int id);
        public Task<string> PostEmisionesFugitivasConsumo(EmisionesFugitivasConsumoCreateDTO org);
        public Task<string> DeleteEmisionesFugitivasConsumo(int id);
        public Task<string> UpdateEmisionesFugitivasConsumoPorId(int id, EmisionesFugitivasConsumoModifyDTO org);
    }

    public class EmisionesFugitivasConsumoServices : IEmisionesFugitivasConsumoServices
    {
        private readonly HttpClient _httpClient;
        private static readonly string baseUrl = "https://localhost:7011/";

        public EmisionesFugitivasConsumoServices(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {Token.token}");
        }
        public async Task<IEnumerable<EmisionesFugitivasConsumoDTO>?> GetEmisionesFugitivasConsumo()
        {
            var response = await _httpClient.GetAsync(baseUrl + "api/Organizacion/EmisionesFugitivas/Consumo");
            if (response.IsSuccessStatusCode)
            {
                var resultString = await response.Content.ReadAsStringAsync();
                var list = JsonConvert.DeserializeObject<IEnumerable<EmisionesFugitivasConsumoDTO>>(resultString);
                List<EmisionesFugitivasConsumoDTO> lista = new List<EmisionesFugitivasConsumoDTO>();
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
        public async Task<IEnumerable<EmisionesFugitivasConsumoDTO>?> GetEmisionesFugitivasConsumoByID(int id)
        {
            var response = await _httpClient.GetAsync(baseUrl + "api/Organizacion/EmisionesFugitivas/Consumo/" + id);
            if (response.IsSuccessStatusCode)
            {
                var resultString = await response.Content.ReadAsStringAsync();
                var list = JsonConvert.DeserializeObject<IEnumerable<EmisionesFugitivasConsumoDTO>>(resultString);
                List<EmisionesFugitivasConsumoDTO> lista = new List<EmisionesFugitivasConsumoDTO>();
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
        public async Task<string> PostEmisionesFugitivasConsumo(EmisionesFugitivasConsumoCreateDTO org)
        {
            var orgJson =
                new StringContent(JsonConvert.SerializeObject(org), Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(baseUrl + "api/Organizacion/EmisionesFugitivas/Consumo", orgJson);
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

        public async Task<string> DeleteEmisionesFugitivasConsumo(int id)
        {
            var response = await _httpClient.DeleteAsync(baseUrl + "api/Organizacion/EmisionesFugitivas/Consumo/" + id);
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
        public async Task<string> UpdateEmisionesFugitivasConsumoPorId(int id, EmisionesFugitivasConsumoModifyDTO org)
        {
            var response = await _httpClient.PutAsJsonAsync(baseUrl + "api/Organizacion/EmisionesFugitivas/Consumo/" + id, org);
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
