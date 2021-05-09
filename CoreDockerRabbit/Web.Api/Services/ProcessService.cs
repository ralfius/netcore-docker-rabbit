using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Web.Contracts;
using Web.DAL;
using Web.DAL.Entities;

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

        public async Task StartProcessesAsync()
        {
            await _context.Processes.AddAsync(
                _mapper.Map<ProcessModel, Process>(
                    new ProcessModel()
                    {
                        Created = DateTime.Now,
                        ProcessId = Guid.NewGuid(),
                        Status = Contracts.ProcessStatus.Queued
                    }
                ));
            await _context.SaveChangesAsync();
        }
    }
}
