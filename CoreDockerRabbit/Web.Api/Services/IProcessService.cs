using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Common.Models;

namespace Web.Api.Services
{
    public interface IProcessService
    {
        public Task<IEnumerable<ProcessModel>> GetProcessesAsync();
        public Task StartProcessesAsync();
    }
}
