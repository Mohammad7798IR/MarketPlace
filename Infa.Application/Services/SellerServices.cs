using Common.ClassHelpers;
using Infa.Application.Interfaces;
using Infa.Application.Utils;
using Infa.Domain.Interfaces;
using Infa.Domain.Models.Contacts;
using Infa.Domain.Models.Identity;
using Infa.Domain.Models.Store;
using Infa.Domain.ViewModels.Common;
using Infa.Domain.ViewModels.Contact;
using Infa.Domain.ViewModels.Store;
using MarketPlace.Application.Extensions;
using MarketPlace.Application.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infa.Application.Services
{
    public partial class SellerServices : ISellerServices
    {
        private readonly ISellerRepositories _sellerRepositories;
        private readonly IUserRepositories _userRepositories;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IConfiguration _configuration;

        public SellerServices(
            ISellerRepositories SellerRepositories,
            UserManager<ApplicationUser> userManager,
            IUserRepositories userRepositories,
            IConfiguration configuration)
        {
            _sellerRepositories = SellerRepositories;
            _userManager = userManager;
            _userRepositories = userRepositories;
            _configuration = configuration;
        }

    }


    public partial class SellerServices
    {

        public async Task<RequestSellerResult> AddRequestSellerPanel(RequestSellerVM sellerVM, string userId)
        {

            var user = await _sellerRepositories.GetUserAndSeller(userId);

            if (user == null)
                return RequestSellerResult.NotFound;


            foreach (var item in user.Sellers)
            {
                if (item.StoreAcceptanceState == StoreAcceptanceState.UnderProgress)
                {
                    return RequestSellerResult.HasUndergoingRequest;
                }
            }


            var code = user.Sellers.Select(c => c.Code).LastOrDefault();



            var newSeller = new Seller()
            {
                Id = Guid.NewGuid().ToString(),
                Code = code + 1,
                UserId = user.Id,
                CreateAt = PersianDateTime.Now(),
                MobileNumber = sellerVM.MobileNumber,
                PhoneNumber = sellerVM.PhoneNumber,
                Description = sellerVM.Description,
                Avatar = "avatar.jpg",
                StoreName = sellerVM.StoreName,
                StoreAcceptanceStateDescription = "در حال بررسی",
                StoreAcceptanceState = StoreAcceptanceState.UnderProgress,
                Address = sellerVM.Address,
            };

            await _sellerRepositories.AddSeller(newSeller);

            await _sellerRepositories.SaveChanges();


            return RequestSellerResult.success;

        }

        public async Task<EditRequestSellerVM> GetInfoToEditRequestSellerPanel(string sellerId, string userId)
        {
            var seller = _sellerRepositories.GetSellerById(sellerId).Result;

            if (seller == null || seller.UserId != userId)
            {
                return null;
            }

            if (seller.StoreAcceptanceState == StoreAcceptanceState.UnderProgress)
            {
                return null;
            }

            EditRequestSellerVM sellerVM = new EditRequestSellerVM()
            {
                StoreName = seller.StoreName,
                Address = seller.Address,
                Description = seller.Description,
                MobileNumber = seller.MobileNumber,
                PhoneNumber = seller.PhoneNumber,
                SellerId = seller.Id
            };

            return sellerVM;
        }

        public async Task<EditRequestSellerResult> EditRequestSellerPanel(EditRequestSellerVM sellerVM, string userId)
        {
            var seller = _sellerRepositories.GetSellerById(sellerVM.SellerId).Result;

            if (seller == null || seller.UserId != userId)
            {
                return EditRequestSellerResult.NotFound;
            }

            if (seller.StoreAcceptanceState == StoreAcceptanceState.UnderProgress)
            {
                return EditRequestSellerResult.CantEditNow;
            }

            seller.PhoneNumber = sellerVM.PhoneNumber;
            seller.Description = sellerVM.Description;
            seller.StoreName = sellerVM.StoreName;
            seller.MobileNumber = sellerVM.MobileNumber;
            seller.Address = sellerVM.Address;
            seller.StoreAcceptanceState = StoreAcceptanceState.UnderProgress;
            seller.EditedAt = PersianDateTime.Now();


            _sellerRepositories.UpdateSeller(seller);
            await _sellerRepositories.SaveChanges();

            return EditRequestSellerResult.Success;
        }

        public async Task<SellerFilterVM> FliterSeller(SellerFilterVM sellerVM, string userId)
        {
            var query = await _sellerRepositories.GetListOfUserAndSeller(userId);

            var sellers = query.Select(x => x.Sellers);


            foreach (var item in sellers)
            {
                switch (sellerVM.StoreAcceptanceState)
                {
                    case StoreAcceptanceState.UnderProgress:
                        sellerVM.sellers = item.Where(s => s.StoreAcceptanceState == StoreAcceptanceState.UnderProgress).ToList();
                        break;
                    case StoreAcceptanceState.Accepted:
                        sellerVM.sellers = item.Where(s => s.StoreAcceptanceState == StoreAcceptanceState.Accepted).ToList();
                        break;
                    case StoreAcceptanceState.Rejected:
                        sellerVM.sellers = item.Where(s => s.StoreAcceptanceState == StoreAcceptanceState.Rejected).ToList();
                        break;
                    default:
                        break;
                }

                switch (sellerVM.OrderBy)
                {
                    case OrderBy.Order_DEC:
                        sellerVM.sellers = item.OrderByDescending(or => or.CreateAt).ToList();
                        break;

                    case OrderBy.Order_ACE:
                        sellerVM.sellers = item.OrderBy(or => or.CreateAt).ToList();
                        break;
                    default:
                        break;
                }
            }

            sellerVM.UserId = userId;
            return sellerVM;
        }

        public async Task<SellerFilterVM> FilterSellerForAdmin(SellerFilterVM sellerFilterVM)
        {
            var query = await _sellerRepositories.GetAllSellers();


            switch (sellerFilterVM.StoreAcceptanceState)
            {
                case StoreAcceptanceState.Accepted:
                    query = query.Where(sas => sas.StoreAcceptanceState == StoreAcceptanceState.Accepted).ToList();
                    break;
                case StoreAcceptanceState.Rejected:
                    query = query.Where(sas => sas.StoreAcceptanceState == StoreAcceptanceState.Rejected).ToList();
                    break;
                case StoreAcceptanceState.UnderProgress:
                    query = query.Where(sas => sas.StoreAcceptanceState == StoreAcceptanceState.UnderProgress).ToList();
                    break;
                case StoreAcceptanceState.All:
                    break;
                default:
                    break;
            }


            switch (sellerFilterVM.OrderBy)
            {
                case OrderBy.Order_DEC:
                    query = query.OrderByDescending(cr => cr.CreateAt).ToList();
                    break;
                case OrderBy.Order_ACE:
                    query = query.OrderBy(cr => cr.CreateAt).ToList();
                    break;
                default:
                    break;
            }

            if (!string.IsNullOrEmpty(sellerFilterVM.StoreName))
            {
                query = query.Where(s => s.StoreName.EndsWith(sellerFilterVM.StoreName) || s.StoreName.Contains(sellerFilterVM.StoreName) || s.StoreName.StartsWith(sellerFilterVM.StoreName)).ToList();
            }
            if (!string.IsNullOrEmpty(sellerFilterVM.PhoneNumber))
            {
                query = query.Where(s => s.StoreName.EndsWith(sellerFilterVM.PhoneNumber) || s.PhoneNumber.Contains(sellerFilterVM.PhoneNumber) || s.StoreName.StartsWith(sellerFilterVM.PhoneNumber)).ToList();
            }
            if (!string.IsNullOrEmpty(sellerFilterVM.MobileNumber))
            {
                query = query.Where(s => s.StoreName.EndsWith(sellerFilterVM.MobileNumber) || s.MobileNumber.Contains(sellerFilterVM.MobileNumber) || s.StoreName.StartsWith(sellerFilterVM.MobileNumber)).ToList();
            }


            sellerFilterVM.sellers = query;

            return sellerFilterVM;
        }

        public async Task<bool> AcceptSellerRequest(string sellerId)
        {
            var seller = _sellerRepositories.GetSellerById(sellerId).Result;

            if (seller == null) return false;



            foreach (var item in seller.user.UserRoles)
            {
                item.RoleId = _configuration.GetSection("Roles")["AplicationSeller"];
                item.UserId = seller.UserId;
                item.Id = Guid.NewGuid().ToString();
                await _userRepositories.AddUserRoles(item);
                await _userRepositories.SaveChanges();
            }

            seller.StoreAcceptanceState = StoreAcceptanceState.Accepted;
            seller.EditedAt = PersianDateTime.Now();


            _sellerRepositories.UpdateSeller(seller);
            await _sellerRepositories.SaveChanges();

            return true;


        }

        public async Task<bool> RejectSellerRequest(RejectItemVM rejectItemVM)
        {
            var seller = await _sellerRepositories.GetSellerByCode(rejectItemVM.Code);


            if (seller == null)
                return false;

            seller.StoreAcceptanceState = StoreAcceptanceState.Rejected;
            seller.EditedAt = PersianDateTime.Now();
            seller.StoreAcceptanceStateDescription = rejectItemVM.RejectMessage;

            _sellerRepositories.UpdateSeller(seller);
            await _sellerRepositories.SaveChanges();

            return true;
        }

        public async Task<string> GetLastActiveSellerId(string userId)
        {
            var seller = await _sellerRepositories.GetLastActiveSeller(userId);
            return seller.Id;
        }

    }
}
