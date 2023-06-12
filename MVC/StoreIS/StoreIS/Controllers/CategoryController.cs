using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using StoreIS.Contracts;
using StoreIS.Models.Category;

namespace StoreIS.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryService categoryService;
        private readonly IMapper mapper;

        public CategoryController(ICategoryService categoryService,
            IMapper mapper)
        {
            this.categoryService = categoryService;
            this.mapper = mapper;

        }

        public async Task<IActionResult> Index()
        {
            var categories = await categoryService.GetAll();

            return View(categories);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateCategoryViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            try
            {
                await categoryService.CreateCategory(model);

                return Redirect("/");
            }
            catch (Exception e)
            {
                return RedirectToAction("Error", "/Home", new { message = e.Message });
            }

        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var category = await categoryService.GetById(id);

            var viewModel = mapper.Map<CreateCategoryViewModel>(category);

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Guid id, CreateCategoryViewModel model)
        {
            if (await categoryService.UpdateCategory(id, model))
            {
                return Redirect("/");
            }


            return View(model);
        }
    }
}
