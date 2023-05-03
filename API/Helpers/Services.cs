using API.Authorization;
using API.Models;
using API.Models.Context;
using Microsoft.Extensions.Options;

namespace API.Helpers
{
    public interface IServices
    {
        Organizacion GetOrganizacionById(int id);
    }

    public class Services : IServices
    {
        private DatabaseContext _context;

        public Services(DatabaseContext context)
        {
            _context = context;
        }
        public Organizacion GetOrganizacionById(int id)
        {
            var org = _context.Organizacion.Find(id);
            if (org == null) throw new Exception("User not found");
            return org;
        }
    }
}
