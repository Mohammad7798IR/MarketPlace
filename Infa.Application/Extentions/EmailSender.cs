using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Infa.Application.Extentions
{
    public class EmailSender
    {
        private readonly IConfiguration _configuration;

        public EmailSender(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async void Send(string email, string subject, string template)
        {
            MailMessage message = new MailMessage();
            SmtpClient smtpClient = new SmtpClient();


            message.From = new MailAddress("mohammadbookshoptest@gmail.com", "MarketPlace");

            message.To.Add(email);

            message.Body = template;

            message.Subject = subject;

            message.IsBodyHtml = true;


            smtpClient.Port = 587;
            smtpClient.Host = "smtp.gmail.com";
            smtpClient.Credentials = new NetworkCredential(_configuration.GetSection("Email")["EmailAddress"], _configuration.GetSection("Email")["Password"]);
            smtpClient.EnableSsl = true;

            await smtpClient.SendMailAsync(message);

        }
    }
}
