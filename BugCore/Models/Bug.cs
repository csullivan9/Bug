using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BugCore.Models
{
    public class Bug
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public Severity BugSeverity {get; set;}

        public string Severity { get; set; }

        public string Status { get; set; }
    }

    public enum Severity
    {
        Low,
        Medium,
        High,
    }
}
