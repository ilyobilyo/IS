using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using StoreIS.Server.Contracts;
using StoreIS.Server.Data;
using StoreIS.Server.Models.User;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace StoreIS.Server.Services
{
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext dbContext;
        private readonly IConfiguration config;
        private readonly UserManager<IdentityUser> userManager;
        private readonly IMapper mapper;

        public UserService(ApplicationDbContext dbContext,
            IConfiguration config,
            UserManager<IdentityUser> userManager,
            IMapper mapper)
        {
            this.dbContext = dbContext;
            this.config = config;
            this.userManager = userManager;
            this.mapper = mapper;
        }

        public async Task<UserResponseModel> GetById(string userId)
        {
            var user = await dbContext
                .Users
                .FirstOrDefaultAsync(x => x.Id == userId);

            if (user == null)
            {
                throw new ArgumentException("User is not found");
            }

            return new UserResponseModel
            {
                Status = StatusCodes.Status200OK,
                User = mapper.Map<UserModel>(user),
            };
        }

        public async Task<UserResponseModel> Login(LoginInputModel model)
        {
            var user = await userManager.FindByEmailAsync(model.Email);

            if (user != null && await userManager.CheckPasswordAsync(user, model.Password))
            {
                return new UserResponseModel()
                {
                    Message = "Login successfull",
                    Status = StatusCodes.Status200OK,
                    User = mapper.Map<UserModel>(user),
                    Token = GetToken(user),
                };
            }

            throw new ArgumentException("Email or password is invalid. Try again!");
        }

        public async Task<UserResponseModel> RegisterUser(RegisterInputModel model)
        {
            var userExists = await userManager.FindByEmailAsync(model.Email);

            if (userExists != null)
            {
                throw new ArgumentException("User already exists!");
            }

            var user = new IdentityUser()
            {
                Email = model.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = model.Username,
            };

            var result = await userManager.CreateAsync(user, model.Password);

            if (!result.Succeeded)
            {
                throw new ArgumentException("User creation failed! Please check user details and try again.");
            }


            var userModel = mapper.Map<UserModel>(user);

            return new UserResponseModel 
            { 
                Status = StatusCodes.Status201Created, 
                Message = "User created successfully!",
                User = userModel,
                Token = GetToken(user),
            };
        }

        private string GetToken(IdentityUser user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(config["JWT:Secret"]);

            var authClaims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.NameIdentifier, user.Id),
            };

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(authClaims),
                Expires = DateTime.UtcNow.AddDays(7), // Set token expiration
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                Audience = config["JWT:ValidAudience"],
                Issuer = config["JWT:ValidIssuer"],
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            return tokenString;
        }

    }
}
