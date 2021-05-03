using System;

namespace Web.Contracts
{
    public class ProcessModel
    {
        public DateTime Created { get; set; }
        public Guid ProcessId { get; set; }
        public ProcessStatus Status { get; set; }
    }
}
