using Infa.Application.Interfaces;
using Infa.Domain.ViewModels.Store;
using MarketPlace.Web.Areas.Admin.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace MarketPlace.Host.Areas.Admin.Controllers
{
    public partial class SellerController
    {
        private readonly ISellerServices _sellerServices;

        public SellerController(ISellerServices sellerServices)
        {
            _sellerServices = sellerServices;
        }
    }

    public partial class SellerController : AdminBaseController
    {
        [HttpGet]
        [Route("SellerRequests")]
        public async Task<IActionResult> SellerRequests(SellerFilterVM sellerFilterVM )
        {
            var result = await _sellerServices.FilterSellerForAdmin(sellerFilterVM);
            return View(result);
        }
    }
}
