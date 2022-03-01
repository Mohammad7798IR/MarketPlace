using Infa.Application.Extentions;
using Infa.Application.Interfaces;
using Infa.Domain.ViewModels.Products;
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
        [HttpGet("products")]
        public async Task<IActionResult> Index(FilterProductsVM productsVM)
        {
            productsVM.SellerId = await _sellerServices.GetLastActiveSellerId(User.GetUserId());
            productsVM.FilterProductState = FilterProductState.All;
            productsVM = await _productServices.filterProducts(productsVM);
            return View(productsVM);
        }
    }
}
