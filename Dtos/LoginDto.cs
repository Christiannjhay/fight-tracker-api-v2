using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace apiv2.Dtos
{
    public class LoginDto
    {
        
        public required string Username { get; set; } 

        public required string Password { get; set; } 
    }
}