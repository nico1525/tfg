﻿using API.Models;
using API.Models.Autentificacion;
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
        }
    }
}
