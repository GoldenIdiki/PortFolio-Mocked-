using GoldPortFolio.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GoldPortFolio.Data
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser>
    {
        private readonly DbContextOptions _options;

        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
            _options = options;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Profile> ProfileTbl { get; set; }
        public DbSet<Address> AddressTbl { get; set; }  
        public DbSet<WorkExperience> WorkExperienceTbl { get; set; }
        public DbSet<ContactPage> ContactPageTbl { get; set; }
    }
}
