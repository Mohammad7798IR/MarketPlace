using Infa.Application.Interfaces;
using Infa.Application.Utils;
using Infa.Domain.Interfaces;
using Infa.Domain.Models.SellersProduct;
using Infa.Domain.Models.Store;
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
        public async Task<FilterProductsVM> filterProducts(FilterProductsVM filterProductsVM)
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

        public async Task<List<Category>> GetAllCategoriesByParentId(string? parentId)
        {
            if (parentId == null)
            {
                return await _productRepositories.GetAllCategoryByParentId(parentId);
            }


            return await _productRepositories.GetAllCategoryByParentId(parentId);

        }

        public async Task<List<Category>> GetAllCategories()
        {
            return await _productRepositories.GetAllCategories();
        }

        public async Task<CreateProductResult> CreateProduct(CreateProductVM productVM, IFormFile postedFile, string sellerId)
        {
            postedFile.IsImage();
            string imageName = Guid.NewGuid().ToString() + Path.GetExtension(postedFile.FileName);
            postedFile.AddImageToServer(imageName, FilePath.ProductThumbnailImageServer, 100, 100, FilePath.ProductThumbnailImage);


            var product = new Product()
            {
                Title = productVM.Title,
                Price = productVM.Price,
                ShortDescription = productVM.ShortDescription,
                Description = productVM.Description,
                IsActive = productVM.IsActive,
                SellerId = sellerId,
                ImageName = imageName,

            };

            await _productRepositories.AddProduct(product);
            await _productRepositories.SaveChanges();


            return CreateProductResult.Success;
        }
    }
}
