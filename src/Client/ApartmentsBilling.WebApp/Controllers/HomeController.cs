using ApartmentsBilling.Common.Dtos.BillTypeDto;
using ApartmentsBilling.Common.Dtos.CustomDto;
using ApartmentsBilling.WebApp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace ApartmentsBilling.WebApp.Controllers
{
    public class HomeController : BaseController
    {


        public IActionResult Index()
        {

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public async Task<IActionResult> GetBillType()
        {
            try
            {
                var response = await _client.GetFromJsonAsync<CustomResponseDto<List<GetBillTypeDto>>>(_client.BaseAddress + "BillType");
                ViewBag.data = response.Data;
                return View();
            }
            catch (System.Exception)
            {
                return RedirectToAction("Forbidden", "Login");
            }

        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult NotFoundPage()
        {
            return View();
        }
    }
}
