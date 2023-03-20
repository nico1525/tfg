using System.Net.Http;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;
using System.Diagnostics.Metrics;
using API.Models;

namespace Web.Services
{
    public class TestServices : ITestServices
    {
        private readonly HttpClient _httpClient;

        public TestServices(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<IEnumerable<Hoja>> GetAllCountries()
        {
            return await JsonSerializer.DeserializeAsync<IEnumerable<Hoja>>
                (await _httpClient.GetStreamAsync($"api/Hojas"), new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
        }

        public async Task<Hoja> GetCountryById(int countryId)
        {
            return await JsonSerializer.DeserializeAsync<Hoja>
                (await _httpClient.GetStreamAsync($"api/Hojas{countryId}"), new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
        }
    }
}
