using Infa.Domain.Models.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Infa.Domain.Models.Contacts
{
    //Properties
    public partial class TicketMessage : BaseEntity
    {

        public string TicketId { get; set; }

        public string SenderId { get; set; }

        [Display(Name = "متن پیام")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string Text { get; set; }
    }




    //Relations
    public partial class TicketMessage
    {
        public Ticket Ticket { get; set; }

        public ApplicationUser Sender { get; set; }
    }
}
