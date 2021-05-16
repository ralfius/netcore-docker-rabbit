using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Web.Common.Models;

namespace Web.Ui.Services
{
    public class ProcessService : IProcessService
    {
        public async Task<IEnumerable<ProcessModel>> GetProcessesAsync()
        {
            using (var httpClient = new HttpClient())
            {
                // TODO move to configs
                var response = await httpClient.GetAsync("http://localhost:43002/processes");
                response.EnsureSuccessStatusCode();

                return JsonConvert.DeserializeObject<IEnumerable<ProcessModel>>(await response.Content.ReadAsStringAsync());
            }
        }

        public async Task<ProcessModel> StartProcessAsync()
        {
            using (var httpClient = new HttpClient())
            {
                // TODO move to configs
                var response = await httpClient.PostAsync("http://localhost:43002/processes", new StringContent(string.Empty));
                response.EnsureSuccessStatusCode();

                return JsonConvert.DeserializeObject<ProcessModel>(await response.Content.ReadAsStringAsync());
            }
        }
    }
}
