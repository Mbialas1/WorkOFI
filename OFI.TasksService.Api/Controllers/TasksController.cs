using Core.Application.Dtos;
using Core.Enums;
using Core.InterfaceRepository;
using Microsoft.AspNetCore.Mvc;
using OFI.Infrastructure.Task;

namespace OFI.TasksService.Api.Controllers
{
    public class TasksController : Controller
    {

        private readonly ITaskRepository taskRepository;
        public TasksController(ITaskRepository _taskRepository)
        {
            taskRepository = _taskRepository;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("task/{userId}")]
        public async Task<IEnumerable<TaskForDashboardDto>> GetTaskForDashboardByUserId(int userId)
        {
            var tasks = await taskRepository.GetByUserIdAsync(userId);
            return tasks.Select(task => new TaskForDashboardDto
            {
                Name = task.Name,
                Description = task.Description,
                TaskStatusStatus = (TaskStatusEnum)task.Progress
            }).ToList();
        }
    }
}
