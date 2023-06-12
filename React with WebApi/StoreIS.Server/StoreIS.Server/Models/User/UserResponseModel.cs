namespace StoreIS.Server.Models.User
{
    public class UserResponseModel : ResponseModel
    {
        public UserModel User { get; set; }

        public string Token { get; set; }
    }
}
