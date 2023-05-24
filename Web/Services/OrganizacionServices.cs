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
        Task<string> PostOrganizacion(OrganizacionCreateDTO org);
        Task<string?> DeleteOrganizacion();
        Task<string> UpdateOrganizacion(OrganizacionModifyDTO org);
    }

    public class OrganizacionServices : IOrganizacionServices
    {
        private readonly HttpClient _httpClient;
        private static readonly string baseUrl = "https://localhost:7011/";
        public OrganizacionServices(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<IEnumerable<OrganizacionDTO>?> GetOrg()
        {
            return await JsonSerializer.DeserializeAsync<IEnumerable<OrganizacionDTO>>
                (await _httpClient.GetStreamAsync(baseUrl + "api/Organizacion"), new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
        }

        public async Task<string> PostOrganizacion(OrganizacionCreateDTO org)
        {
            var orgJson =
                new StringContent(JsonSerializer.Serialize(org), Encoding.UTF8, "application/json");

                var response = await _httpClient.PostAsync(baseUrl + "api/Organizacion/register", orgJson);
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

        public async Task<string> DeleteOrganizacion()
        {
            var response = await _httpClient.DeleteAsync(baseUrl + "api/Organizacion");
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

        public async Task<string> UpdateOrganizacion(OrganizacionModifyDTO org)
        {
            var response = await _httpClient.PutAsJsonAsync(baseUrl + "api/Organizacion", org);
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
