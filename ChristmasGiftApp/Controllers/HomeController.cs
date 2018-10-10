using ChristmasGiftApp.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Threading.Tasks;

namespace ChristmasGiftApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly GiftService _giftService;
        public HomeController(GiftService giftService)
        {
            _giftService = giftService;

        }
        public async Task<IActionResult> Index()
        {
            var res =await _giftService.AssignRandomEmployee("k.brown@company.com");
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

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