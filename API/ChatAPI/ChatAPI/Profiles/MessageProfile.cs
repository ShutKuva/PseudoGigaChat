using AutoMapper;
using Core;
using Core.DTOs;

namespace ChatAPI.Profiles
{
    public class MessageProfile : Profile
    {
        public MessageProfile()
        {
            CreateMap<Message, MessageDTO>();
        }
    }
}
