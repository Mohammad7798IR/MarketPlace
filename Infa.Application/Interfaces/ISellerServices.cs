using Infa.Domain.Models.Contacts;
using Infa.Domain.Models.Store;
using Infa.Domain.ViewModels.Common;
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

        Task<SellerFilterVM> FilterSellerForAdmin(SellerFilterVM sellerFilterVM);

        Task<string> GetLastActiveSellerId(string sellerId);

        Task<bool> AcceptSellerRequest(string sellerId);

        Task<bool> RejectSellerRequest(RejectItemVM rejectItemVM);
    }
}
