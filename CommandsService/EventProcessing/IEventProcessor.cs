using System;
namespace CommandsService.EventProcessing
{
    public interface IEventProcessor
    {
        void ProcessEvent(string massage);
    }
}

