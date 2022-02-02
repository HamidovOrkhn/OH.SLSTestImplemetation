using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SLSImplementation.Filter;
using SLSImplementation.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace SLSImplementation.Controllers
{
   // [OAuthClient]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult SetSession([FromQuery] string token,[FromQuery] int date)
        {
            DateTimeOffset dateTimeOffset = DateTimeOffset.FromUnixTimeSeconds(date);
            DateTime dt = dateTimeOffset.LocalDateTime;
            HttpContext.Response.Cookies.Append("e_token", token, new CookieOptions { Secure = true, HttpOnly = true, Expires = dt });
            return RedirectToAction("Index");
        }
        //public IActionResult Logout()
        //{
        //    if (HttpContext.Session.GetString("ssid_token") is object)
        //    {
        //        HttpContext.Session.Remove("ssid_token");
        //    }
        //    return Redirect("http://localhost:61999/auth/login");
        //}

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
