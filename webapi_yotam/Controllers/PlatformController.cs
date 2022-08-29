using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using webapi_yotam.AsyncDataServices;
using webapi_yotam.Data;
using webapi_yotam.DTO;
using webapi_yotam.Models;
using webapi_yotam.SyncDataServices.Http;

namespace webapi_yotam.Controllers
{
    [Route("api/platforms/[controller]")]
    [ApiController]
    public class PlatformController : ControllerBase
    {
        private readonly IPlatformRepo _repo;
        private readonly IMapper _mapper;
        private readonly ICommandDataClient _commandDataClient;
        private readonly IMassageBusClient _massageBusClient;

        public PlatformController(
            IPlatformRepo repo,
            IMapper mapper,
            ICommandDataClient commandDataClient,
            IMassageBusClient massageBusClint
            )
        {
            _repo = repo;
            _mapper = mapper;
            _commandDataClient = commandDataClient;
            _massageBusClient = massageBusClint;

        }

        [HttpGet]
        public ActionResult<IEnumerable<PlatformReadDTO>> GetPlatforms()
        {
            Console.WriteLine("--> getting Platforms..");

            var platformItem = _repo.GetAllPlatforms();

            return Ok(_mapper.Map<IEnumerable<PlatformReadDTO>>(platformItem));

        }

        [HttpGet("{id}", Name ="GetPlatformById")]
        public ActionResult<IEnumerable<PlatformReadDTO>> GetPlatformById(int id)
        {
            var platformItem = _repo.GetPlatformById(id);
            if (platformItem != null)
            {
                return Ok(_mapper.Map<PlatformReadDTO>(platformItem));
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost]
        public async Task<ActionResult<PlatformReadDTO>> CreatePlatform(PlatformCreateDTO platformCreateDTO)
        {
            var platformModel = _mapper.Map<Platform>(platformCreateDTO);
            _repo.CreatePlatform(platformModel);
            _repo.SaveChanges();

            var platformReadDTO = _mapper.Map<PlatformReadDTO>(platformModel);

            //sync massage
            try
            {
                await _commandDataClient.SendPlatformToCommand(platformReadDTO);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }


            //async massage
            try
            {
                var platformPublishedDto = _mapper.Map<PlatformPublishedDTO>(platformReadDTO);
                platformPublishedDto.Event = "Platform_Published";
                _massageBusClient.PublishNewPlatform(platformPublishedDto);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);

            }

            return CreatedAtRoute(nameof(GetPlatformById), new { Id = platformReadDTO.Id }, platformReadDTO);
        }
        
    }
}
