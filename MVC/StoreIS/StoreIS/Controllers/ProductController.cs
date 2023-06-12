using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using StoreIS.Contracts;
using StoreIS.Models.Product;

namespace StoreIS.Controllers
{
    public class ProductController : Controller
    {
        private readonly ICategoryService categoryService;
        private readonly IProductService productService;
        private readonly IMapper mapper;


        public ProductController(ICategoryService categoryService,
            IProductService productService,
            IMapper mapper)
        {
            this.categoryService = categoryService;
            this.productService = productService;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var viewModel = new CreateProductViewModel
            {
                Categories = await categoryService.GetAll()
            };


            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateProductViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Error", "/Home", new { message = "Invalid data" });
            }

            try
            {
                await productService.CreateProduct(model);

                return Redirect("/");
            }
            catch (Exception e)
            {
                return RedirectToAction("Error", "/Home", new { message = e.Message });
            }
        }

        [HttpGet]
        public async Task<IActionResult> All(string categoryId = null)
        {
            var products = categoryId != null 
                ? await productService.GetAllProducts(categoryId) 
                : await productService.GetAllProducts();

            var viewModel = new AllProductViewModel
            {
                Categories = await categoryService.GetAll(),
                Products = products
            };

            return View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid productId)
        {
            var product = await productService.GetById(productId);

            var categories = await categoryService.GetAll();

            var model = mapper.Map<EditProductViewModel>(product);

            model.Categories = categories;

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditProductViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Error", "/Home", new { message = "Invalid data" });
            }

            try
            {
                await productService.UpdateProduct(model);

                return Redirect("/Product/All");
            }
            catch (Exception e)
            {
                return RedirectToAction("Error", "/Home", new { message = e.Message });
            }
        }
    }
}
