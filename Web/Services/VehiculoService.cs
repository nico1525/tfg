using API.Models.Autentificacion;
using API.Models;
using System.Text;
using Web.Helpers;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace Web.Services
{
    public interface IVehiculoServices
    {
        public Task<List<VehiculoDTO>?> GetVehiculo();
        public Task<VehiculoDTO?> GetVehiculoById(int id);
        public Task<string> PostVehiculo(VehiculoCreateDTO org);
        public Task<string> DeleteVehiculo(int id);
        public Task<string> UpdateVehiculoPorId(int id, VehiculoModifyDTO org);
    }

    public class VehiculoServices : IVehiculoServices
    {
        private readonly HttpClient _httpClient;
        private static readonly string baseUrl = "https://localhost:7011/";


        public VehiculoServices(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {Storage.token}");
            httpClient.Timeout = TimeSpan.FromMinutes(30);
        }
        public async Task<List<VehiculoDTO>?> GetVehiculo()
        {
            
            var response = await _httpClient.GetAsync(baseUrl + "api/Organizacion/Vehiculo");
           
            if (response.IsSuccessStatusCode)
            {
                var resultString = await response.Content.ReadAsStringAsync();
                var list = JsonConvert.DeserializeObject<IEnumerable<VehiculoDTO>>(resultString);
                List<VehiculoDTO> vehiculolista = new List<VehiculoDTO>();
                foreach (var veh in list)
                {
                    vehiculolista.Add(veh);
                }
                return vehiculolista;
            }
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception(response.Content.ReadAsStringAsync().Result);
            }
            return null;
        }
        public async Task<VehiculoDTO?> GetVehiculoById(int id)
        {
            var response = await _httpClient.GetAsync(baseUrl + "api/Organizacion/Vehiculo/" + id);

            if (response.IsSuccessStatusCode)
            {
                var resultString = await response.Content.ReadAsStringAsync();
                var vehiculo = JsonConvert.DeserializeObject<VehiculoDTO>(resultString);
                return vehiculo;
            }
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception(response.Content.ReadAsStringAsync().Result);
            }
            return null;
        }

        public async Task<string> PostVehiculo(VehiculoCreateDTO org)
        {
            var orgJson =
                new StringContent(JsonConvert.SerializeObject(org), Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(baseUrl + "api/Organizacion/Vehiculo", orgJson);
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

        public async Task<string> DeleteVehiculo(int id)
        {
            var response = await _httpClient.DeleteAsync(baseUrl + "api/Organizacion/Vehiculo/" + id);
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
        public async Task<string> UpdateVehiculoPorId(int id, VehiculoModifyDTO org)
        {
            var response = await _httpClient.PutAsJsonAsync(baseUrl + "api/Organizacion/Vehiculo/" + id, org);
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
