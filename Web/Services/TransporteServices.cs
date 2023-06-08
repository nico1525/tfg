using API.Models.Autentificacion;
using API.Models;
using Newtonsoft.Json;
using System.Text;
using Web.Helpers;

namespace Web.Services
{
    public interface ITransporteServices
    {
        public Task<List<TransporteDTO>?> GetTransporte();
        public Task<TransporteDTO?> GetTransporteById(int id);
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
            _httpClient.DefaultRequestHeaders.Clear();
            _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {Storage.token}");
        }
        public async Task<List<TransporteDTO>?> GetTransporte()
        {
            var response = await _httpClient.GetAsync(baseUrl + "api/Organizacion/Transporte");
            if (response.IsSuccessStatusCode)
            {
                var resultString = await response.Content.ReadAsStringAsync();
                var list = JsonConvert.DeserializeObject<IEnumerable<TransporteDTO>>(resultString);
                List<TransporteDTO> lista = new List<TransporteDTO>();
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
        public async Task<TransporteDTO?> GetTransporteById(int id)
        {
            var response = await _httpClient.GetAsync(baseUrl + "api/Organizacion/Transporte/" + id);

            if (response.IsSuccessStatusCode)
            {
                var resultString = await response.Content.ReadAsStringAsync();
                var Transporte = JsonConvert.DeserializeObject<TransporteDTO>(resultString);
                return Transporte;
            }
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception(response.Content.ReadAsStringAsync().Result);
            }
            return null;
        }
        public async Task<string> PostTransporte(TransporteCreateDTO org)
        {
            var orgJson =
                new StringContent(JsonConvert.SerializeObject(org), Encoding.UTF8, "application/json");

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
