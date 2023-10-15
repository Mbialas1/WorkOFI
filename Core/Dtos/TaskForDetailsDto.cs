using Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Dtos
{
    public class TaskForDetailsDto
    {
        public long Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime CreatedDate { get; set; }
        public string NameOfUser { get; set; } = string.Empty;
        public DateTime LastEditTime { get; set; }
        public TimeOnly TottalRemaining { get; set; }
        public string TaskStatus { get; set; } = string.Empty;
    }
}
