using ChristmasGiftApp.ViewModels;
using Microsoft.AspNetCore.Mvc;
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

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Draw(EmployeeViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var result = await _giftService.AssignRandomEmployee(viewModel.EmailAddress);
                return View(result);
            }
            else
                return RedirectToAction("Index");
        }
    }
}