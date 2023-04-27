using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Tcc.Domain.Identity;
using Tcc.Persistence.Contextos;
using Tcc.Persistence.Interface;

namespace Tcc.Persistence
{
    public class UserPersist : GeralPersist, IUserPersist
    {
        private readonly TccContext _context;

        public UserPersist(TccContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<User>> GetUsersAsync()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<User> GetUserByIdAsync(int id)
        {
            return await _context.Users.FindAsync(id);
        }

        public async Task<User> GetUserByUserNameAsync(string userName)
        {
            return await _context.Users
                                 .SingleOrDefaultAsync(user => user.UserName == userName.ToLower());
        }
    }
}