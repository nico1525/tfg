using System.Net.Http;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;
using System.Diagnostics.Metrics;
using API.Models;
using Mysqlx.Session;
using MySqlX.XDevAPI;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Web.Services
{
    public interface IOrganizacionServices
    {
        Task<IEnumerable<OrganizacionDTO>> GetOrg();
        Task<ActionResult<Organizacion>> PostOrganizacion(OrganizacionCreateDTO org);
        Task<IActionResult?> DeleteOrganizacion();
        Task<IActionResult> UpdateOrganizacion(OrganizacionModifyDTO org);
    }

    public class OrganizacionServices : IOrganizacionServices
    {
        private readonly HttpClient _httpClient;

        public OrganizacionServices(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<IEnumerable<OrganizacionDTO>?> GetOrg()
        {
            return await JsonSerializer.DeserializeAsync<IEnumerable<OrganizacionDTO>>
                (await _httpClient.GetStreamAsync($"api/Organizacion"), new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
        }

        public async Task<ActionResult<Organizacion>> PostOrganizacion(OrganizacionCreateDTO org)
        {
            var orgJson =
                new StringContent(JsonSerializer.Serialize(org), Encoding.UTF8, "application/json");

                var response = await _httpClient.PostAsync("api/Organizacion/register", orgJson);
                if (response.IsSuccessStatusCode)
                {
                    return await JsonSerializer.DeserializeAsync<ActionResult<Organizacion>>(await response.Content.ReadAsStreamAsync());
                }
                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception(response.Content.ReadAsStringAsync().Result);
                }
            
            return null;
        }

        public async Task<IActionResult> DeleteOrganizacion()
        {
            var response = await _httpClient.DeleteAsync("api/Organizacion");
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

        public async Task<IActionResult> UpdateOrganizacion(OrganizacionModifyDTO org)
        {
            var response = await _httpClient.PutAsJsonAsync("$api/Organizacion", org);
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
