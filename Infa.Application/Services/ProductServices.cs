using Common.ClassHelpers;
using Infa.Application.Interfaces;
using Infa.Application.Utils;
using Infa.Domain.Interfaces;
using Infa.Domain.Models.SellersProduct;
using Infa.Domain.Models.Store;
using Infa.Domain.ViewModels.Common;
using Infa.Domain.ViewModels.Products;
using MarketPlace.Application.Extensions;
using MarketPlace.Application.Utils;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infa.Application.Services
{
    public partial class ProductServices : IProductServices
    {
        private readonly IProductRepositories _productRepositories;
        private readonly ISellerRepositories _sellerRepositories;

        public ProductServices(IProductRepositories productRepositories, ISellerRepositories sellerRepositories)
        {
            _productRepositories = productRepositories;
            _sellerRepositories = sellerRepositories;
        }

      
    }

    public partial class ProductServices
    {
        public async Task<FilterProductsVM> FilterProducts(FilterProductsVM filterProductsVM)
        {

            if (filterProductsVM.SellerId != null)
            {
                var query = await _productRepositories.GetAllProducts(filterProductsVM.SellerId);

                switch (filterProductsVM.FilterProductState)
                {
                    case FilterProductState.All:
                        break;
                    case FilterProductState.UnderProgress:
                        query = query.Where(x => x.ProductAcceptanceState == ProductAcceptanceState.UnderProgress).ToList();
                        break;
                    case FilterProductState.Accepted:
                        query = query.Where(x => x.ProductAcceptanceState == ProductAcceptanceState.Accepted).ToList();
                        break;
                    case FilterProductState.Rejected:
                        query = query.Where(x => x.ProductAcceptanceState == ProductAcceptanceState.Rejected).ToList();
                        break;
                    case FilterProductState.Active:
                        query = query.Where(x => x.ProductAcceptanceState == ProductAcceptanceState.Active && x.IsActive).ToList();
                        break;
                    case FilterProductState.NotActive:
                        query = query.Where(x => x.ProductAcceptanceState == ProductAcceptanceState.NotActive && !x.IsActive).ToList();
                        break;
                    default:
                        break;
                }

                if (!string.IsNullOrEmpty(filterProductsVM.ProductTitle))
                {
                    query = query.Where(s => s.Title.EndsWith(filterProductsVM.ProductTitle) || s.Title.Contains(filterProductsVM.ProductTitle) || s.Title.StartsWith(filterProductsVM.ProductTitle)).ToList();
                }

                filterProductsVM.Products = query;

                return filterProductsVM;
            }


            var products = await _productRepositories.GetAllProductsWithoutSellerId();

            switch (filterProductsVM.FilterProductState)
            {
                case FilterProductState.All:
                    break;
                case FilterProductState.UnderProgress:
                    products = products.Where(x => x.ProductAcceptanceState == ProductAcceptanceState.UnderProgress).ToList();
                    break;
                case FilterProductState.Accepted:
                    products = products.Where(x => x.ProductAcceptanceState == ProductAcceptanceState.Accepted).ToList();
                    break;
                case FilterProductState.Rejected:
                    products = products.Where(x => x.ProductAcceptanceState == ProductAcceptanceState.Rejected).ToList();
                    break;
                case FilterProductState.Active:
                    products = products.Where(x => x.ProductAcceptanceState == ProductAcceptanceState.Active && x.IsActive).ToList();
                    break;
                case FilterProductState.NotActive:
                    products = products.Where(x => x.ProductAcceptanceState == ProductAcceptanceState.NotActive && !x.IsActive).ToList();
                    break;
                default:
                    break;
            }

            if (!string.IsNullOrEmpty(filterProductsVM.ProductTitle))
            {
                products = products.Where(s => s.Title.EndsWith(filterProductsVM.ProductTitle) || s.Title.Contains(filterProductsVM.ProductTitle) || s.Title.StartsWith(filterProductsVM.ProductTitle)).ToList();
            }

            filterProductsVM.Products = products;

            return filterProductsVM;


        }

        public async Task<List<Category>> GetAllCategoriesByParentId(string? parentId)
        {
            return await _productRepositories.GetAllCategories();
        }

        public async Task<List<Category>> GetAllCategories()
        {
            return await _productRepositories.GetAllCategories();
        }

        public async Task<CreateProductResult> CreateProduct(CreateProductVM productVM, IFormFile postedFile, string sellerId)
        {
            if (postedFile == null) return CreateProductResult.HasNoImage;

            if (!postedFile.IsImage()) return CreateProductResult.Fail;


            string imageName = Guid.NewGuid().ToString() + Path.GetExtension(postedFile.FileName);
            var res = postedFile.AddImageToServer(imageName, FilePath.ProductThumbnailImageServer, 100, 100, FilePath.ProductThumbnailImage);


            if (res)
            {
                var product = new Product()
                {
                    Id = Guid.NewGuid().ToString(),
                    Title = productVM.Title,
                    Price = productVM.Price,
                    ShortDescription = productVM.ShortDescription,
                    Description = productVM.Description,
                    IsActive = productVM.IsActive,
                    SellerId = sellerId,
                    ImageName = imageName,
                    CreateAt = PersianDateTime.Now(),
                    ProductAcceptOrRejectDescription = "accepted",

                };

                await _productRepositories.AddProduct(product);
                await _productRepositories.SaveChanges();

                var lstCat = new List<ProductCategory>();

                foreach (var item in productVM.SelectedCategories)
                {
                    var selectedCat = new ProductCategory()
                    {
                        Id = Guid.NewGuid().ToString(),
                        ProductId = product.Id,
                        CreateAt = PersianDateTime.Now(),
                        IsDeleted = false,
                        CategoryId = item
                    };
                    lstCat.Add(selectedCat);
                }

                await _productRepositories.AddProductCategory(lstCat);
                await _productRepositories.SaveChanges();


                var lstColor = new List<ProductColor>();

                foreach (var item in productVM.ProductColors)
                {
                    var selectedCat = new ProductColor()
                    {
                        Id = Guid.NewGuid().ToString(),
                        ProductId = product.Id,
                        CreateAt = PersianDateTime.Now(),
                        ColorName = item.ColorName,
                        IsDeleted = false,
                        Price = item.Price,
                    };
                    lstColor.Add(selectedCat);
                }
                await _productRepositories.AddProductColors(lstColor);
                await _productRepositories.SaveChanges();

                return CreateProductResult.Success;
            }

            return CreateProductResult.Fail;
        }

        public async Task<bool> AcceptProduct(string productId)
        {
            var product = await _productRepositories.GetProductById(productId);

            if (product == null) return false;


            product.ProductAcceptanceState           = ProductAcceptanceState.Accepted;
            product.ProductAcceptOrRejectDescription = "Accepted";
            _productRepositories.UpdateProduct(product);
            await _productRepositories.SaveChanges();

            return true;
           
        }

        public async Task<bool> RejectProduct(string productId, RejectItemVM rejectItemVM)
        {
            var product = await _productRepositories.GetProductById(productId);
            if (product == null) return false;

            product.ProductAcceptanceState           = ProductAcceptanceState.Rejected;
            product.ProductAcceptOrRejectDescription = rejectItemVM.RejectMessage;
            _productRepositories.UpdateProduct(product);
            await _productRepositories.SaveChanges();

            return true;
        }
    }
}
