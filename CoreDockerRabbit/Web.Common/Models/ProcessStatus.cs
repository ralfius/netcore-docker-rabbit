using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web.Common.Models
{
    public enum ProcessStatus
    {
        Queued = 0,
        [JsonProperty("In Progress")]
        InProgress,
        Completed
    }
}
