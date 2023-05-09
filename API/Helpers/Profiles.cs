using API.Models;
using API.Models.Autentificacion;
using API.Models.Consumos;
using AutoMapper;

namespace API.Helpers
{
    public class OrganizacionProfile : Profile
    {
        public OrganizacionProfile()
        {
            CreateMap<Organizacion, OrganizacionDTO>();
            CreateMap<OrganizacionCreateDTO, Organizacion>();

            CreateMap<Usuario, UsuarioDTO>();
            CreateMap<UsuarioCreateDTO, Usuario>();
            CreateMap<LoginRequest, Usuario>();
            CreateMap<Usuario, LoginUserResponse>();

            CreateMap<Vehiculo, TransporteDTO>();
            CreateMap<VehiculoCreateDTO, Vehiculo>();

            CreateMap<VehiculoConsumo, VehiculoConsumoDTO>();
            CreateMap<VehiculoConsumoCreateDTO, VehiculoConsumo>();

            CreateMap<Transporte, TransporteDTO>();
            CreateMap<TransporteCreateDTO, Transporte>();

            CreateMap<TransporteConsumo, TransporteConsumoDTO>();
            CreateMap<TransporteConsumoCreateDTO, TransporteConsumo>();

            CreateMap<EmisionesFugitivas, EmisionesFugitivasDTO>();
            CreateMap<EmisionesFugitivasDTO, EmisionesFugitivas>();

            CreateMap<EmisionesFugitivas, EmisionesFugitivasDTO>();
            CreateMap<EmisionesFugitivasDTO, EmisionesFugitivas>();
        }
    }
}
