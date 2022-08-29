using System;
using System.Text.Json;
using AutoMapper;
using CommandsService.Data;
using CommandsService.DTO;
using CommandsService.Models;

namespace CommandsService.EventProcessing
{
    public class EventProcessor:IEventProcessor
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly IMapper _mapper;

        public EventProcessor(IServiceScopeFactory scopeFactory ,IMapper mapper)
        {
            _scopeFactory = scopeFactory;
            _mapper = mapper;
        }

        public void ProcessEvent(string massage)
        {
            var eventType = DetermineEvent(massage);

            switch (eventType)
            {
                case EventType.PlatformPublished:
                    AddPlatform(massage);
                break;
                default:
                    break;
            }
        }


        private EventType DetermineEvent(string notificationMassage)
        {
            Console.WriteLine("-->determining event");
            var eventType = JsonSerializer.Deserialize<GenericEventDTO>(notificationMassage);
            switch (eventType.Event)
            {
                case "Platform_Published":
                    Console.WriteLine("-->Platform published event detected");
                    return EventType.PlatformPublished;
                default:
                    Console.WriteLine("-->could not determine the event type");
                    return EventType.Undetermined;
            }
        }

        private void AddPlatform(string platformPublishedMassage)
        {
            using(var scope = _scopeFactory.CreateScope())
            {
                var repo = scope.ServiceProvider.GetRequiredService<ICommandRepo>();
                var platformPublishedDto = JsonSerializer.Deserialize<PlatformPublishedDTO>(platformPublishedMassage);

                try
                {
                    var platform = _mapper.Map<Platform>(platformPublishedDto);
                    if (!repo.ExternalPlatformExits(platform.ExternalID))
                    {
                        repo.CreatePlatform(platform);
                        repo.SaveChanges();
                        Console.WriteLine("-->platform added");

                    }
                    else
                    {
                        Console.WriteLine("platform allraedy exisits...");
                    }

                }
                catch (Exception ex)
                {

                }
            }
        }

       
    }
    enum EventType
    {
        PlatformPublished,
        Undetermined
    }
}

