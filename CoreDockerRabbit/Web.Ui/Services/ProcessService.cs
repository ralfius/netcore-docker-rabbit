using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Web.Common;
using Web.Common.Models;

namespace Web.Ui.Services
{
    public class ProcessService : IProcessService
    {
        private readonly IConfiguration _configuration;
        private readonly Uri _processesUrl;
        private const string ProcessesPath = "processes";

        public ProcessService(IConfiguration configuration)
        {
            _configuration = configuration;
            _processesUrl = new Uri(new Uri(_configuration[Constants.WebApiBaseUrlKey]), ProcessesPath);
        }

        public async Task<IEnumerable<ProcessModel>> GetProcessesAsync()
        {
            using (var httpClient = new HttpClient())
            {
                var response = await httpClient.GetAsync(_processesUrl);
                response.EnsureSuccessStatusCode();

                return JsonConvert.DeserializeObject<IEnumerable<ProcessModel>>(await response.Content.ReadAsStringAsync());
            }
        }

        public async Task<ProcessModel> StartProcessAsync()
        {
            using (var httpClient = new HttpClient())
            {
                var response = await httpClient.PostAsync(_processesUrl, new StringContent(string.Empty));
                response.EnsureSuccessStatusCode();

                return JsonConvert.DeserializeObject<ProcessModel>(await response.Content.ReadAsStringAsync());
            }
        }
    }
}
