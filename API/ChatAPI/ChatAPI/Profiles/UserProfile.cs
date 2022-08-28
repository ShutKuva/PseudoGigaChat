using AutoMapper;
using Core;
using Core.DTOs;

namespace ChatAPI.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserDTOToUI>();
            CreateMap<UserDTOToUI, User>();
        }
    }
}
