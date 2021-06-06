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

        public WebDbContext(DbContextOptions options): base (options) { }
    }
}
