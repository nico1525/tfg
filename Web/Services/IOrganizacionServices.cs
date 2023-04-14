using System.Diagnostics.Metrics;
using API.Models;
namespace Web.Services
{
    public interface IOrganizacionServices
    {
        Task<IEnumerable<Organizacion>> GetAllOrgs();
        Task<Organizacion> GetOrgByID(int orgId);
        Task<Organizacion?> PostOrganizacion(Organizacion org);
    }
}
