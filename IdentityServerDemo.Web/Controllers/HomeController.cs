using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using IdentityServerDemo.Web.Models;
using System.Threading.Tasks;
using IdentityServerDemo.Web.Services;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using IdentityModel.Client;
using IdentityServerDemo.Web.Services.Interfaces;

namespace IdentityServerDemo.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        public ITokenService _tokenService { get; set; }

        public HomeController (ILogger<HomeController> logger, ITokenService tokenService)
        {
            _logger = logger;
            _tokenService = tokenService;
        }

        public IActionResult Index ()
        {
            return View();
        }

        public IActionResult Privacy ()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error ()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public async Task<IActionResult> Weather ()
        {
            var data = new List<WeatherForecast>();
            var token = await _tokenService.GetToken("myApi.read");
            using (var client = new HttpClient())
            {
                client.SetBearerToken(token.AccessToken);
                var result = await client.GetAsync("https://localhost:44315/weatherforecast");
                if (result.IsSuccessStatusCode)
                {
                    var model = await result.Content.ReadAsStringAsync();
                    data = JsonConvert.DeserializeObject<List<WeatherForecast>>(model);
                    return View(data);
                }
                else
                {
                    throw new System.Exception("Failed to get Data from API");
                }
            }
        }

        public IActionResult Guide ()
        {
            return View();
        }
    }
}
