using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CommandsService.Data;
using CommandsService.DTO;
using CommandsService.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CommandsService.Controllers
{
    [Route("api/commands/pltforms/{platformId}/[controller]")]
    [ApiController]
    public class CommandsController : ControllerBase
    {
        private readonly ICommandRepo _repo;
        private readonly IMapper _mapper;

        public CommandsController(ICommandRepo repo,IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<IEnumerable<CommandReadDTO>> GetCommandsFromPlatform(int platformId)
        {
            Console.WriteLine($"--> hit getCommandsFromPlatform:{ platformId}");
            if (!_repo.PlatforExits(platformId))
            {
                return NotFound();
            }
            var commands = _repo.GetCommandsForPlatform(platformId);
            return Ok(_mapper.Map<IEnumerable<CommandReadDTO>>(commands));
        }


        [HttpGet("{commandId}",Name="GetCommandForPlatform")]
        public  ActionResult<IEnumerable<CommandReadDTO>> GetCommandForPlatform(int platformId,int commandId)
        {
            Console.WriteLine($"-->hit GetCommandFromPlatform: {platformId}/{commandId}");

            if (!_repo.PlatforExits(platformId))
            {
                return NotFound();
            }

            var command = _repo.getCommand(platformId, commandId);
            if (command == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<CommandReadDTO>(command));
        }

        [HttpPost]
        public ActionResult<CommandReadDTO> CrerateCommandForPlatform(int platformId, CommandCrreateDTO commandDto)
        {
            Console.WriteLine($"-->hit CrerateCommandForPlatform: {platformId}");

            if (!_repo.PlatforExits(platformId))
            {
                return NotFound();
            }
            var command = _mapper.Map<Command>(commandDto);

            _repo.CreateCommand(platformId, command);
            _repo.SaveChanges();

            var commandReadDto = _mapper.Map<CommandReadDTO>(command);
            return CreatedAtRoute(nameof(GetCommandForPlatform), new { platformId = platformId, commandId = commandReadDto.Id }, commandReadDto);

        }


    }
}
