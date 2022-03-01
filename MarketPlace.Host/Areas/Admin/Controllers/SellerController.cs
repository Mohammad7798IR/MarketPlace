using Infa.Application.Extentions;
using Infa.Application.Interfaces;
using Infa.Domain.ViewModels.Common;
using Infa.Domain.ViewModels.Store;
using MarketPlace.Web.Areas.Admin.Controllers;
using MarketPlace.Web.Http;
using Microsoft.AspNetCore.Mvc;

namespace MarketPlace.Host.Areas.Admin.Controllers
{
    public partial class SellerController
    {
        private readonly ISellerServices _sellerServices;


        public SellerController
            (
            ISellerServices sellerServices
            )
        {
            _sellerServices = sellerServices;
        }
    }

    public partial class SellerController : AdminBaseController
    {
        [HttpGet]
        [Route("SellerRequests")]
        public async Task<IActionResult> SellerRequests(SellerFilterVM sellerFilterVM)
        {
            var result = await _sellerServices.FilterSellerForAdmin(sellerFilterVM);
            return View(result);
        }




        public async Task<IActionResult> AcceptSellerRequest(string sellerId)
        {
            var result = await _sellerServices.AcceptSellerRequest(sellerId);

            if (result)
            {
                return RedirectToAction("SellerRequests");
            }

            return RedirectToAction("SellerRequests");
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RejectSellerRequest(RejectItemVM rejectItemVM)
        {
            var result = await _sellerServices.RejectSellerRequest(rejectItemVM);

            if (result)
            {
                TempData[SuccessMessage] = "با موفقیت رد شد";
                return RedirectToAction("SellerRequests");
            }

            TempData[ErrorMessage] = "مشکلی پیش امد لطفا دوباره تلاش کنید";
            return RedirectToAction("SellerRequests");
        }

    }
}
