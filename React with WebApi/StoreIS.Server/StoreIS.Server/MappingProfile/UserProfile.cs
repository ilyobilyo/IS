using AutoMapper;
using Microsoft.AspNetCore.Identity;
using StoreIS.Server.Models.User;

namespace StoreIS.Server.MappingProfile
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<IdentityUser, UserModel>();
        }
    }
}
