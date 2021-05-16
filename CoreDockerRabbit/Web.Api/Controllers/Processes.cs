using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Web.Api.Services;
using Web.Common.Models;
using Web.DAL;
using ProcessStatus = Web.Common.Models.ProcessStatus;

namespace Web.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class Processes : ControllerBase
    {
        private readonly IProcessService _service;
        public Processes(IProcessService service)
        {
            _service = service;
        }

        [HttpGet]
        public Task<IEnumerable<ProcessModel>> Get()
        {
            return _service.GetProcessesAsync();
        }

        // POST api/<Processes>
        [HttpPost]
        public Task Post()
        {
            return _service.StartProcessesAsync();
        }
    }
}
