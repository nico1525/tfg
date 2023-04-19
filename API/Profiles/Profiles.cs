using API.Models;
using API.Models.DTOs.Incoming;
using API.Models.DTOs.Outcoming;
using AutoMapper;

namespace API.Profiles
{
    public class OrganizacionProfile : Profile
    {
        public OrganizacionProfile()
        {
            CreateMap<Organizacion, OrganizacionDTO>();
            CreateMap<OrganizacionCreateDTO, Organizacion>();
        }
    }
}
