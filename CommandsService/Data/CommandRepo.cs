using System;
using CommandsService.Models;
using Microsoft.CodeAnalysis;
using Platform = CommandsService.Models.Platform;

namespace CommandsService.Data
{
    public class CommandRepo : ICommandRepo
    {
        private readonly AppDbContext _context;

        public CommandRepo(AppDbContext context)
        {
            _context = context;
        }

        public void CreateCommand(int platformId, Command command)
        {
            if (command == null)
            {
                throw new ArgumentException(nameof(command));

            }
            command.PlatformId = platformId;
            _context.Commands.Add(command);
        }

        public void CreatePlatform(Platform plat)
        {
            if (plat==null)
            {
                throw new ArgumentException(nameof(plat));

            }
            _context.Platforms.Add(plat);
        }

        public bool ExternalPlatformExits(int ExternalPlatform)
        {
            return (_context.Platforms.Any(p => p.ExternalID == ExternalPlatform));
        }

        public IEnumerable<Platform> GetAllPlatforms()
        {
            return _context.Platforms.ToList();
        }

        public Command getCommand(int platformId, int commandId)
        {
            return _context.Commands.Where(c => c.PlatformId == platformId && c.Id == commandId).FirstOrDefault();
        }

        public IEnumerable<Command> GetCommandsForPlatform(int platformId)
        {
            return (_context.Commands.Where(c => c.PlatformId == platformId).OrderBy(c=>c.Platform.Name));
        }

        public bool PlatforExits(int platformId)
        {
            return (_context.Platforms.Any(p => p.Id == platformId));
        }

        public bool SaveChanges()
        {
            return (_context.SaveChanges() >= 0);
        }
    }
}

