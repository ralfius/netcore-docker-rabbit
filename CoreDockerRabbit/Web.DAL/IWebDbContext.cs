using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Threading;
using System.Threading.Tasks;
using Web.DAL.Entities;

namespace Web.DAL
{
    public interface IWebDbContext : IDisposable
    {
        #region DbContext members

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
        DatabaseFacade Database { get; }

        #endregion DbContext members

        DbSet<Process> Processes { get; set; }
    }
}
