using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tcc.Domain.Identity;

namespace Tcc.Persistence.Interface
{
    public interface IUserPersist : IGeralPersist
    {
        Task<IEnumerable<User>> GetUsersAsync();
        Task<User> GetUserByIdAsync(Guid id);
        Task<User> GetUserByUserNameAsync(string userName);
    }
}