using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StoreIS.Contracts;
using StoreIS.Server.Models;
using StoreIS.Server.Models.Product;

namespace StoreIS.Server.Controllers
{
    [Route("Product")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService productService;

        public ProductController(IProductService productService)
        {
            this.productService = productService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(string? categoryId = null)
        {
            var response = await productService.GetAllProducts(categoryId);

            return Ok(response);
        }

        [HttpGet("{productId:guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid productId)
        {
            try
            {
                var response = await productService.GetById(productId);

                return Ok(response);
            }
            catch (Exception e)
            {
                return NotFound(new ResponseModel { Message = e.Message, Status = StatusCodes.Status404NotFound });
            }

        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create([FromForm] ProductInputModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new ResponseModel { Status = StatusCodes.Status400BadRequest, Message = "Invalid Data" });
            }

            try
            {
                var response = await productService.CreateProduct(model);

                return CreatedAtAction(nameof(GetById), new { productId = response.Product.Id }, response);
            }
            catch (Exception e)
            {
                return BadRequest(new ResponseModel { Message = "hello", Status = StatusCodes.Status400BadRequest });
            }
        }

        [HttpPut("{productId:guid}")]
        [Authorize]
        public async Task<IActionResult> Update([FromRoute] Guid productId, [FromForm] ProductInputModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new ResponseModel { Status = StatusCodes.Status400BadRequest, Message = "Invalid Data" });
            }

            try
            {
                var response = await productService.UpdateProduct(productId, model);

                return Ok(response);
            }
            catch (Exception e)
            {
                return NotFound(new ResponseModel { Message = e.Message, Status = StatusCodes.Status404NotFound });
            }
        }
    }
}
