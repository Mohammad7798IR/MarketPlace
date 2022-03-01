using Infa.Application.Extentions;
using Infa.Application.Interfaces;
using Infa.Domain.ViewModels.Products;
using MarketPlace.Host.Areas.User.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace MarketPlace.Host.Areas.Seller.Controllers
{
    public partial class HomeController : SellerBaseController
    {
   
    }
    public partial class HomeController : SellerBaseController
    {

        public async Task<IActionResult> Index(FilterProductsVM productsVM)
        {
            return View();
        }
    }
}
