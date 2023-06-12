using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using StoreIS.Contracts;
using StoreIS.Models.ShoppingList;
using System.Security.Claims;

namespace StoreIS.Controllers
{
    public class ShoppingListController : Controller
    {
        private readonly IShoppingListService shoppingListService;
        private readonly IMapper mapper;

        public ShoppingListController(IShoppingListService shoppingListService,
            IMapper mapper)
        {
            this.shoppingListService = shoppingListService;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateShoppingListViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Error", "/Home", new { message = "Invalid data" });
            }

            try
            {
                await shoppingListService.CreateShoppingList(model);

                return Redirect("/");
            }
            catch (Exception e)
            {
                return RedirectToAction("Error", "/Home", new { message = e.Message });
            }

        }

        [HttpGet]
        public async Task<IActionResult> Details(Guid listId)
        {
            var list = await shoppingListService.GetListDetails(listId);

            return View(list);
        }

        [HttpGet]
        public async Task<IActionResult> GetMyLists()
        {
            var lists = await shoppingListService.GetMyShoppingLists(User.FindFirstValue(ClaimTypes.NameIdentifier));

            return Ok(lists);
        }

        [HttpPost]
        public async Task<IActionResult> AddToList([FromBody] AddProductInputModel model)
        {
            var isProductAdded = await shoppingListService.AddProductToList(model);

            if (isProductAdded)
            {
                return Ok();
            }
            else
            {
                return BadRequest(new { message = "Product already is in list" });
            }
        }

        [HttpPost]
        public async Task<IActionResult> BuyProduct([FromBody] BuyProductInputModel model)
        {
            var isProductAdded = await shoppingListService.BuyProduct(model);

            if (isProductAdded)
            {
                return Ok();
            }
            else
            {
                return BadRequest(new { message = "Product is already buyed" });
            }

        }

        [HttpGet]
        public async Task<IActionResult> DeleteList(Guid id)
        {
            try
            {
                var isDeletedSucceed = await shoppingListService.DeleteList(id);

                if (isDeletedSucceed)
                {
                    return Redirect("/");
                }
                else
                {
                    return RedirectToAction("Error", "/Home", new { message = "Deletion failed!" });
                }
            }
            catch (Exception e)
            {
                return RedirectToAction("Error", "/Home", new { message = e.Message });
            }

        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var list = await shoppingListService.GetById(id);

            var model = mapper.Map<EditListViewModel>(list);

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditListViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Error", "/Home", new { message = "Invalid data" });
            }

            try
            {
                await shoppingListService.UpdateList(model);

                return Redirect("/");
            }
            catch (Exception e)
            {
                return RedirectToAction("Error", "/Home", new { message = e.Message });
            }
        }
    }
}
