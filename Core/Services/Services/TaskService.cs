using Core.Application.Dtos;
using Core.Enums;
using Core.InterfaceRepository;
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
        private readonly ITaskRepository taskRepository;
        public TaskService(ITaskRepository _taskRepository) 
        { 
            taskRepository = _taskRepository;
        }

        public async Task<IEnumerable<TaskForDashboardDto>> GetTaskForDashboardByUserId(int userId)
        {
            throw new NotImplementedException();
            //var tasks = await taskRepository.GetByUserIdAsync(userId);
            //return tasks.Select(task => new TaskForDashboardDto
            //{
            //    Name = task.Name,
            //    Description = task.Description,
            //    TaskStatusStatus = (TaskStatusEnum)task.Progress
            //}).ToList();
        }
    }
}
