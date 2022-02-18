using Infa.Domain.Models.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infa.Domain.Interfaces
{
    public partial interface IUserRepositories
    {
        //Task CreateUser(ApplicationUser user);
        Task<bool> UserExistsByEmail(string email);

        Task<bool> UserExistsByPhone(string phone);

        Task<bool> UserExistsByUserName(string userName);

        Task<bool> UserExistsByActiveCode(string activeCode);

        Task<ApplicationUser> GetByEmail(string email);

        Task<ApplicationUser> GetByActiveCode(string activeCode);  

       

    }
    public partial interface IUserRepositories
    {
        void UpdateUser(ApplicationUser user);

        Task SaveChanges();
    }
}
