using System;
namespace Web.DAL.Entities
{
    public class Process
    {
        public DateTime Created { get; set; }
        public Guid ProcessId { get; set; }
        public ProcessStatus Status { get; set; }
    }
}
