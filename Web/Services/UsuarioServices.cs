using API.Models;
using System.Text.Json;
using System.Text;
using API.Models.Autentificacion;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using Microsoft.AspNetCore.Identity;
using Web.Helpers;

namespace Web.Services
{
    public interface IUsuarioServices
    {
        public Task<IEnumerable<UsuarioDTO>?> GetUsuario();
        public Task<IEnumerable<UsuarioDTO>?> GetAllUsuario();
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


        public UsuarioServices(HttpClient httpClient)
        { 
            _httpClient = httpClient;
            //_httpClientAnonymous = httpClient;  Por si para las llamadas anonimas no se tuviera que enviar token
            _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {Token.token}");
        }
        public async Task<IEnumerable<UsuarioDTO>?> GetUsuario()
        {
            return await JsonSerializer.DeserializeAsync<IEnumerable<UsuarioDTO>>
                (await _httpClient.GetStreamAsync($"api/Organizacion/Usuario"), new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
        }
        public async Task<IEnumerable<UsuarioDTO>?> GetAllUsuario()
        {
            return await JsonSerializer.DeserializeAsync<IEnumerable<UsuarioDTO>>
                (await _httpClient.GetStreamAsync($"api/Organizacion/Usuario/all"), new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
        }

        public async Task<string> RegistrarUsuario(UsuarioCreateDTO org)
        {
            var orgJson =
                new StringContent(JsonSerializer.Serialize(org), Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("api/Organizacion/Usuario/register", orgJson);
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
                new StringContent(JsonSerializer.Serialize(org), Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("api/Organizacion/Usuario/login", orgJson);
            if (response.IsSuccessStatusCode)
            {
                return await JsonSerializer.DeserializeAsync<LoginUserResponse>(await response.Content.ReadAsStreamAsync());
            }
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception(response.Content.ReadAsStringAsync().Result);
            }

            return null;
        }

        public async Task<string> DeleteUsuario(int id)
        {
            var response = await _httpClient.DeleteAsync("api/Organizacion/Usuario/" + id);
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
            var response = await _httpClient.PutAsJsonAsync("$api/Organizacion/Usuario", org);
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
            var response = await _httpClient.PutAsJsonAsync("$api/Organizacion/Usuario/" + id, org);
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
