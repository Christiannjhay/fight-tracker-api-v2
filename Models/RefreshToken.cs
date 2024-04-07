using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using apiv2.models;

namespace apiv2.Models
{
    public class RefreshToken
    {
         public int Id { get; set; }

        public int UserId { get; set; } 
        [ForeignKey("UserId")] 
        public User? User { get; set; } 

        public string Token { get; set; } = string.Empty;
        public DateTime Expires { get; set; } 
        public bool IsRevoked { get; set; }

        public DateTime Created { get; set; } = DateTime.UtcNow;

        public DateTimeOffset? Revoked { get; set; } 
       
        public string ReplacedByToken { get; set; } = string.Empty;

        public bool IsExpired => DateTime.UtcNow >= Expires;
        public bool IsActive => Revoked == null && !IsExpired; 
    }
}