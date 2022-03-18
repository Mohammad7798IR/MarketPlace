using Infa.Application.Extentions;
using Infa.Application.Interfaces;
using Infa.Application.Utils;
using Infa.Domain.ViewModels.Products;
using MarketPlace.Application.Extensions;
using MarketPlace.Application.Utils;
using MarketPlace.Host.Areas.User.Controllers;
using MarketPlace.Web.Http;
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
                var seller = await _sellerServices.GetLastActiveSellerId(User.GetUserId());
                var result = await _productServices.CreateProduct(productVM, postedFile,seller);
                switch (result)
                {
                    case CreateProductResult.Success:
                        TempData[SuccessMessage] = $"محصول مورد نظر با عنوان {productVM.Title} با موفقیت ثبت شد";
                        return RedirectToAction("Index");

                    case CreateProductResult.HasNoImage:
                        TempData[WarningMessage] = "لطفا تصویر محصول را وارد نمایید";
                        break;

                    case CreateProductResult.Fail:
                        TempData[ErrorMessage] = "عملیات ثبت محصول با خطا مواجه شد";
                        break;
                    default:
                        break;
                }
            }
            ViewBag.Categories = await _productServices.GetAllCategoriesByParentId(null);
            return View();
        }


        [HttpGet("product-categories/{parentId}")]
        public async Task<IActionResult> GetProductCategoriesByParent(string parentId)
        {
            var categories = await _productServices.GetAllCategoriesByParentId(parentId);

            return JsonResponseStatus.SendStatus(JsonResponseStatusType.Success, "اطلاعات دسته بندی ها", categories);
        }
    }
}
