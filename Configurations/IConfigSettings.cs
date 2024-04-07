using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace apiv2.Configurations
{
    public interface IConfigSettings
    {
        string JwtKey { get; }
        string JwtIssuer { get; }
        string JwtAudience { get; }
        double JwtExpirationDays { get; } 
        int RefreshTokenExpirationDays { get; }
    }
}