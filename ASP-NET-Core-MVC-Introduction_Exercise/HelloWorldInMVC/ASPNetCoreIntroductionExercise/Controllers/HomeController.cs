using ASPNetCoreIntroductionExercise.Models;
using ASPNetCoreIntroductionExercise.Service;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Diagnostics.Metrics;
using System.Text.Json;

namespace ASPNetCoreIntroductionExercise.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            ViewBag.Message = "Hello World!";
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewBag.Message = "This is an ASP.NET Core MVC app.";
            return View();
        }

        public IActionResult Numbers()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> NumbersToN(int count = 3)
        {
            if (count > 100000)
            {
                return Redirect("NumbersToN");
            }
            var str = await NumberService.Counter(count);
            ViewBag.Count = count;
            ViewBag.Numbers = str;
            return View();
        }

        [HttpGet]
        public IActionResult NumbersToN()
        {
            ViewBag.Message = "Enter a number to count to (3 is default and 100 000 is max for performance reasons)";
            return View();
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}