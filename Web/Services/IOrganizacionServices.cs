using System.Diagnostics.Metrics;
using API.Models;
namespace Web.Services
{
    public interface IOrganizacionServices
    {
        Task<IEnumerable<Organizacion>> GetAllOrgs();
        Task<OrganizacionDTO> GetOrgByID(int orgId);
        Task<Organizacion?> PostOrganizacion(Organizacion org);
        Task<Organizacion?> DeleteOrganizacion(string name);
        Task<Organizacion?> UpdateOrganizacion(string id, Organizacion org);
    }
}
