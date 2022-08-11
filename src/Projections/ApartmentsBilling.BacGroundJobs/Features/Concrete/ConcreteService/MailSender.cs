using ApartmentsBilling.BacGroundJobs.Features.Abstract.AbstracService;
using Microsoft.Extensions.Configuration;
using System;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace ApartmentsBilling.BacGroundJobs.Features.Concrete.ConcreteService
{
    public class MailSender : IMailSender
    {
        private readonly IConfiguration _configuration;
        public MailSender(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public Task<bool> SenMail(string Mail, string password)
        {
            MailMessage msg = new()
            {
                Subject = "Kayıt",
                From = new MailAddress(_configuration["MailAuth:mail"])
            };
            msg.To.Add(new MailAddress(Mail));
            msg.IsBodyHtml = true;
            msg.Body = $"Sistemimize kayıt oldunuz şifreniz:{password}";
            msg.Priority = MailPriority.High;
            var client = new SmtpClient("smtp.mailtrap.io", 2525)
            {
                Credentials = new NetworkCredential("4dd6a18b5f5cc3", "2a197535bfeb90"),
                EnableSsl = true
            };

            try
            {
                client.Send(msg);
                return Task.FromResult(true);
            }
            catch (Exception)
            {
                return Task.FromResult(false);
            }

        }
    }

}
