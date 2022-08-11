using Microsoft.AspNetCore.Mvc;

namespace ApartmentsBilling.WebApp.Controllers
{
    public class ErrorController : Controller
    {
        [Route("Error/404")]
        public IActionResult NotFoundPage()
        {
            return View();
        }

        [Route("Error/403")]
        public IActionResult UnAutHorized()
        {
            return View();
        }
    }
}
