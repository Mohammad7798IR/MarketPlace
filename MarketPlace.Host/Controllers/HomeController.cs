using GoogleReCaptcha.V3.Interface;
using Infa.Application.Extentions;
using Infa.Application.Interfaces;
using Infa.Domain.Interfaces;
using Infa.Domain.Models.Identity;
using Infa.Domain.Models.Site;
using Infa.Domain.ViewModels.Contact;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace MarketPlace.Host.Controllers
{
    public partial class HomeController : BaseController
    {
        private readonly ICaptchaValidator _captchaValidator;
        private readonly IContactServices _contactServices;
        private readonly ISiteServices _siteServices;
        public HomeController
            (
            ICaptchaValidator captchaValidator,
            IContactServices contactServices,
            ISiteServices siteServices)
        {
            _captchaValidator = captchaValidator;
            _contactServices = contactServices;
            _siteServices = siteServices;
        }
    }
    public partial class HomeController
    {
        public async Task<IActionResult> Index()
        {
            ViewBag.banners = await _siteServices.GetAllSitesBannerByPositions(new List<SiteBannerPosition>
            { SiteBannerPosition.Home_1, SiteBannerPosition.Home_2, SiteBannerPosition.Home_3 });

            return View();
        }

        #region ContactUs


        [Route("ContactUs")]
        public IActionResult ContactUs()
        {
            return View();
        }

        [Route("ContactUs")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ContactUs(ContactUsVM contactUsVM)
        {
            if (await _captchaValidator.IsCaptchaPassedAsync(contactUsVM.Token))
            {
                if (ModelState.IsValid)
                {
      
                    await _contactServices.CreateContactUs(contactUsVM , HttpContext.GetUserIp(),User.GetUserId());
                    TempData[SuccessMessage] = "اطلاعات شما با موفقیت ثبت شد";
                    return Redirect("/");

                }
            }

            TempData[ErrorMessage] = "اعتبار سنجی انجام نشد";
            return View(contactUsVM);
        }


        #endregion

        #region AboutUs

        [Route("AboutUs")]
        [HttpGet]
        public async Task<IActionResult> AboutUs()
        {
            var result = await _siteServices.GetAppSetting();
            return View(result);
        }

        #endregion
    }
}
