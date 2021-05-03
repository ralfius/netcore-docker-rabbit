using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.DAL.Entities;

namespace Web.DAL
{
    public class WebDbContext : DbContext
    {
        public DbSet<Process> Processes { get; set; }

        public WebDbContext()
        {
            //Database.EnsureCreated();
            Database.Migrate();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // TODO move to config
            optionsBuilder.UseNpgsql("Host=localhost;Port=5433;Database=webdb;Username=webuser;Password=WebP@ss!");
        }
    }
}
