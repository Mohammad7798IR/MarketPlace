using Infa.Domain.Models.Contacts;
using Infa.Domain.ViewModels.Contact;
using Infa.Domain.ViewModels.Store;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infa.Application.Interfaces
{
    public interface IContactServices
    {
        Task CreateContactUs(ContactUsVM ContactUsVM, string UserIp, string? UserId);

        Task<AddTicketResult> AddTicket(AddUserTicketVM ticketVM, string userId);

        Task<List<Ticket>> FilterTickets(FilterTicketVM filterTicketVM, string userId);

        Task<TicketDetailsVM> GetTicketDetail(string ticketId, string userId);
    }
}
