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
        public async Task<List<Product>> GetAllProducts(string sellerId)
        {
            return await _context.Product.AsQueryable()
                .Where(x => x.IsDeleted == false &&  x.SellerId==sellerId).ToListAsync();
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
    }
}
