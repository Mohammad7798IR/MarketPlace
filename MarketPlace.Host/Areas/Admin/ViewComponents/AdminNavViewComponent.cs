using Infa.Application.Extentions;
using Infa.Application.Interfaces;
using Infa.Domain.ViewModels.Admin;
using Microsoft.AspNetCore.Mvc;

namespace MarketPlace.Host.Areas.Admin.ViewComponents
{
    public class AdminNavViewComponent : ViewComponent
    {
        private readonly IUserServices _userServices;

        public AdminNavViewComponent(IUserServices userServices)
        {
            _userServices = userServices;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        
        {
            var user = await _userServices.GetUserAsync(UserClaimsPrincipal.GetUserId());

            var adminVM = new AdminDetailsVM()
            {
                FullName = user.FirstName + " " + user.LastName,
                Avatar   = user.Avatar,
                Email    = user.Email,
            };


            return View("_AdminNaveBarSection", adminVM);
        }
    }
}
