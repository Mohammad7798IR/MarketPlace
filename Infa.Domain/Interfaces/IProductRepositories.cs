using Infa.Domain.Models.Contacts;
using Infa.Domain.Models.SellersProduct;
using Infa.Domain.ViewModels.Contact;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infa.Domain.Interfaces
{
    public partial interface IProductRepositories
    {
        Task<List<Product>> GetAllProducts(string sellerId);

        Task<List<Category>> GetAllCategories();

        Task<Product> GetProductById(string id);

        Task<List<Category>> GetAllCategoryByParentId(string parentId);

        Task<List<Product>> GetAllProductsWithoutSellerId();

        Task<List<ProductCategory>> GetProductCategoryByProductId(string productId);

        Task<List<ProductColor>> GetProductColorsByProductId(string productId);
    }


    public partial interface IProductRepositories
    {
        void UpdateProduct(Product Product);



        Task AddProduct(Product Product);

        Task AddProductColors(List<ProductColor> productColors);

        Task AddProductCategory(List<ProductCategory> productCategories);

        Task SaveChanges();
    }
}
