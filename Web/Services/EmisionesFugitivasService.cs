using API.Models.Autentificacion;
using API.Models;
using Newtonsoft.Json;
using System.Text;
using Web.Helpers;

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
        private static readonly string baseUrl = "https://localhost:7011/";

        public EmisionesFugitivasServices(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {Token.token}");
        }
        public async Task<IEnumerable<EmisionesFugitivasDTO>?> GetEmisionesFugitivas()
        {
            var response = await _httpClient.GetAsync(baseUrl + "api/Organizacion/EmisionesFugitivas");
            if (response.IsSuccessStatusCode)
            {
                var resultString = await response.Content.ReadAsStringAsync();
                var list = JsonConvert.DeserializeObject<IEnumerable<EmisionesFugitivasDTO>>(resultString);
                List<EmisionesFugitivasDTO> lista = new List<EmisionesFugitivasDTO>();
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

        public async Task<string> PostEmisionesFugitivas(EmisionesFugitivasCreateDTO org)
        {
            var orgJson =
                new StringContent(JsonConvert.SerializeObject(org), Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(baseUrl + "api/Organizacion/EmisionesFugitivas", orgJson);
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
            var response = await _httpClient.DeleteAsync(baseUrl + "api/Organizacion/EmisionesFugitivas/" + id);
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
            var response = await _httpClient.PutAsJsonAsync(baseUrl + "$api/Organizacion/EmisionesFugitivas/" + id, org);
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

