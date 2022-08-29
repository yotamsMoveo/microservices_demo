using System;
using System.ComponentModel.DataAnnotations;

namespace webapi_yotam.DTO
{
    public class PlatformReadDTO
    {

        public int Id { get; set; }
        
        public string Name { get; set; }
        
        public string Publisher { get; set; }
        
        public string Cost { get; set; }
    }
}

