using Microsoft.EntityFrameworkCore;
using Web.DAL.Entities;

namespace Web.DAL
{
    public class WebDbContext : DbContext, IWebDbContext
    {
        public DbSet<Process> Processes { get; set; }

        public WebDbContext(DbContextOptions options): base (options) { }
    }
}
