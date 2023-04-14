using System.Net.Http;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;
using System.Diagnostics.Metrics;
using API.Models;
using Mysqlx.Session;
using MySqlX.XDevAPI;
using System.Text;

namespace Web.Services
{
    public class OrganizacionServices : IOrganizacionServices
    {
        private readonly HttpClient _httpClient;

        public OrganizacionServices(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<IEnumerable<Organizacion>> GetAllOrgs()
        {
            return await JsonSerializer.DeserializeAsync<IEnumerable<Organizacion>>
                (await _httpClient.GetStreamAsync($"api/Organizacions"), new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
        }

        public async Task<Organizacion> GetOrgByID(int orgId)
        {
            return await JsonSerializer.DeserializeAsync<Organizacion>
                (await _httpClient.GetStreamAsync($"api/Organizacions{orgId}"), new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
        }

        public async Task<Organizacion?> PostOrganizacion(Organizacion org)
        {
            var orgJson =
                new StringContent(JsonSerializer.Serialize(org), Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("api/Organizacions", orgJson);

            if (response.IsSuccessStatusCode)
            {
                return await JsonSerializer.DeserializeAsync<Organizacion>(await response.Content.ReadAsStreamAsync());
            }

            return null;
        }
    }
}
