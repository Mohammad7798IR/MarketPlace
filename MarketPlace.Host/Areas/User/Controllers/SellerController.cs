using GoogleReCaptcha.V3.Interface;
using Infa.Application.Extentions;
using Infa.Application.Interfaces;
using Infa.Domain.ViewModels.Store;
using Microsoft.AspNetCore.Mvc;

namespace MarketPlace.Host.Areas.User.Controllers
{
    public partial class SellerController : BaseController
    {

        private readonly IUserServices _userServices;
        private readonly ICaptchaValidator _captchaValidator;
        private readonly ISellerServices _sellerServices;

        public SellerController
            (
            IUserServices userServices,
            ICaptchaValidator captchaValidator,
            ISellerServices sellerServices
            )
        {
            _userServices = userServices;
            _captchaValidator = captchaValidator;
            _sellerServices = sellerServices;
        }
    }
    public partial class SellerController
    {


        [HttpGet]
        [Route("SellerRequestPanelList")]
        public async Task<IActionResult> Index(SellerFilterVM filterVM)
        {
            filterVM.OrderBy = OrderBy.Order_DEC;
            var seller = await _sellerServices.FliterSeller(filterVM, User.GetUserId());
            return View(seller);
        }


        #region RequestSellerPanel


        [HttpGet]
        [Route("RequestSellerPanel")]
        public IActionResult RequestSellerPanel()
        {
            return View();
        }


        [HttpPost]
        [Route("RequestSellerPanel")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RequestSellerPanel(RequestSellerVM sellerVM)
        {

            if (ModelState.IsValid)
            {
                if ( await _captchaValidator.IsCaptchaPassedAsync(sellerVM.Token))
                {
                    var result = await _sellerServices.AddRequestSellerPanel(sellerVM, User.GetUserId());
                    switch (result)
                    {
                        case RequestSellerResult.success:
                            TempData[SuccessMessage] = "درخواست شما با موفقیت ثبت شد";
                            TempData[InfoMessage] = "پاسخ شما به زودی ارسال خواهد شد";
                            return Redirect("Index");

                        case RequestSellerResult.NotFound:
                            TempData[ErrorMessage] = "مشکلی پیش امد لطفا دوباره تلاش کنید";
                            return View(sellerVM);

                        case RequestSellerResult.HasUndergoingRequest:
                            TempData[ErrorMessage] = "درخواست قیلی شما در حال پیگیری است";
                            TempData[InfoMessage] = "در حال حاضر نمی توانید درحواست جدید ثبت کنید";
                            return View(sellerVM);

                        case RequestSellerResult.NotImage:
                            TempData[ErrorMessage] = "فرمت فایل ارسالی صحیح نمی باشد";
                            return View(sellerVM);
                        default:
                           return View(sellerVM);
                    }
                   
                }
                TempData[ErrorMessage] = "اعتبار سنجی انجام نشد";
            }
            return View();
        }

        #endregion


        #region EditRequestSellerPanel


        [HttpGet]
        [Route("EditRequestSellerPanel/{sellerId}")]
        public async Task<IActionResult> EditRequestSellerPanel(string sellerId)
        {

            var info = await _sellerServices.GetInfoToEditRequestSellerPanel(sellerId,User.GetUserId());
            if (info == null) return NotFound();
            return View(info);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("EditRequestSellerPanel/{selllerId}")]
        public async Task<IActionResult> EditRequestSellerPanel(EditRequestSellerVM sellerVM)
        {

            if (ModelState.IsValid)
            {
                var info = await _sellerServices.EditRequestSellerPanel(sellerVM, User.GetUserId());
                switch (info)
                {
                    case EditRequestSellerResult.Success:
                        TempData[SuccessMessage] = "اطلاعات مورد نظر با موفقیت ویرایش شد";
                        TempData[InfoMessage] = "فرآیند تایید اطلاعات از سر گرفته شد";
                        return RedirectToAction("Index");
                    case EditRequestSellerResult.NotFound:
                        TempData[ErrorMessage] = "اطلاعات مورد نظر یافت نشد";
                        return View();
                    case EditRequestSellerResult.CantEditNow:
                        TempData[ErrorMessage] = "اطلاعات در حال حاضر قابل تغییر نیست";
                        TempData[InfoMessage] = "اطلاعات شما ابتدا باید بررسی شود";
                        return View();
                    default:
                        break;
                }
            }
         
            
            return View(sellerVM);
        }

        #endregion
    }
}
