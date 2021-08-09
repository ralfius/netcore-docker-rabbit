using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Web.Common.Models;
using Web.Common.Services;
using Web.DAL;
using Web.DAL.Entities;

namespace Web.Api.Services
{
    public class ProcessService : IProcessService
    {
        private readonly IWebDbContext _context;
        private readonly IMapper _mapper;
        private readonly IMessageBusService _messageBusService;


        public ProcessService(IWebDbContext context, IMapper mapper, IMessageBusService messageBusService)
        {
            _context = context;
            _mapper = mapper;
            _messageBusService = messageBusService;
        }

        public async Task<IEnumerable<ProcessModel>> GetProcessesAsync()
        {
            return await _mapper.ProjectTo<ProcessModel>(_context.Processes).ToListAsync();
        }

        public async Task StartProcessesAsync()
        {
            var newProcess = _mapper.Map<ProcessModel, Process>(
                new ProcessModel()
                {
                    Created = DateTime.Now,
                    ProcessId = Guid.NewGuid(),
                    Status = Common.Models.ProcessStatus.Queued
                });
            await _context.Processes.AddAsync(newProcess);
            await _context.SaveChangesAsync();
            _messageBusService.Send("processes", newProcess);
        }
    }
}
