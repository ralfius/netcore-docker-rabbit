using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using Web.Contracts;
using Web.DAL;

namespace Web.Api.Services
{
    class ProcessService : IProcessService
    {
        private readonly WebDbContext _context;
        private readonly IMapper _mapper;


        public ProcessService(WebDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ProcessModel>> GetProcessesAsync()
        {
            return await _mapper.ProjectTo<ProcessModel>(_context.Processes).ToListAsync();
        }
    }
}
