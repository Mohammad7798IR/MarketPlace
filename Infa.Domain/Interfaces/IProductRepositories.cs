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

        Task<List<Category>> GetAllCategoryByParentId(string parentId);
    }


    public partial interface IProductRepositories
    {
        void UpdateProduct(Product Product);
        Task AddProduct(Product Product);
        Task SaveChanges();
    }
}
