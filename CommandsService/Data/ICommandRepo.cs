using System;
using CommandsService.Models;

namespace CommandsService.Data
{
    public interface ICommandRepo
    {
        bool SaveChanges();

        IEnumerable<Platform> GetAllPlatforms();

        void CreatePlatform(Platform plat);

        bool PlatforExits(int platformId);
        bool ExternalPlatformExits(int ExternalPlatform);

        //////commands

        IEnumerable<Command> GetCommandsForPlatform(int platformId);

        Command getCommand(int platformId,int commandId);

        void CreateCommand(int platformId, Command command);
    }
}

