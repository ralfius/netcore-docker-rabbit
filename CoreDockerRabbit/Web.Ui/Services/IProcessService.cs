using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Contracts;

namespace Web.Ui.Services
{
    interface IProcessService
    {
        Task<IEnumerable<ProcessModel>> GetProcessesAsync();
        Task<ProcessModel> StartProcessAsync();
    }
}
