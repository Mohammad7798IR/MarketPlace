using Infa.Data.Context;
using Infa.Domain.Interfaces;
using Infa.Domain.Models.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infa.Data.Repositories
{
    public partial class UserRepositories : IUserRepositories
    {
        private readonly MarketPlaceDBContext _context;

        public UserRepositories(MarketPlaceDBContext context)
        {
            _context = context;
        }

    
    }
    public partial class UserRepositories 
    {
        public async Task<ApplicationUser> GetByActiveCode(string activeCode)
        {
           return await _context.Users.AsQueryable()
                .SingleOrDefaultAsync(u=> u.ActiveCode == activeCode);
        }

        public async Task<bool> UserExistsByEmail(string email)
        {
           return await _context.Users.AsQueryable()
                .AnyAsync(e=>e.Email == email);
        }

        public async Task<bool> UserExistsByPhone(string phone)
        {
            return await _context.Users.AsQueryable()
                .AnyAsync(e=>e.PhoneNumber == phone);
        }

        public async Task<bool> UserExistsByUserName(string userName)
        {
            return await _context.Users.AsQueryable()
              .AnyAsync(e => e.UserName == userName);
        }

        public async Task<ApplicationUser> GetByEmail(string email)
        {
           return await _context.Users.AsNoTracking().AsQueryable()
                .Include(ur=>ur.UserRoles).Where(e=>e.Email==email).SingleOrDefaultAsync();

        }

        public Task<bool> UserExistsByActiveCode(string activeCode)
        {
            return _context.Users.AsQueryable()
                .AnyAsync(_ => _.ActiveCode == activeCode);
        }
    }




    public partial class UserRepositories
    {
        public void UpdateUser(ApplicationUser user)
        {
            _context.Users.Update(user);
        }

        public async Task SaveChanges()
        {
            await _context.SaveChangesAsync();
        }
    }
}
