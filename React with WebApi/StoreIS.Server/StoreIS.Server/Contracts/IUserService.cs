using StoreIS.Server.Models.User;

namespace StoreIS.Server.Contracts
{
    public interface IUserService
    {
        Task<UserResponseModel> RegisterUser(RegisterInputModel model);
        Task<UserResponseModel> GetById(string id);
        Task<UserResponseModel> Login(LoginInputModel model);
    }
}
