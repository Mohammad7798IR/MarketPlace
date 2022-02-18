using Common.ClassHelpers;
using Infa.Application.Interfaces;
using Infa.Domain.Interfaces;
using Infa.Domain.Models.Contacts;
using Infa.Domain.Models.Identity;
using Infa.Domain.ViewModels.Contact;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Infa.Domain.ViewModels.Contact.FilterTicketVM;

namespace Infa.Application.Services
{
    public partial class ContactServices : IContactServices
    {
        private readonly IContactRepositories _contactRepositories;
        private readonly UserManager<ApplicationUser> _userManager;

        public ContactServices(IContactRepositories contactRepositories, UserManager<ApplicationUser> userManager)
        {
            _contactRepositories = contactRepositories;
            _userManager = userManager;
        }

       
    }


    public partial class ContactServices
    {


        public async Task<AddTicketResult> AddTicket(AddUserTicketVM ticketVM, string userId)
        {
            if (string.IsNullOrEmpty(ticketVM.Text))
            {
                return AddTicketResult.Failure;
            }

            var NewTicket = new Ticket()
            {
                Id = Guid.NewGuid().ToString(),
                OwnerId = userId,
                CreateAt = PersianDateTime.Now(),
                Title = ticketVM.Title,
                IsReadByOwner = true,
                TicketPriority = ticketVM.TicketPriority,
                TicketSection = ticketVM.TicketSection,
                TicketState = TicketState.UnderProgress,
                IsReadByAdmin = false
            };

            await _contactRepositories.AddTicket(NewTicket);
            await _contactRepositories.SaveChanges();




            var ticketMessage = new TicketMessage()
            {
                Id = Guid.NewGuid().ToString(),
                CreateAt = PersianDateTime.Now(),
                SenderId = userId,
                Text = ticketVM.Text,
                TicketId = NewTicket.Id,
                IsDeleted = false,
            };
            await _contactRepositories.AddTicketMessage(ticketMessage);
            await _contactRepositories.SaveChanges();

            return AddTicketResult.Success;
        }

        public async Task CreateContactUs(ContactUsVM ContactUsVM, string UserIp, string? UserId)
        {
            var NewContactUs = new ContactUs()
            {
                Id = Guid.NewGuid().ToString(),
                UserId = UserId,
                UserIp = UserIp,
                Email = ContactUsVM.Email,
                FullName = ContactUsVM.FullName,
                Subject = ContactUsVM.Subject,
                Text = ContactUsVM.Text,
                EditedAt = null,
                PostedAt = PersianDateTime.Now(),
            };

            await _contactRepositories.AddContactUs(NewContactUs);
            await _contactRepositories.SaveChanges();



        }

        public async Task<List<Ticket>> FilterTickets(FilterTicketVM filterTicketVM, string userId)
        {
            var query = _contactRepositories.GetAllTickets(userId).Result;

            switch (filterTicketVM.FilterTicketStates)
            {
                case FilterTicketState.All:
                    break;

                case FilterTicketState.Deleted:
                    query = query.Where(t => t.IsDeleted == true).ToList();
                    break;

                case FilterTicketState.NotDeleted:
                    query = query.Where(t => t.IsDeleted == false).ToList();
                    break;

                default:
                    break;
            }

            switch (filterTicketVM.OrderBy)
            {
                case FilterTicketOrder.CreateDate_DES:
                    query = query.OrderByDescending(t => t.CreateAt).ToList();
                    break;

                case FilterTicketOrder.CreateDate_ASC:
                    query = query.OrderBy(t => t.CreateAt).ToList();
                    break;

                default:
                    break;
            }

            if (filterTicketVM.TicketSection != null)
            {
                switch (filterTicketVM.TicketSection)
                {

                    case TicketSection.Support:
                        query = query.Where(ts => ts.TicketSection == filterTicketVM.TicketSection.Value).ToList();
                        break;
                    case TicketSection.Technical:
                        query = query.Where(ts => ts.TicketSection == filterTicketVM.TicketSection.Value).ToList();
                        break;
                    case TicketSection.Academic:
                        query = query.Where(ts => ts.TicketSection == filterTicketVM.TicketSection.Value).ToList();
                        break;
                }
            }

            if (filterTicketVM.TicketPriority != null)
            {
                switch (filterTicketVM.TicketPriority)
                {

                    case TicketPriority.High:
                        query = query.Where(ts => ts.TicketPriority == filterTicketVM.TicketPriority.Value).ToList();
                        break;
                    case TicketPriority.Medium:
                        query = query.Where(ts => ts.TicketPriority == filterTicketVM.TicketPriority.Value).ToList();
                        break;
                    case TicketPriority.Low:
                        query = query.Where(ts => ts.TicketPriority == filterTicketVM.TicketPriority.Value).ToList();
                        break;

                }
            }
        

            if (!string.IsNullOrEmpty(filterTicketVM.Title))
                query = query.Where(s => EF.Functions.Like(s.Title, $"%{filterTicketVM.Title}%")).ToList();

        

            return query;
        }

        public async Task<TicketDetailsVM> GetTicketDetail(string ticketId, string userId)
    {

        var foundTicket = _contactRepositories.GetTicketByTicketId(ticketId, userId).Result;
        var owner = _userManager.FindByIdAsync(userId);
        foundTicket.Owner = await owner;

        if (foundTicket != null && foundTicket.OwnerId == userId)
        {

            return new TicketDetailsVM { Ticket = foundTicket, TicketMessage = foundTicket.ticketMessages.ToList(), User = foundTicket.Owner };
        }
        return null;
    }

}
}
