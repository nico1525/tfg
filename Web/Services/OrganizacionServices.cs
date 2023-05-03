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
                (await _httpClient.GetStreamAsync($"api/Organizacion"), new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
        }

        public async Task<OrganizacionDTO> GetOrgByID(int orgId)
        {
            return await JsonSerializer.DeserializeAsync<OrganizacionDTO>
                (await _httpClient.GetStreamAsync($"api/Organizacion{orgId}"), new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
        }

        public async Task<Organizacion?> PostOrganizacion(Organizacion org)
        {
            var orgJson =
                new StringContent(JsonSerializer.Serialize(org), Encoding.UTF8, "application/json");

                var response = await _httpClient.PostAsync("api/Organizacion", orgJson);
                if (response.IsSuccessStatusCode)
                {
                    return await JsonSerializer.DeserializeAsync<Organizacion>(await response.Content.ReadAsStreamAsync());
                }
                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception(response.Content.ReadAsStringAsync().Result);
                }
            
            return null;
        }

        public async Task<Organizacion?> DeleteOrganizacion(string id)
        {
            var response = await _httpClient.DeleteAsync("api/Organizacion/"+ id);
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception(response.Content.ReadAsStringAsync().Result);
            }
            if (response.IsSuccessStatusCode)
            {
                return await JsonSerializer.DeserializeAsync<Organizacion>(await response.Content.ReadAsStreamAsync());
            }
            return null;
        }

        public async Task<Organizacion?> UpdateOrganizacion(string id, Organizacion org)
        {
            var response = await _httpClient.PutAsJsonAsync("$api/Organizacion/" + id, org);
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception(response.Content.ReadAsStringAsync().Result);
            }
            if (response.IsSuccessStatusCode)
            {
                return await JsonSerializer.DeserializeAsync<Organizacion>(await response.Content.ReadAsStreamAsync());
            }
            return null;
        }
    }
}
