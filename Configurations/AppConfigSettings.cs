using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using apiv2.Configurations;
using Microsoft.Extensions.Configuration;

namespace apiv2.Services 
{
    public class AppConfigSettings : IConfigSettings
    {
        private readonly IConfiguration _configuration; 

        public AppConfigSettings(IConfiguration configuration) 
        {
            _configuration = configuration;
        }

        public string JwtKey => _configuration["Jwt:Key"];
        public string JwtIssuer => _configuration["Jwt:Issuer"]; 
        public string JwtAudience => _configuration["Jwt:Audience"]; 
        public double JwtExpirationDays => Convert.ToDouble(_configuration["Jwt:ExpireDays"]);  
        public int RefreshTokenExpirationDays => Convert.ToInt32(_configuration["RefreshTokenExpirationDays"]); 
    }
}
