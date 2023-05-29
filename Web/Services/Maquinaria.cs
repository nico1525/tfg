using API.Models.Autentificacion;
using API.Models;
using Newtonsoft.Json;
using System.Text;
using Web.Helpers;

namespace Web.Services
{
    public interface IMaquinariaServices
    {
        public Task<List<MaquinariaDTO>?> GetMaquinaria();
        public Task<MaquinariaDTO?> GetMaquinariaById(int id);
        public Task<string> PostMaquinaria(MaquinariaCreateDTO org);
        public Task<string> DeleteMaquinaria(int id);
        public Task<string> UpdateMaquinariaPorId(int id, MaquinariaModifyDTO org);
    }

    public class MaquinariaServices : IMaquinariaServices
    {
        private readonly HttpClient _httpClient;
        private static readonly string baseUrl = "https://localhost:7011/";

        public MaquinariaServices(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {Token.token}");
        }
        public async Task<List<MaquinariaDTO>?> GetMaquinaria()
        {
            var response = await _httpClient.GetAsync(baseUrl + "api/Organizacion/Maquinaria");
            if (response.IsSuccessStatusCode)
            {
                var resultString = await response.Content.ReadAsStringAsync();
                var list = JsonConvert.DeserializeObject<IEnumerable<MaquinariaDTO>>(resultString);
                List<MaquinariaDTO> lista = new List<MaquinariaDTO>();
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

        public async Task<MaquinariaDTO?> GetMaquinariaById(int id)
        {
            var response = await _httpClient.GetAsync(baseUrl + "api/Organizacion/Maquinaria/" + id);

            if (response.IsSuccessStatusCode)
            {
                var resultString = await response.Content.ReadAsStringAsync();
                var Maquinaria = JsonConvert.DeserializeObject<MaquinariaDTO>(resultString);
                return Maquinaria;
            }
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception(response.Content.ReadAsStringAsync().Result);
            }
            return null;
        }
        public async Task<string> PostMaquinaria(MaquinariaCreateDTO org)
        {
            var orgJson =
                new StringContent(JsonConvert.SerializeObject(org), Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(baseUrl + "api/Organizacion/Maquinaria", orgJson);
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
            var response = await _httpClient.DeleteAsync(baseUrl + "api/Organizacion/Maquinaria/" + id);
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
            var response = await _httpClient.PutAsJsonAsync(baseUrl + "api/Organizacion/Maquinaria/" + id, org);
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

