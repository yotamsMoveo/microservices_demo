using System;
using AutoMapper;
using CommandsService.DTO;
using CommandsService.Models;

namespace CommandsService.Profiels
{
    public class CommandsProfiels:Profile
    {

        public CommandsProfiels()
        {
            // source-->target

            CreateMap<Platform, PlatformReadDTO>();
            CreateMap<CommandCrreateDTO, Command>();
            CreateMap<Command, CommandCrreateDTO>();
            CreateMap<PlatformPublishedDTO, Platform>().ForMember(dest => dest.ExternalID, opt => opt.MapFrom(src => src.Id));
            
;        }
    }
}

