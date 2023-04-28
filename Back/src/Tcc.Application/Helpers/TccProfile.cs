using System;
using AutoMapper;
using Tcc.Application.Dtos;
using Tcc.Domain;
using Tcc.Domain.Identity;
using Tcc.Persistence.Models;

namespace Tcc.API.Helpers
{
    public class TccProfile : Profile
    {
        public TccProfile()
        {
            CreateMap<Evento, EventoDto>().ReverseMap();
            CreateMap<Lote, LoteDto>().ReverseMap();
            CreateMap<RedeSocial, RedeSocialDto>().ReverseMap();
            CreateMap<Palestrante, PalestranteDto>().ReverseMap();
            CreateMap<Palestrante, PalestranteAddDto>().ReverseMap();
            CreateMap<Palestrante, PalestranteUpdateDto>().ReverseMap();

            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<User, UserLoginDto>().ReverseMap();
            CreateMap<User, UserUpdateDto>().ReverseMap();


            CreateMap<Associado, AssociadoDto>().ReverseMap();
        }
    }
}