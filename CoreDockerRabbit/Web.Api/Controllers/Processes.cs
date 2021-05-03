using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Web.Api.Services;
using Web.Contracts;
using Web.DAL;
using ProcessStatus = Web.Contracts.ProcessStatus;

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
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<Processes>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<Processes>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
