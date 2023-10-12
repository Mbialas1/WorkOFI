using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Dtos
{
    public class LogTimeTaskDTO
    {
        public string Time { get; set; } = string.Empty;   
        public long TaskId { get; set; }
    }
}
