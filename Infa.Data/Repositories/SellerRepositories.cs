using Infa.Data.Context;
using Infa.Domain.Interfaces;
using Infa.Domain.Models.Identity;
using Infa.Domain.Models.Store;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infa.Data.Repositories
{
    public partial class SellerRepositories : ISellerRepositories
    {
        private readonly MarketPlaceDBContext _context;

        public SellerRepositories(MarketPlaceDBContext context)
        {
            _context = context;
        }

     
    }
    public partial class SellerRepositories
    {

        public async Task AddSeller(Seller seller)
        {
            await _context.Sellers.AddAsync(seller);
        }

        public async Task<bool> HasUnderProgressRequest(string sellerId)
        {
          return await _context.Sellers.AsQueryable()
                .AnyAsync(hup=>hup.StoreAcceptanceState == StoreAcceptanceState.UnderProgress);
        }

        public async Task<ApplicationUser> GetUserAndSeller(string userId)
        {
           return await _context.Users.Include(s=>s.Sellers)
                .Where(u=>u.Id==userId).AsNoTracking().SingleOrDefaultAsync();
        }

        public async Task<List<ApplicationUser>> GetListOfUserAndSeller(string userId)
        {
            return await _context.Users.AsQueryable()
                  .Include(s => s.Sellers).Where(u => u.Id == userId).ToListAsync();
        }

        public async Task<Seller> GetSellerById(string sellerId)
        {
            return await _context.Sellers.AsQueryable()
                .Where(s=>s.Id==sellerId).SingleOrDefaultAsync();
        }

        public async Task<List<Seller>> GetAllSellers()
        {
            return await _context.Sellers.AsQueryable()
                .Where(x => x.IsDeleted == false).ToListAsync();
        }
    }

    public partial class SellerRepositories
    {
        public void UpdateSeller(Seller seller)
        {
            _context.Sellers.Update(seller);
        }

        public async Task SaveChanges()
        {
            await _context.SaveChangesAsync();
        }
    }
}
