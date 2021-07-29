using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Polly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Threading.Tasks;
using Web.Api.HostedServices;
using Web.Api.Services;
using Web.Common;
using Web.Common.Services;
using Web.DAL;

namespace Web.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Web.Api", Version = "v1" });
            });
            services.AddDbContext<WebDbContext>(o => o.UseNpgsql(Configuration[Web.Common.Constants.WebDbConnectionStringKey]));
            services.AddTransient<IProcessService, ProcessService>();
            services.AddSingleton<IMessageBusService, MessageBusService>();
            services.AddAutoMapper(typeof(Startup));
            services.AddHostedService<ProcessHostedService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Web.Api v1"));
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            MigrateDb(app);
        }

        public void MigrateDb(IApplicationBuilder builder)
        {
            using (var scope = builder.ApplicationServices.CreateScope())
            using (var dbContext = scope.ServiceProvider.GetService<WebDbContext>())
            {
                Policy
                    .Handle<SocketException>()
                    .WaitAndRetry(
                        int.Parse(Configuration[Constants.WebDbConnectionRetryNumberKey]), 
                        attempt => TimeSpan.FromSeconds(int.Parse(Configuration[Constants.WebDbConnectionRetryIntervalKey])))
                    .Execute(() => dbContext.Database.Migrate());
            }
        }
    }
}
