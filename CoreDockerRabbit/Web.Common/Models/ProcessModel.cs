using System;

namespace Web.Common.Models
{
    public class ProcessModel
    {
        public DateTime Created { get; set; }
        public Guid ProcessId { get; set; }
        public ProcessStatus Status { get; set; }
    }
}
