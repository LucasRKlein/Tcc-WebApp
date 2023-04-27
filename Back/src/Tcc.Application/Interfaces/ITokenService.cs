using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tcc.Application.Dtos;

namespace Tcc.Application.Interface
{
    public interface ITokenService
    {
        Task<string> CreateToken(UserUpdateDto userUpdateDto);
    }
}