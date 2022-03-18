using Infa.Data.Context;
using Infa.Domain.Interfaces;
using Infa.Domain.Models.SellersProduct;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infa.Data.Repositories
{
    public partial class ProductRepositories : IProductRepositories
    {
        private readonly MarketPlaceDBContext _context;

        public ProductRepositories(MarketPlaceDBContext context)
        {
            _context = context;
        }

      
    }

    public partial class ProductRepositories
    {
        public async Task<List<Category>> GetAllCategories()
        {
            return await _context.Category.Where(a=>a.IsActive && !a.IsDeleted).ToListAsync();
        }

        public async Task<List<Product>> GetAllProducts(string sellerId)
        {
            return await _context.Product.AsQueryable()
                .Where(x => x.IsDeleted == false && x.SellerId == sellerId).ToListAsync();
        }

        public async Task<List<Category>> GetAllCategoryByParentId(string parentId)
        {
            return await _context.Category.AsQueryable()
                 .Include(p => p.Parent).Where(a => a.IsActive && !a.IsDeleted && a.ParentId == parentId).ToListAsync();
        }
    }

    public partial class ProductRepositories
    {
        public async Task SaveChanges()
        {
            await _context.SaveChangesAsync();
        }

        public void UpdateProduct(Product Product)
        {
            _context.Product.Update(Product);
        }

        public async Task AddProduct(Product Product)
        {
            await _context.Product.AddAsync(Product);
        }

        public async Task AddProductColors(List<ProductColor> productColors)
        {
            foreach (var item in productColors)
            {
                await _context.ProductColor.AddAsync(item);
            }
        }

        public async Task AddProductCategory(List<ProductCategory> productCategories)
        {

            foreach (var item in productCategories)
            {
                await _context.ProductCategory.AddAsync(item);
            }
        }
    }
}
