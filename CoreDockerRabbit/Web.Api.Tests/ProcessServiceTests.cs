using AutoMapper;
using NSubstitute;
using System;
using System.Threading.Tasks;
using Web.Api.Services;
using Web.Common.Services;
using Web.DAL;
using Web.DAL.Entities;
using Xunit;

namespace Web.Api.Tests
{
    public class ProcessServiceTests
    {
        [Fact]
        public async Task StartProcessesAsync_ShouldCreateNewProcess_AndCallMessageBusService()
        {
            // Arrange
            var messageBusMock = NSubstitute.Substitute.For<IMessageBusService>();
            var dbContextMock = NSubstitute.Substitute.For<IWebDbContext>();
            var sut = new ProcessService(dbContextMock, GetMapper(), messageBusMock);

            // Act
            await sut.StartProcessesAsync();

            // Assert
            await dbContextMock.Processes.Received(1).AddAsync(Arg.Is<Process>(p => p.Status == ProcessStatus.Queued));
            await dbContextMock.Received(1).SaveChangesAsync();
            messageBusMock.Received(1).Send("processes", Arg.Is<Process>(p => p.Status == ProcessStatus.Queued));
        }

        private IMapper GetMapper()
        {
            var config = new MapperConfiguration(cfg => {
                cfg.AddProfile<AutomapperProfile>();
            });

            return config.CreateMapper();
        }
    }
}
