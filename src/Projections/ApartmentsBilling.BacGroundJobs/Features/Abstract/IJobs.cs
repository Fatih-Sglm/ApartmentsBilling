using System;

namespace ApartmentsBilling.BacGroundJobs.Features.Abstract
{
    public interface IJobs
    {
        void DelayedJob(int userId, string userName, TimeSpan timeSpan);
        bool FireAndForget(string mail, string password);
        void ReccuringJob();
    }
}
