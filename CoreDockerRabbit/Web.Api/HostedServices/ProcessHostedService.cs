using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Web.Common;
using Web.Common.Models;
using Web.Common.Services;
using Web.DAL;
using Web.DAL.Entities;

namespace Web.Api.HostedServices
{
    public class ProcessHostedService : IHostedService, IDisposable
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IMessageBusService _messageBusService;
        private readonly IMapper _mapper;
        private WebQueueChannel _queueChannel;

        public ProcessHostedService(IServiceProvider serviceProvider, IMessageBusService messageBusService, IMapper mapper)
        {
            _serviceProvider = serviceProvider;
            _messageBusService = messageBusService;
            _mapper = mapper;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            //TODO add logging
            _messageBusService.GetChannel<ProcessModel>(Constants.ProcessQueueName, async (processModel) =>
            {
                using (var scope = _serviceProvider.CreateScope())
                {
                    using (var dbContext = scope.ServiceProvider.GetRequiredService<WebDbContext>())
                    {
                        var process = dbContext.Processes
                            .Where(p => p.ProcessId == processModel.ProcessId)
                            .SingleOrDefault();

                        if (process.Status == DAL.Entities.ProcessStatus.Queued)
                        {
                            process.Status = DAL.Entities.ProcessStatus.InProgress;
                            await dbContext.SaveChangesAsync();

                            // imitation of running task
                            await Task.Delay(10000);
                            process.Status = DAL.Entities.ProcessStatus.Completed;
                            await dbContext.SaveChangesAsync();

                            _messageBusService.Send(Constants.ProcessQueueName, _mapper.Map<Process, ProcessModel>(process));
                        }
                    }
                }
            });

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _queueChannel?.Dispose();

            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _queueChannel?.Dispose();
        }
    }
}
