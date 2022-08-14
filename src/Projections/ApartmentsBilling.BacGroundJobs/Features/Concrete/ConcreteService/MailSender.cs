using ApartmentsBilling.BacGroundJobs.Features.Abstract.AbstracService;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Configuration;
using MimeKit;
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
        public async Task<bool> SenMail(string Mail, string password)
        {
            //MailMessage msg = new()
            //{
            //    Subject = "Kayıt",
            //    From = new MailAddress(_configuration["MailAuth:mail"])
            //};
            //msg.To.Add(new MailAddress(Mail));
            //msg.IsBodyHtml = true;
            //msg.Body = $"Apartman Sistemimize kayıt oldunuz şifreniz:{password}";
            //msg.Priority = MailPriority.High;
            //var smtp = new SmtpClient
            //{
            //    Host = "smtp.gmail.com",   //gmail example
            //    Port = 587,
            //    EnableSsl = false,
            //    DeliveryMethod = SmtpDeliveryMethod.Network,
            //    UseDefaultCredentials = false,
            //    Credentials = new NetworkCredential(_configuration["MailAuth:mail"], _configuration["MailAuth:pass"])
            //};


            //try
            //{
            //    smtp.Send(msg);
            //    return Task.FromResult(true);
            //}
            //catch (Exception)
            //{
            //    return Task.FromResult(false);
            //}
            var email = new MimeMessage();
            email.Sender = MailboxAddress.Parse(_configuration["MailAuth:mail"]);
            email.To.Add(MailboxAddress.Parse(Mail));
            email.Subject = "Kayıt";
            var builder = new BodyBuilder
            {
                HtmlBody = $"Apartman Sistemimize kayıt oldunuz şifreniz:{password}"
            };
            email.Body = builder.ToMessageBody();
            using var smtp = new SmtpClient();
            smtp.Connect("smtp.gmail.com", 587, SecureSocketOptions.StartTls);
            var v = _configuration["MailAuth:mail"];
            var p = _configuration["MailAuth:pass"];
            smtp.Authenticate(v, p);
            await smtp.SendAsync(email);
            smtp.Disconnect(true);
            return true;
        }
    }

}
