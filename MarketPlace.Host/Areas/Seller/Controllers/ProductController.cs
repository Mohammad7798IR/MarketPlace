using Infa.Application.Extentions;
using Infa.Application.Interfaces;
using Infa.Application.Utils;
using Infa.Domain.ViewModels.Products;
using MarketPlace.Application.Extensions;
using MarketPlace.Application.Utils;
using MarketPlace.Host.Areas.User.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace MarketPlace.Host.Areas.Seller.Controllers
{
    public partial class ProductController : SellerBaseController
    {
        private readonly ISellerServices _sellerServices;
        private readonly IProductServices _productServices;
        public ProductController(ISellerServices sellerServices, IProductServices productServices)
        {
            _sellerServices = sellerServices;
            _productServices = productServices;
        }
    }
    public partial class ProductController : SellerBaseController
    {
        [HttpGet("ProductsList")]
        public async Task<IActionResult> Index(FilterProductsVM productsVM)
        {
            productsVM.SellerId = await _sellerServices.GetLastActiveSellerId(User.GetUserId());
            productsVM.FilterProductState = FilterProductState.All;
            productsVM = await _productServices.filterProducts(productsVM);
            return View(productsVM);
        }

        [HttpGet("CreateProduct")]
        public async Task<IActionResult> CreateProduct()
        {
            ViewBag.Categories = await _productServices.GetAllCategoriesByParentId(null);
            return View();
        }

        [HttpPost("CreateProduct")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateProduct(CreateProductVM productVM, IFormFile postedFile)
        {
            if (ModelState.IsValid)
            {
                var result = await _productServices.CreateProduct(productVM, postedFile,User.GetUserId());

            }
            ViewBag.Categories = await _productServices.GetAllCategoriesByParentId(null);
            return View();
        }
    }
}
