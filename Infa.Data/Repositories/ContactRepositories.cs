using Infa.Data.Context;
using Infa.Domain.Interfaces;
using Infa.Domain.Models.Contacts;
using Infa.Domain.ViewModels.Contact;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infa.Data.Repositories
{
    public partial class ContactRepositories : IContactRepositories
    {
        private readonly MarketPlaceDBContext _context;

        public ContactRepositories(MarketPlaceDBContext context)
        {
            _context = context;
        }


    }
    public partial class ContactRepositories
    {
        public async Task AddContactUs(ContactUs contactUs)
        {
            await _context.ContactUs.AddAsync(contactUs);
        }

        public async Task AddTicket(Ticket ticketVM)
        {
            await _context.Tickets
                 .AddAsync(ticketVM);
        }

        public async Task AddTicketMessage(TicketMessage messageVM)
        {
            await _context.TicketMessages
              .AddAsync(messageVM);
        }

        public async Task<List<Ticket>> GetAllTickets(string userId)
        {
            return await _context.Tickets.AsNoTracking()
                .Where(u => u.OwnerId == userId).ToListAsync();
        }

        public async Task<Ticket> GetTicketByTicketId(string ticketId, string userId)
        {
            return await _context.Tickets.Include(tm => tm.ticketMessages)
                .Where(t => t.Id == ticketId && t.OwnerId == userId).OrderByDescending(t=>t.CreateAt).AsNoTracking().SingleOrDefaultAsync();
        }
    }



    public partial class ContactRepositories
    {
        public async Task SaveChanges()
        {
            await _context.SaveChangesAsync();
        }

        public void UpdateContactUs(ContactUs contactUs)
        {
            _context.ContactUs.Update(contactUs);
        }
    }
}
