using Microsoft.AspNetCore.Mvc;
using StoreIS.Contracts;
using StoreIS.Models;
using System.Diagnostics;
using System.Security.Claims;

namespace StoreIS.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IShoppingListService shoppingListService;

        public HomeController(ILogger<HomeController> logger,
            IShoppingListService shoppingListService)
        {
            _logger = logger;
            this.shoppingListService = shoppingListService;
        }

        public async Task<IActionResult> Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                var lists = await shoppingListService.GetMyShoppingLists(User.FindFirstValue(ClaimTypes.NameIdentifier));

                return View(lists);
            }
            else
            {
                return View();
            }
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error(string message)
        {
            return View(new ErrorViewModel { Message = message });
        }
    }
}