using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using Web.Contracts;

namespace Web.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class Processes : ControllerBase
    {
        [HttpGet]
        public IEnumerable<Process> Get()
        {
            return new List<Process>()
            {
                new Process { ProcessId = Guid.NewGuid(), Status = ProcessStatus.Queued },
                new Process { ProcessId = Guid.NewGuid(), Status = ProcessStatus.InProgress },
                new Process { ProcessId = Guid.NewGuid(), Status = ProcessStatus.Completed }
            };
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
