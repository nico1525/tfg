using API.Models;
using System.Text.Json;
using System.Text;
using API.Models.Autentificacion;
using Microsoft.AspNetCore.Mvc;

namespace Web.Services
{
    public interface IUsuarioServices
    {
        public Task<IEnumerable<UsuarioDTO>?> GetUsuario();
        public Task<IEnumerable<UsuarioDTO>?> GetAllUsuario();
        public Task<ActionResult<Usuario>> RegistrarUsuario(UsuarioCreateDTO org);
        public Task<LoginUserResponse?> LoginUsuario(LoginRequest org);
        public Task<IActionResult> DeleteUsuario(string id);
        public Task<IActionResult?> UpdateUsuarioActual(UsuarioModifyDTO org);
        public Task<IActionResult> UpdateUsuarioPorId(string id, UsuarioModifyDTO org);
    }

    public class UsuarioServices : IUsuarioServices
    {
        private readonly HttpClient _httpClient;

        public UsuarioServices(HttpClient httpClient)
        {
            _httpClient = httpClient;
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

        public async Task<ActionResult<Usuario>?> RegistrarUsuario(UsuarioCreateDTO org)
        {
            var orgJson =
                new StringContent(JsonSerializer.Serialize(org), Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("api/Organizacion/Usuario/register", orgJson);
            if (response.IsSuccessStatusCode)
            {
                return await JsonSerializer.DeserializeAsync<ActionResult<Usuario>>(await response.Content.ReadAsStreamAsync());
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

        public async Task<IActionResult> DeleteUsuario(string id)
        {
            var response = await _httpClient.DeleteAsync("api/Organizacion/Usuario/" + id);
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception(response.Content.ReadAsStringAsync().Result);
            }
            if (response.IsSuccessStatusCode)
            {
                return await JsonSerializer.DeserializeAsync<IActionResult>(await response.Content.ReadAsStreamAsync());
            }
            return null;
        }

        public async Task<IActionResult?> UpdateUsuarioActual(UsuarioModifyDTO org)
        {
            var response = await _httpClient.PutAsJsonAsync("$api/Organizacion/Usuario", org);
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception(response.Content.ReadAsStringAsync().Result);
            }
            if (response.IsSuccessStatusCode)
            {
                return await JsonSerializer.DeserializeAsync<IActionResult>(await response.Content.ReadAsStreamAsync());
            }
            return null;
        }
        public async Task<IActionResult> UpdateUsuarioPorId(string id, UsuarioModifyDTO org)
        {
            var response = await _httpClient.PutAsJsonAsync("$api/Organizacion/Usuario/" + id, org);
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception(response.Content.ReadAsStringAsync().Result);
            }
            if (response.IsSuccessStatusCode)
            {
                return await JsonSerializer.DeserializeAsync<IActionResult>(await response.Content.ReadAsStreamAsync());
            }
            return null;
        }
    }
}
