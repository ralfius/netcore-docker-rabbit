using System;

namespace Web.Contracts
{
    public class Process
    {
        public Guid ProcessId { get; set; }
        public ProcessStatus Status { get; set; }
    }
}
