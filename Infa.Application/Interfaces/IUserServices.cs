using Infa.Domain.Models.Identity;
using Infa.Domain.ViewModels.Admin;
using Infa.Domain.ViewModels.PublicAuth;
using Infa.Domain.ViewModels.Userpanel;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infa.Application.Interfaces
{
    public partial interface IUserServices
    {
        Task<RegisterUserResult> Register(RegisterUserVM userVM);

        Task<LoginUserResult> Login(LoginUserVM userVM);

        Task<ForgotPasswordResult> ForgotPassword(ForgotPasswordUserVM userVM);

        Task<ResetPasswordResult> ResetPassword(ResetPasswordUserVM userVM);

        Task<ActiveAccountResult> ActiveAccount(string Id);

        Task<ApplicationUser> GetByEmail(string email);

        Task<bool> ExitsByActiveCode(string Code);
    }


    //UserPanel
    public partial interface IUserServices
    {
        Task<UserDetailsVM> GetUserDetails(string userId);

        Task<UserDetailsResult> UpdateUserDetails(UserDetailsVM userDetailsVM , string userId, IFormFile userAvatar);

        Task<ApplicationUser> GetUserAsync(string userId);
    }


    //AdminPanel
    public partial interface IUserServices
    {
        Task<AvatarResult> UpdateAvatar(IFormFile postedAvatar, string userId);
    }
}
