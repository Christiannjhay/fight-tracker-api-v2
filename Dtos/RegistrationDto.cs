using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace apiv2.Dtos
{
    public class RegistrationDto
    {
        public string Name { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public DateTimeOffset Birthday { get; set; }

        public int CoupleCode { get; set; }
        public int CoupleUserId { get; internal set; }
    }
}