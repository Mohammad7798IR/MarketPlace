using Infa.Domain.Models.Contacts;
using Infa.Domain.ViewModels.Contact;
using Infa.Domain.ViewModels.Store;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infa.Application.Interfaces
{
    public interface ISellerServices
    {
        Task<RequestSellerResult> AddRequestSellerPanel(RequestSellerVM sellerVM , string userId);

        Task<EditRequestSellerVM> GetInfoToEditRequestSellerPanel(string sellerId, string userId);

        Task<EditRequestSellerResult> EditRequestSellerPanel(EditRequestSellerVM sellerVM , string userId);

        Task<SellerFilterVM> FliterSeller(SellerFilterVM sellerVM, string userId);

    }
}
