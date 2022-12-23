using AutoMapper;
using System;
using WebSignalRChat.Models;

namespace WebSignalRChat
{
    public class AppMappingProfile : Profile
    {
        public AppMappingProfile()
        {
            CreateMap<SendModel, MapSendModel>();
        }
    }
}
