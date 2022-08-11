using ApartmentsBilling.BacGroundJobs.Features.Abstract;
using ApartmentsBilling.BacGroundJobs.Features.Abstract.AbstracService;
using System;

namespace ApartmentsBilling.BacGroundJobs.Features.Concrete.HangFireJobs
{
    public class Jobs : IJobs
    {
        private readonly IMailSender _mailSender;
        public Jobs(IMailSender mailSender)
        {
            _mailSender = mailSender;
        }
        public void DelayedJob(int userId, string userName, TimeSpan timeSpan)
        {
            throw new NotImplementedException();
        }

        public bool FireAndForget(string mail, string password)
        {
            try
            {
                Hangfire.BackgroundJob.Enqueue(() => _mailSender.SenMail(mail, password));
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }

        public void ReccuringJob()
        {
            throw new NotImplementedException();
        }
    }
}
