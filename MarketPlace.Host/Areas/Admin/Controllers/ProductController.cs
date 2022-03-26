using Infa.Application.Interfaces;
using Infa.Domain.ViewModels.Common;
using Infa.Domain.ViewModels.Products;
using MarketPlace.Web.Areas.Admin.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace MarketPlace.Host.Areas.Admin.Controllers
{
    public partial class ProductController : AdminBaseController
    {
        private readonly IProductServices _productServices;

        public ProductController(IProductServices productServices)
        {
            _productServices = productServices;
        }
    }
    public partial class ProductController
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [Route("FilterProducts")]
        public async Task<IActionResult> FilterProducts(FilterProductsVM filterProductsVM)
        {
            return View(await _productServices.FilterProducts(filterProductsVM));
        }

        [HttpGet]
        public async Task<IActionResult> AcceptProduct(string Id)
        {
            await _productServices.AcceptProduct(Id);
            return RedirectToAction("FilterProducts");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RejectProduct(string Id, RejectItemVM rejectItemVM)
        {
            if (ModelState.IsValid)
            {
                await _productServices.RejectProduct(Id, rejectItemVM);
                return RedirectToAction("FilterProducts");
            }
            return View();
        }
    }
}
