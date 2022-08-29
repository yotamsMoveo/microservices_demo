using System;
using System.ComponentModel.DataAnnotations;

namespace CommandsService.DTO
{
    public class CommandCrreateDTO
    {


        [Required]
        public string HowTo { get; set; }

        [Required]
        public string CommandLine { get; set; }


  
        public CommandCrreateDTO()
        {
        }
    }
}

