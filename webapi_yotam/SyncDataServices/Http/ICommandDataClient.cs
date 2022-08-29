using System;
using webapi_yotam.DTO;

namespace webapi_yotam.SyncDataServices.Http
{
    public interface ICommandDataClient
    {
        Task SendPlatformToCommand(PlatformReadDTO plat);
    }
}

