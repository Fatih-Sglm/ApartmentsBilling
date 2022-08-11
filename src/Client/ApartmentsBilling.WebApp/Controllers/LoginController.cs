using ApartmentsBilling.Common.Dtos.CustomDto;
using ApartmentsBilling.Common.Dtos.SystemDto;
using ApartmentsBilling.Common.Dtos.UserDtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace ApartmentsBilling.WebApp.Controllers
{
    public class LoginController : BaseController
    {
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginUserDto loginUserDto)
        {
            var response = await _client.PostAsJsonAsync("Auth/login", loginUserDto);
            if (response.IsSuccessStatusCode)
            {
                var body = await response.Content.ReadFromJsonAsync<CustomResponseDto<TokenDto>>();
                SetHttpContext(body);
                return RedirectToAction("ListBill", "Bill");
            }

            else if ((int)response.StatusCode == 400)
            {
                return await Error400(response, loginUserDto);
            }

            return await ErrorView(response);
        }

    }
}
