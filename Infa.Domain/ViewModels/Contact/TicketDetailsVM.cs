using Infa.Domain.Models.Contacts;
using Infa.Domain.Models.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infa.Domain.ViewModels.Contact
{
    public class TicketDetailsVM
    {
        public Ticket Ticket { get; set; }

        public ApplicationUser User { get; set; }

        public List<TicketMessage> TicketMessage { get; set; }
    }
}
