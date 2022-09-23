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
            var email = new MimeMessage();
            email.Sender = MailboxAddress.Parse(_configuration["MailAuth:mail"]);
            email.To.Add(MailboxAddress.Parse(Mail));
            email.Subject = "Kayıt";
            var builder = new BodyBuilder
            {
                HtmlBody = $"Apartman Sistemimize kayıt oldunuz şifreniz : <H2> {password} </H2>"
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
