using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities.Task
{
    public class LogTimeTask
    {
        public long TaskId { get; set; }    
        public string LoggedTime { get; set; } = string.Empty;
        public TimeOnly TaskTime { get; set; }  
        public DateTime LoggedDate { get; set; }
    }
}
