using GoogleReCaptcha.V3.Interface;
using Infa.Application.Extentions;
using Infa.Application.Interfaces;
using Infa.Domain.ViewModels.Contact;
using Infa.Domain.ViewModels.Userpanel;
using Microsoft.AspNetCore.Mvc;

namespace MarketPlace.Host.Areas.User.Controllers
{

    public partial class HomeController : BaseController
    {
        private readonly IUserServices _userServices;
        private readonly ICaptchaValidator _captchaValidator;
        private readonly ISellerServices _contactServices;

        public HomeController
            (
            IUserServices userServices,
            ICaptchaValidator captchaValidator,
            ISellerServices contactServices
            )
        {
            _userServices = userServices;
            _captchaValidator = captchaValidator;
            _contactServices = contactServices;
        }
    }


    public partial class HomeController
    {
        public IActionResult Index()
        {
            return View();
        }


        #region UserDetails

        [Route("UserDetails")]
        [HttpGet]
        public async Task<ActionResult> UserDetails()
        {
            var result = await _userServices.GetUserDetails(User.GetUserId());
            if (result == null)
            {
                return NotFound();
            }
            return View(result);
        }


        [Route("UserDetails")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> UserDetails(UserDetailsVM userDetailsVM, IFormFile avatar)
        {
            if (await _captchaValidator.IsCaptchaPassedAsync(userDetailsVM.Token))
            {
                ModelState.Remove("Email");
                ModelState.Remove("ImageName");
                if (ModelState.IsValid)
                {
                    var result = await _userServices.UpdateUserDetails(userDetailsVM, User.GetUserId(), avatar);
                    switch (result)
                    {
                        case UserDetailsResult.Success:
                            TempData[SuccessMessage] = "اطلاعات شما با موفقیت ثیت شد";
                            TempData[InfoMessage] = "لطفا دوباره وارد شوید";
                            return Redirect("/Logout");


                        case UserDetailsResult.NotImage:
                            TempData[ErrorMessage] = "فرمت فایل انتخابی صحیح نیست";
                            return View(userDetailsVM);

                        case UserDetailsResult.NotFound:
                            TempData[ErrorMessage] = "مشکلی پیش امد لطفا دوباره تلاش کنید";
                            break;
                        default:
                            break;
                    }
                }
                TempData[ErrorMessage] = "اعتبار سنجی انجام نشد";
            }
            return View();
        }

        #endregion


  
    }
}
