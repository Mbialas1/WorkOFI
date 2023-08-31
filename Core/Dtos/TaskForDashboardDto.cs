using Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.Dtos
{
    public class TaskForDashboardDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public TaskStatusEnum TaskStatusStatus { get; set; }    
    }
}
