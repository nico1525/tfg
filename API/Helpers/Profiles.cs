using API.Models;
using API.Models.Autentificacion;
using API.Models.DTOs.Incoming;
using API.Models.DTOs.Outcoming;
using AutoMapper;

namespace API.Helpers
{
    public class OrganizacionProfile : Profile
    {
        public OrganizacionProfile()
        {
            CreateMap<Organizacion, OrganizacionDTO>();
            CreateMap<OrganizacionCreateDTO, Organizacion>();
            CreateMap<LoginRequest, Organizacion>();
            CreateMap<Organizacion, LoginResponse>();
        }
    }
}
