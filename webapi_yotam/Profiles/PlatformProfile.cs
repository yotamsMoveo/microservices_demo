using System;
using AutoMapper;
using webapi_yotam.DTO;
using webapi_yotam.Models;

namespace webapi_yotam.Profiles
{
    public class PlatformProfile:Profile
    {
        public PlatformProfile()
        {
            //source -> target
            CreateMap<Platform, PlatformReadDTO>();
            CreateMap<PlatformCreateDTO, Platform>();
            CreateMap<PlatformReadDTO, PlatformPublishedDTO>();
        }
    }
}

