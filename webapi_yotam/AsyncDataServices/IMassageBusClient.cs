using System;
using webapi_yotam.DTO;

namespace webapi_yotam.AsyncDataServices
{
    public interface IMassageBusClient
    {
        void PublishNewPlatform(PlatformPublishedDTO platformPublishedDTO);
    }
}

