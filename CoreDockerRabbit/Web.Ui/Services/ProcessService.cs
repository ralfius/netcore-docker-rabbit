using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Web.Contracts;

namespace Web.Ui.Services
{
    public class ProcessService : IProcessService
    {
        public async Task<IEnumerable<Process>> GetProcessesAsync()
        {
            using (var httpClient = new HttpClient())
            {
                // TODO move to configs
                var response = await httpClient.GetAsync("http://webapi/processes");
                response.EnsureSuccessStatusCode();

                return JsonConvert.DeserializeObject<IEnumerable<Process>>(await response.Content.ReadAsStringAsync());
            }
        }
    }
}
