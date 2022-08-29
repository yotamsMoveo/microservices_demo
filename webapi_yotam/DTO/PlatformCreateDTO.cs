using System;
using System.ComponentModel.DataAnnotations;

namespace webapi_yotam.DTO
{
    public class PlatformCreateDTO
    {

        [Required]
        public string Name { get; set; }

        [Required]
        public string Publisher { get; set; }

        [Required]
        public string Cost { get; set; }
    }
}

