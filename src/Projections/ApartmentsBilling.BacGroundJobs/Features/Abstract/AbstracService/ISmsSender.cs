using System.Threading.Tasks;

namespace ApartmentsBilling.BacGroundJobs.Features.Abstract.AbstracService
{
    public interface IMailSender
    {
        Task<bool> SenMail(string Mail, string password);
    }
}
