using Core.Services.InterfaceServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services.Services
{
    public class TaskService : ITaskService
    {
        private readonly ITaskService taskService;
        public TaskService(ITaskService _taskService) 
        { 
            taskService = _taskService;
        }
    }
}
