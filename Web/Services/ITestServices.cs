using System.Diagnostics.Metrics;
using API.Models;
namespace Web.Services
{
    public interface ITestServices
    {
        Task<IEnumerable<Hoja>> GetAllCountries();
        Task<Hoja> GetCountryById(int countryId);
    }
}
