using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using apiv2.models;
using apiv2.Models;
using Microsoft.EntityFrameworkCore;

namespace apiv2.Data
{
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext()
        {
        }

        public ApplicationDBContext(DbContextOptions dbContextOptions) : base (dbContextOptions)
        {
            
        }

        public DbSet<User> Users {get; set;}

        public DbSet<Couple> Couples {get; set;}

        public DbSet<Fight> Fights {get; set;}

        public DbSet<RefreshToken> RefreshTokens { get; set; } 

        
    }
}