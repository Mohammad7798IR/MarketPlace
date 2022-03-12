using Infa.Domain.Models.SellersProduct;
using Infa.Domain.Models.Store;
using Infa.Domain.ViewModels.Products;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infa.Application.Interfaces
{
    public interface IProductServices
    {
        Task<FilterProductsVM> filterProducts(FilterProductsVM filterProductsVM);

        Task<List<Category>> GetAllCategoriesByParentId(string parentId);

        Task<List<Category>> GetAllCategories();

        Task<CreateProductResult> CreateProduct(CreateProductVM productVM, IFormFile postedFile, string sellerId);
    }
}
