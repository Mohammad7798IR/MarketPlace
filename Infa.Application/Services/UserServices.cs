using Common.ClassHelpers;
using DataFramework.Security;
using Infa.Application.Extentions;
using Infa.Application.Interfaces;
using Infa.Application.Utils;
using Infa.Domain.Interfaces;
using Infa.Domain.Models.Identity;
using Infa.Domain.ViewModels.Admin;
using Infa.Domain.ViewModels.PublicAuth;
using Infa.Domain.ViewModels.Userpanel;
using MarketPlace.Application.Extensions;
using MarketPlace.Application.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using SendEmail;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infa.Application.Services
{
    public partial class UserServices
    {
        private readonly IUserRepositories _userRepositories;

        private readonly UserManager<ApplicationUser> _userManager;

        private readonly IViewRenderService _viewRenderService;

        private readonly IConfiguration _configuration;

        public UserServices
            (
            IUserRepositories userRepositories,
            UserManager<ApplicationUser> userManager,
            IViewRenderService viewRenderService,
            IConfiguration configuration
            )
        {
            _userRepositories = userRepositories;
            _userManager = userManager;
            _viewRenderService = viewRenderService;
            _configuration = configuration;
        }
    }

    public partial class UserServices : IUserServices
    {


        public async Task<RegisterUserResult> Register(RegisterUserVM userVM)
        {
            if (await _userRepositories.UserExistsByEmail(userVM.Email))
            {
                return RegisterUserResult.EmailExists;
            }


            List<ApplicationUserRole> roles = new List<ApplicationUserRole>();



            ApplicationUser newUser = new ApplicationUser()
            {
                Id = Guid.NewGuid().ToString(),
                ActiveCode = Guid.NewGuid().ToString(),
                PasswordHash = HashGenerator.GenerateHash(userVM.Password),
                Email = userVM.Email,
                FirstName = userVM.FirstName,
                LastName = userVM.LastName,
                UserName = "DefaultUser" + $"{Random.Shared.Next(1, 1000000)}",
                CreatedAt = PersianDateTime.Now(),
                Gender = Gender.unknown,
                Avatar = "avatar.jpg"
            };

            var userRole = new ApplicationUserRole();


            userRole.Id     = Guid.NewGuid().ToString();
            userRole.UserId = newUser.Id;
            userRole.RoleId = _configuration.GetSection("Roles")["AplicationUser"];


            List<ApplicationUserRole> lst = new List<ApplicationUserRole>();

            lst.Add(userRole);

            newUser.UserRoles = lst;

            var result = await _userManager.CreateAsync(newUser);

            if (result.Succeeded)
            {
                string template = _viewRenderService.RenderToStringAsync("ActiveEmail", newUser);
                EmailSender sender = new EmailSender(_configuration);
                sender.Send(userVM.Email, "فعال سازی", template);
                return RegisterUserResult.Success;
            }

            return RegisterUserResult.Fail;
        }

        public async Task<LoginUserResult> Login(LoginUserVM userVM)
        {
            var user = await _userRepositories.GetUserByEmail(userVM.Email);

            if (user != null)
            {
                if (user.EmailConfirmed == true)
                {
                    if (user.PasswordHash == HashGenerator.GenerateHash(userVM.Password))
                    {
                        return LoginUserResult.Success;
                    }
                    return LoginUserResult.NotFound;
                }
                return LoginUserResult.InActive;
            }


            return LoginUserResult.NotFound;
        }

        public async Task<ActiveAccountResult> ActiveAccount(string Id)
        {
            var user = await _userRepositories.GetByActiveCode(Id);
            if (user != null)
            {
                user.EmailConfirmed = true;
                user.ActiveCode = Guid.NewGuid().ToString();
                user.UpdatedAt = PersianDateTime.Now();
                _userRepositories.UpdateUser(user);
                await _userRepositories.SaveChanges();
                return ActiveAccountResult.Success;
            }
            return ActiveAccountResult.NotFound;
        }

        public async Task<ForgotPasswordResult> ForgotPassword(ForgotPasswordUserVM userVM)
        {
            var user = await _userRepositories.GetUserByEmail(userVM.Email);

            if (user != null)
            {
                if (user.EmailConfirmed)
                {
                    try
                    {
                        string template = _viewRenderService.RenderToStringAsync("ForgotPasswordEmail", user);
                        EmailSender sender = new EmailSender(_configuration);
                        sender.Send(userVM.Email, "تغییر رمز عبور", template);
                        return ForgotPasswordResult.Success;
                    }
                    catch (Exception)
                    {

                        return ForgotPasswordResult.Fail;
                    }

                }
                return ForgotPasswordResult.InActive;
            }

            return ForgotPasswordResult.NotFound;

        }

        public async Task<ResetPasswordResult> ResetPassword(ResetPasswordUserVM userVM)
        {
            var user = await _userRepositories.GetByActiveCode(userVM.ActiveCode);
            if (user != null)
            {
                user.PasswordHash = HashGenerator.GenerateHash(userVM.Password);
                user.ActiveCode = Guid.NewGuid().ToString();
                user.UpdatedAt = PersianDateTime.Now();



                _userRepositories.UpdateUser(user);
                await _userRepositories.SaveChanges();
                return ResetPasswordResult.Success;
            }

            return ResetPasswordResult.Fail;
        }


    }


    //Return Results From Repository 
    //Doesnt Have Any Logic Behind It

    public partial class UserServices
    {
        public async Task<bool> ExitsByActiveCode(string activeCode)
        {
            return await _userRepositories.UserExistsByActiveCode(activeCode);
        }

        public Task<ApplicationUser> GetByEmail(string email)
        {
            return _userRepositories.GetUserByEmail(email);
        }
    }


    //UserPanel 
    public partial class UserServices
    {
        public async Task<UserDetailsVM> GetUserDetails(string userId)
        {
            var user = _userManager.FindByIdAsync(userId).Result;

            if (user == null)
            {
                return null;
            }

            UserDetailsVM detailsVM = new UserDetailsVM();

            detailsVM.Username = user.UserName;
            detailsVM.Email = user.Email;
            detailsVM.Gender = user.Gender;
            detailsVM.PhoneNumber = user.PhoneNumber;
            detailsVM.FirstName = user.FirstName;
            detailsVM.LastName = user.LastName;
            detailsVM.ImageName = user.Avatar;

            return detailsVM;

        }

        public async Task<UserDetailsResult> UpdateUserDetails(UserDetailsVM userDetailsVM, string userId, IFormFile userAvatar)
        {
            var user = _userManager.FindByIdAsync(userId).Result;

            if (user == null)
                return UserDetailsResult.NotFound;

            if (userAvatar == null || !CheckContentImage.IsImage(userAvatar))
            {
                return UserDetailsResult.NotImage;
            }

            var imageName = Guid.NewGuid().ToString("N") + Path.GetExtension(userAvatar.FileName);
            userAvatar.AddImageToServer(imageName, FilePath.UserAvatarOriginServer, 100, 100, FilePath.UserAvatarThumbServer, user.Avatar);

            user.Avatar = imageName;
            user.UpdatedAt = PersianDateTime.Now();
            user.UserName = userDetailsVM.Username;
            user.PhoneNumber = userDetailsVM.PhoneNumber;
            user.FirstName = userDetailsVM.FirstName;
            user.LastName = userDetailsVM.LastName;
            user.PasswordHash = HashGenerator.GenerateHash(userDetailsVM.Password);
            user.Gender = userDetailsVM.Gender;


            _userRepositories.UpdateUser(user);
            await _userRepositories.SaveChanges();


            return UserDetailsResult.Success;
        }

        public async Task<ApplicationUser> GetUserAsync(string userId)
        {
            var user = _userManager.FindByIdAsync(userId).Result;
            return user;
        }
    }



    //AdminPanel 
    public partial class UserServices
    {
        public async Task<AvatarResult> UpdateAvatar(IFormFile postedAvatar, string userId)
        {
            var user = _userManager.FindByIdAsync(userId).Result;

            if (user == null) return AvatarResult.notFound;

            if (!CheckContentImage.IsImage(postedAvatar) || postedAvatar == null) return AvatarResult.notImage;

            var imageName = Guid.NewGuid().ToString("N") + Path.GetExtension(postedAvatar.FileName);

            postedAvatar.AddImageToServer(imageName, FilePath.UserAvatarOriginServer, 100, 100, FilePath.UserAvatarThumbServer, user.Avatar);

            user.Avatar    = imageName;
            user.UpdatedAt = PersianDateTime.Now();


            _userRepositories.UpdateUser(user);
            await _userRepositories.SaveChanges();

            return AvatarResult.success;


        }
    }
}
