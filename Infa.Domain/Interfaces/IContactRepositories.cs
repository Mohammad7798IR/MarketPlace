using Infa.Domain.Models.Contacts;
using Infa.Domain.ViewModels.Contact;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infa.Domain.Interfaces
{
    public partial interface IContactRepositories
    {
        Task AddContactUs(ContactUs contactUsVM);

        Task AddTicket(Ticket ticketVM);

        Task AddTicketMessage(TicketMessage messageVM);

        Task<List<Ticket>> GetAllTickets(string userId);

        Task<Ticket> GetTicketByTicketId(string ticketId, string userId);
    }


    public partial interface IContactRepositories
    {
        void UpdateContactUs(ContactUs contactUs);

        Task SaveChanges();
    }
}
