using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StoreIS.Contracts;
using StoreIS.Server.Models;
using StoreIS.Server.Models.Category;

namespace StoreIS.Server.Controllers
{
    [Route("Category")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService categoryService;
        private readonly IMapper mapper;

        public CategoryController(ICategoryService categoryService, IMapper mapper)
        {
            this.categoryService = categoryService;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var response = await categoryService.GetAll();

            return Ok(response);
        }

        [HttpGet("{categoryId:guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid categoryId)
        {
            try
            {
                var response = await categoryService.GetById(categoryId);

                return Ok(response);
            }
            catch (Exception e)
            {
                return NotFound(new ResponseModel { Message = e.Message, Status = StatusCodes.Status404NotFound });
            }

        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create(CategoryInputModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new ResponseModel { Status = StatusCodes.Status400BadRequest, Message = "Invalid Data" });
            }

            try
            {
                var response = await categoryService.CreateCategory(model);

                return CreatedAtAction(nameof(GetById), new { categoryId = response.Category.Id }, response);
            }
            catch (Exception e)
            {
                return BadRequest(new ResponseModel { Message = e.Message, Status = StatusCodes.Status400BadRequest });
            }
        }

        [HttpPut("{categoryId:guid}")]
        [Authorize]
        public async Task<IActionResult> Update([FromRoute] Guid categoryId, [FromBody] CategoryInputModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new ResponseModel { Status = StatusCodes.Status400BadRequest, Message = "Invalid Data" });
            }

            try
            {
                var response = await categoryService.UpdateCategory(categoryId, model);

                return Ok(response);
            }
            catch (Exception e)
            {
                return NotFound(new ResponseModel { Message = e.Message, Status = StatusCodes.Status404NotFound });
            }
        }
    }
}
