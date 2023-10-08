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
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }
        public string NameOfUser { get; set; }
        public DateTime LastEditTime { get; set; }
        public TimeOnly TottalRemaining { get; set; }
        public string TaskStatus { get; set; }
    }
}
