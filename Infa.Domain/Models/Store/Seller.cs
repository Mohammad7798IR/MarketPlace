using Infa.Domain.Models.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infa.Domain.Models.SellersProduct;

namespace Infa.Domain.Models.Store
{
    public partial class Seller : BaseEntity
    {
        //Properties

        [Required]
        public string StoreName { get; set; }

        public string Avatar { get; set; }

        [Required]
        [MaxLength(55)]
        public string PhoneNumber { get; set; }

        [MaxLength(55)]
        public string MobileNumber { get; set; }

        [Required]
        public string Address { get; set; }


        [MaxLength(500)]
        public string Description { get; set; }

        [Required]
        public StoreAcceptanceState StoreAcceptanceState { get; set; }

        public string StoreAcceptanceStateDescription { get; set; }

    }

    public partial class Seller
    {
        //Relations

        public string UserId { get; set; }

        public ApplicationUser user { get; set; }

        public ICollection<Product> Products { get; set; }


    }

    public enum StoreAcceptanceState
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
}
