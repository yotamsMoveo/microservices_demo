using System.Collections.Generic;
using Microsoft.CodeAnalysis;
using webapi_yotam.Models;
using Platform = webapi_yotam.Models.Platform;

namespace webapi_yotam.Data
{
    public interface IPlatformRepo
    {
        bool SaveChanges();
        IEnumerable<Platform> GetAllPlatforms();
        Platform GetPlatformById(int id);
        void CreatePlatform(Platform plat);
    }
}
