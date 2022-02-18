using Infa.Domain.Models.Contacts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infa.Domain.ViewModels.Contact
{
    public class FilterTicketVM
    {
        public string Title { get; set; }

        public long? UserId { get; set; }

        public FilterTicketState FilterTicketStates { get; set; }

        public TicketSection? TicketSection { get; set; }

        public TicketPriority? TicketPriority { get; set; }

        public FilterTicketOrder OrderBy { get; set; }

        public List<Ticket> Tickets { get; set; }

        public enum FilterTicketState
        {
            All,
            NotDeleted,
            Deleted
        }

        public enum FilterTicketOrder
        {
            CreateDate_DES,
            CreateDate_ASC,
        }
    }
}
