using API.Models;
using System.Text;
using API.Models.Autentificacion;
using Web.Helpers;
using Newtonsoft.Json;
using Azure;
using API.Models.Query_Models;

namespace Web.Services
{
    public interface IUsuarioServices
    {
        public Task<InfoUsuario> GetUsuario();
        public Task<List<UsuarioDTO>?> GetAllUsuario();
        public Task<UsuarioDTO?> GetUsuarioById(int id);
        public Task<string> RegistrarUsuario(UsuarioCreateDTO org);
        public Task<LoginUserResponse?> LoginUsuario(LoginRequest org);
        public Task<string> DeleteUsuario(int id);
        public Task<string?> UpdateUsuarioActual(UsuarioModifyDTO org);
        public Task<string> UpdateUsuarioPorId(int id, UsuarioModifyDTO org);
    }

    public class UsuarioServices : IUsuarioServices
    {
        private readonly HttpClient _httpClient;
        //private readonly HttpClient _httpClientAnonymous;
        private static readonly string baseUrl = "https://localhost:7011/";


        public UsuarioServices(HttpClient httpClient)
        { 
            _httpClient = httpClient;
            _httpClient.DefaultRequestHeaders.Clear();
            _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {Storage.token}");
        }
        public async Task<InfoUsuario> GetUsuario()
        {
            var response = await _httpClient.GetAsync(baseUrl + "api/Organizacion/Usuario");
            if (response.IsSuccessStatusCode)
            {
                var resultString = await response.Content.ReadAsStringAsync();
                var usuario = JsonConvert.DeserializeObject<InfoUsuario>(resultString);
                return usuario;
            }
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception(response.Content.ReadAsStringAsync().Result);
            }
            return null;
        }
        public async Task<UsuarioDTO?> GetUsuarioById(int id)
        {
            var response = await _httpClient.GetAsync(baseUrl + "api/Organizacion/Usuario/" + id);

            if (response.IsSuccessStatusCode)
            {
                var resultString = await response.Content.ReadAsStringAsync();
                var usuario = JsonConvert.DeserializeObject<UsuarioDTO>(resultString);
                return usuario;
            }
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception(response.Content.ReadAsStringAsync().Result);
            }
            return null;
        }
            public async Task<List<UsuarioDTO>?> GetAllUsuario()
        {
            var response = await _httpClient.GetAsync(baseUrl + "api/Organizacion/Usuario/all");
            if (response.IsSuccessStatusCode)
            {
                var resultString = await response.Content.ReadAsStringAsync();
                var list = JsonConvert.DeserializeObject<IEnumerable<UsuarioDTO>>(resultString);
                List<UsuarioDTO> lista = new List<UsuarioDTO>();
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

        public async Task<string> RegistrarUsuario(UsuarioCreateDTO org)
        {
            var orgJson =
                new StringContent(JsonConvert.SerializeObject(org), Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(baseUrl + "api/Organizacion/Usuario/register", orgJson);
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
        public async Task<LoginUserResponse?> LoginUsuario(LoginRequest org)
        {
            var orgJson =
                new StringContent(JsonConvert.SerializeObject(org), Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(baseUrl + "api/Organizacion/Usuario/login", orgJson);
            if (response.IsSuccessStatusCode)
            {
                var resultString = await response.Content.ReadAsStringAsync();
               return JsonConvert.DeserializeObject<LoginUserResponse>(resultString);
            }
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception(response.Content.ReadAsStringAsync().Result);
            }

            return null;
        }

        public async Task<string> DeleteUsuario(int id)
        {
            var response = await _httpClient.DeleteAsync(baseUrl + "api/Organizacion/Usuario/" + id);
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception(response.Content.ReadAsStringAsync().Result);
            }
            if (response.IsSuccessStatusCode)
            {
                return response.Content.ReadAsStringAsync().Result;
            }
            return null;
        }

        public async Task<string?> UpdateUsuarioActual(UsuarioModifyDTO org)
        {
            var response = await _httpClient.PutAsJsonAsync(baseUrl + "api/Organizacion/Usuario", org);
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception(response.Content.ReadAsStringAsync().Result);
            }
            if (response.IsSuccessStatusCode)
            {
                return response.Content.ReadAsStringAsync().Result;
            }
            return null;
        }
        public async Task<string> UpdateUsuarioPorId(int id, UsuarioModifyDTO org)
        {
            var response = await _httpClient.PutAsJsonAsync(baseUrl + "api/Organizacion/Usuario/" + id, org);
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception(response.Content.ReadAsStringAsync().Result);
            }
            if (response.IsSuccessStatusCode)
            {
                return response.Content.ReadAsStringAsync().Result;
            }
            return null;
        }
    }
}
