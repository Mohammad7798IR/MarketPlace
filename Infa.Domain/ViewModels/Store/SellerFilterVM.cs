using Infa.Domain.Models.Store;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infa.Domain.ViewModels.Store
{
    public partial class SellerFilterVM 
    {
        public string StoreName { get; set; }

        public StoreAcceptanceState StoreAcceptanceState { get; set; }

        public string PhoneNumber { get; set; }

        public string MobileNumber { get; set; }

        public string Address { get; set; }

        public string Code { get; set; }


        public string UserId { get; set; }

    }

    public partial class SellerFilterVM
    {
        //Filters

        public OrderBy OrderBy { get; set; }

    }

    public partial class SellerFilterVM
    {
        //Relations
        public List<Seller> sellers { get; set; }
    }

    public enum RequesPanelState
    {
        [Display(Name = "همه")]
        All,
        [Display(Name = "در حال بررسی")]
        UnderProgress,
        [Display(Name = "تایید شده")]
        Accepted,
        [Display(Name = "رد شده")]
        Rejected
    }

    public enum OrderBy
    {
        Order_DEC,
        Order_ACE
    }


}
