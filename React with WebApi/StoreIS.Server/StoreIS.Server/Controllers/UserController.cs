using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using StoreIS.Server.Contracts;
using StoreIS.Server.Models;
using StoreIS.Server.Models.User;

namespace StoreIS.Server.Controllers
{
    [Route("User")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService userService;
        private readonly UserManager<IdentityUser> userManager;
        private readonly SignInManager<IdentityUser> signInManager;

        public UserController(IUserService userService,
            SignInManager<IdentityUser> signInManager,
            UserManager<IdentityUser> userManager)
        {
            this.userService = userService;
            this.signInManager = signInManager;
            this.userManager = userManager;

        }

        [HttpGet("{userId:guid}")]
        public async Task<IActionResult> GetById([FromRoute] string userId)
        {
            try
            {
                var response = await userService.GetById(userId);

                return Ok(response);
            }
            catch (Exception e)
            {
                return NotFound(new ResponseModel { Message = e.Message, Status = StatusCodes.Status404NotFound });
            }

        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register(RegisterInputModel model)
        {
            try
            {
                var response = await userService.RegisterUser(model);

                return CreatedAtAction(nameof(GetById), new { userId = response.User.Id }, response);
            }
            catch (Exception e)
            {
                return BadRequest(new ResponseModel { Message = e.Message, Status = StatusCodes.Status400BadRequest });
            }
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginInputModel model)
        {
            try
            {
                var response = await userService.Login(model);

                return Ok(response);
            }
            catch (Exception e)
            {
                return Unauthorized(new ResponseModel { Message = e.Message, Status = StatusCodes.Status401Unauthorized });
            }
        }

        [HttpPost("Logout")]
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            try
            {
                await signInManager.SignOutAsync();
                return NoContent();
            }
            catch (Exception e)
            {
                return BadRequest(new ResponseModel { Message = e.Message, Status = StatusCodes.Status400BadRequest });
            }
        }
    }
}
