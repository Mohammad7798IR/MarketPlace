using Infa.Application.Extentions;
using Infa.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace MarketPlace.Web.Areas.Admin.Controllers
{

    public partial class HomeController
    {
        private readonly IUserServices _userServices;

        public HomeController(IUserServices userServices)
        {
            _userServices = userServices;
        }
    }



    public partial class HomeController : AdminBaseController
    {
        #region index

        public IActionResult Index()
        {
            return View();
        }

        #endregion


        #region UpdateAvatar


        [HttpPost]
        public async Task<IActionResult> UpdateAvatar(IFormFile postedAvatar)
        {

            var result = await _userServices.UpdateAvatar(postedAvatar, User.GetUserId());

            switch (result)
            {
                case Infa.Domain.ViewModels.Admin.AvatarResult.success:
                    TempData[SuccessMessage] = "اواتار شما با موفقیت تغییر یافت";
                    return Redirect("/Admin");

                case Infa.Domain.ViewModels.Admin.AvatarResult.failure:
                    TempData[ErrorMessage] = "مشکلی پیش امد";
                    TempData[InfoMessage] = " لطفا دوباره تلاش کنید";
                    return Redirect("/Admin");

                case Infa.Domain.ViewModels.Admin.AvatarResult.notFound:
                    TempData[ErrorMessage] = "مشکلی پیش امد";
                    TempData[InfoMessage] = " لطفا دوباره تلاش کنید";
                    return Redirect("/LogOut");

                case Infa.Domain.ViewModels.Admin.AvatarResult.notImage:
                    TempData[ErrorMessage] = "فرمت فایل صحیح نمی باشد";
                    return Redirect("/Admin");

                default:
                    break;
            }

            return Redirect("/Admin");
        }


        #endregion
    }
}
