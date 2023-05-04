using System;
using AutoMapper;
using Tcc.Application;
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
            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<User, UserLoginDto>().ReverseMap();
            CreateMap<User, UserUpdateDto>().ReverseMap();

            CreateMap<Associado, AssociadoDto>().ReverseMap();
            CreateMap<Veiculo, VeiculoDto>().ReverseMap();
        }
    }
}