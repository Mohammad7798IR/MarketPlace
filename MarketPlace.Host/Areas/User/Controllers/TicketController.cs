using GoogleReCaptcha.V3.Interface;
using Infa.Application.Extentions;
using Infa.Application.Interfaces;
using Infa.Domain.ViewModels.Contact;
using Microsoft.AspNetCore.Mvc;
using static Infa.Domain.ViewModels.Contact.FilterTicketVM;

namespace MarketPlace.Host.Areas.User.Controllers
{
    public partial class TicketController : BaseController
    {
        private readonly IUserServices _userServices;
        private readonly ICaptchaValidator _captchaValidator;
        private readonly IContactServices _contactServices;

        public TicketController
            (
            IUserServices userServices,
            ICaptchaValidator captchaValidator,
            IContactServices contactServices
            )
        {
            _userServices = userServices;
            _captchaValidator = captchaValidator;
            _contactServices = contactServices;
        }
    }
    public partial class TicketController : BaseController
    {
        [Route("tickets")]
        public async Task<IActionResult> Index(FilterTicketVM ticketVM )
        {

            ticketVM.FilterTicketStates = FilterTicketState.NotDeleted;
            ticketVM.OrderBy = FilterTicketOrder.CreateDate_DES;
            ticketVM.Tickets = await _contactServices.FilterTickets(ticketVM, User.GetUserId());
            return View(ticketVM);
        }

        #region SubmitTicket

        [Route("SubmitTicket")]
        public IActionResult SubmitTicket()
        {
            return View();
        }

        [Route("SubmitTicket")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SubmitTicket(AddUserTicketVM ticketVM)
        {
            if (await _captchaValidator.IsCaptchaPassedAsync(ticketVM.Token))
            {
                if (ModelState.IsValid)
                {
                    var result = await _contactServices.AddTicket(ticketVM, User.GetUserId());

                    switch (result)
                    {
                        case AddTicketResult.Success:
                            TempData[SuccessMessage] = "تیکت شما با موفقیت ثبت شد";
                            TempData[InfoMessage] = "پاسخ شما به زودی ارسال خواهد شد";
                            return Redirect("user");

                        case AddTicketResult.Failure:
                            TempData[ErrorMessage] = "مشکلی پیش امد لطفا دوباره تلاش کنید";
                            return View(ticketVM);

                        default:
                            break;
                    }
                }
                TempData[ErrorMessage] = "اعتبار سنجی انجام نشد";
            }
            return View(ticketVM);
        }

        #endregion


        #region TicketDetail


        [Route("tickets/{ticketId}")]
        [HttpGet]
        public async Task<IActionResult> TicketDetail(string ticketId)
        {
            var ticket = await _contactServices.GetTicketDetail(ticketId, User.GetUserId());
            if (ticket == null) return NotFound();

            return View(ticket);
        }
        
        
        
        #endregion

    }
}
