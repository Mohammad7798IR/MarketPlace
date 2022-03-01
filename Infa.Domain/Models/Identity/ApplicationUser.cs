using Infa.Domain.Models.Contacts;
using Infa.Domain.Models.Store;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infa.Domain.Models.Identity
{
    //Properties
    public partial class ApplicationUser : IdentityUser<string>
    {
        [Display(Name ="تاریخ عضویت")]
        [Required]
        public string? CreatedAt { get; set; }


        [Display(Name = "تاریخ تغییر")]
        public string? UpdatedAt { get; set; }


        [Display(Name = "توضیحات")]
        public string? Description { get; set; }


        [Display(Name = "جنسیت")]
        public Gender Gender { get; set; }


        [Required]
        [Display(Name = "نام")]
        public string FirstName { get; set; }


        [Required]
        [Display(Name = "نام خانوادگی")]
        public string LastName { get; set; }


        [Required]
        [Display(Name = "کد فعال سازی")]
        public string ActiveCode { get; set; }

        [Display(Name = "تصویر آواتار")]
        [MaxLength(200, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد")]
        public string Avatar { get; set; }
    }

    //Relations
    public partial class ApplicationUser
    {
        public ICollection<ApplicationUserRole>? UserRoles { get; set; } = new HashSet<ApplicationUserRole>();

        public ICollection<ContactUs>? ContactUses { get; set; } = new HashSet<ContactUs>();

        public ICollection<Ticket> Tickets { get; set; } = new HashSet<Ticket>();

        public ICollection<TicketMessage> ticketMessages { get; set; } = new HashSet<TicketMessage>();

        public ICollection<Seller> Sellers { get; set; } = new HashSet<Seller>();
    }

    public enum Gender
    {
        [Display(Name ="نامشخص")]
        unknown,
        [Display(Name ="اقا")]
        male,
        [Display(Name = "خانوم")]
        female,
    }
}
