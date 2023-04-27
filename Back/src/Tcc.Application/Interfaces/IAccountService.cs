using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Tcc.Application.Dtos;

namespace Tcc.Application.Interface
{
    public interface IAccountService
    {
        Task<bool> UserExists(string username);
        Task<UserUpdateDto> GetUserByUserNameAsync(string username);
        Task<SignInResult> CheckUserPasswordAsync(UserUpdateDto userUpdateDto, string password);
        Task<UserUpdateDto> CreateAccountAsync(UserDto userDto);
        Task<UserUpdateDto> UpdateAccount(UserUpdateDto userUpdateDto);
    }
}