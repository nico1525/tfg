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

            CreateMap<Vehiculo, VehiculoDTO>();
            CreateMap<VehiculoCreateDTO, Vehiculo>();

            CreateMap<VehiculoConsumo, VehiculoConsumoDTO>();
            CreateMap<VehiculoConsumoCreateDTO, VehiculoConsumo>();

            CreateMap<Transporte, VehiculoDTO>();
            CreateMap<TransporteCreateDTO, Transporte>();

            CreateMap<TransporteConsumo, TransporteConsumoDTO>();
            CreateMap<TransporteConsumoCreateDTO, TransporteConsumo>();

            CreateMap<EmisionesFugitivas, EmisionesFugitivasDTO>();
            CreateMap<EmisionesFugitivasCreateDTO, EmisionesFugitivas>();

            CreateMap<EmisionesFugitivasConsumo, EmisionesFugitivasConsumoDTO>();
            CreateMap<EmisionesFugitivasConsumoCreateDTO, EmisionesFugitivasConsumo>();

            CreateMap<Maquinaria, MaquinariaDTO>();
            CreateMap<MaquinariaCreateDTO, Maquinaria>();

            CreateMap<MaquinariaConsumo, MaquinariaConsumoDTO>();
            CreateMap<MaquinariaConsumoCreateDTO, MaquinariaConsumo>();
        }
    }
}
