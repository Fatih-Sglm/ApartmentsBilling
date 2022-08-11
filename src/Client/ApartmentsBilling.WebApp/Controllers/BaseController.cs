
using ApartmentsBilling.Common.Dtos.CustomDto;
using ApartmentsBilling.Common.Dtos.SystemDto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ApartmentsBilling.WebApp.Controllers
{
    public class BaseController : Controller
    {
        protected readonly Uri _baseAdress = new("https://localhost:5001/api/");
        protected readonly HttpClient _client;

        public BaseController()
        {
            _client = new HttpClient
            {
                BaseAddress = _baseAdress
            };
        }

        public async Task<IActionResult> ErrorView(HttpResponseMessage response)
        {
            return (int)response.StatusCode switch
            {
                403 => RedirectToAction("Forbidden", "Error"),
                404 => RedirectToAction("NotFoundPage", "Error", TempData["Error"] = await response.Content.ReadFromJsonAsync<List<string>>()),
                _ => RedirectToAction("NotFoundPage", "Error"),
            };
        }
        public async Task<IActionResult> Error400(HttpResponseMessage response, object obj = null)
        {

            var body = await response.Content.ReadFromJsonAsync<CustomResponseDto<NoContent>>();
            ViewBag.Errors = body.Errors;
            return View(obj);

        }

        protected bool IsAuthentic()
        {

            if (HttpContext.Session.GetString("Authorization") == null || HttpContext.Session.GetString("Authorization") == string.Empty)
            {
                return false;
            }
            _client.DefaultRequestHeaders.Add("Authorization", "Bearer " + HttpContext.Session.GetString("Authorization").ToString());
            return true;
        }
        protected void SetHttpContext(CustomResponseDto<TokenDto> body)
        {
            var handler = new JwtSecurityTokenHandler();
            HttpContext.Session.SetString("Authorization", body.Data.accessToken);
            var tokens = handler.ReadJwtToken(body.Data.accessToken);
            HttpContext.Session.SetString("Id", tokens.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.NameIdentifier).Value);
            HttpContext.Session.SetString("UserName", tokens.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.GivenName).Value);
            HttpContext.Session.SetString("Role", tokens.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.Role).Value);
        }
    }
}
