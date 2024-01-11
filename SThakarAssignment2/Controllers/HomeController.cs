using Microsoft.AspNetCore.Mvc;
using SThakarAssignment2.Models;
using SThakarAssignment2.Services;
using System.Diagnostics;

namespace SThakarAssignment2.Controllers
{

    public class HomeController : Controller    
    {
        private readonly ILogger<HomeController> _logger;
        private readonly CookieServices _cookieManager;
        public HomeController(ILogger<HomeController> logger, CookieServices cookieManager)
        {
            _logger = logger;
            _cookieManager = cookieManager;
        }

        public IActionResult Index()
        {  
            string welcomeMsg = _cookieManager.GetWelcomeMessage();
            ViewData["WelcomeMessage"] = welcomeMsg;
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}