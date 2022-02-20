using Infa.Domain.Models.Identity;
using Infa.Domain.Models.Store;
using Infa.Domain.ViewModels.Store;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infa.Domain.Interfaces
{
    public interface ISellerRepositories
    {

        Task<bool> HasUnderProgressRequest(string sellerId);

        Task<ApplicationUser> GetUserAndSeller(string userId);

        Task<List<ApplicationUser>> GetListOfUserAndSeller(string userId);

        Task<Seller> GetSellerById(string sellerId);

        Task<List<Seller>> GetAllSellers();

        Task SaveChanges();

        Task AddSeller(Seller seller);

        void UpdateSeller(Seller seller);

    }
}
