using System.Net.Http;
using System.Collections.Generic;
using Newtonsoft.Json;
using API.Models;
using Mysqlx.Session;
using MySqlX.XDevAPI;
using System.Text;
using Web.Helpers;

namespace Web.Services
{
    public interface IOrganizacionServices
    {
        Task<OrganizacionDTO> GetOrg();
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
            _httpClient.DefaultRequestHeaders.Clear();
            _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {Storage.token}");
        }
        public async Task<OrganizacionDTO> GetOrg()
        {
            var response = await _httpClient.GetAsync(baseUrl + "api/Organizacion");
            if (response.IsSuccessStatusCode)
            {
                var resultString = await response.Content.ReadAsStringAsync();
                var list = JsonConvert.DeserializeObject<IEnumerable<OrganizacionDTO>>(resultString);
                OrganizacionDTO org = new OrganizacionDTO();
                org = list.FirstOrDefault();
                return org;
            }
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception(response.Content.ReadAsStringAsync().Result);
            }
            return null;
        }

        public async Task<string> PostOrganizacion(OrganizacionCreateDTO org)
        {
            var orgJson =
                new StringContent(JsonConvert.SerializeObject(org), Encoding.UTF8, "application/json");

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
