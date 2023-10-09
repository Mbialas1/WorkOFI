using Core.Application.Commands.Tasks;
using Core.Dtos;
using Core.Entities.Task;
using Core.InterfaceRepository;
using MediatR;
using OFI.Common.Handler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace OFI.Infrastructure.Handlers.Tasks.Commands
{
    public class LogTimeTaskCommandHandler : IRequestHandler<LogTimeTaskCommand, LogTimeTaskDTO>
    {
        private readonly ITaskRepository taskRepository;

        public LogTimeTaskCommandHandler(ITaskRepository _taskRepository)
        {
                taskRepository = _taskRepository;
        }

        public async Task<LogTimeTaskDTO> Handle(LogTimeTaskCommand request, CancellationToken cancellationToken)
        {
            try
            {
                TimeOnly convertedTime = LogTimeTaskHandler.ConvertTimeTextToRealyTime(request.LogTimeTaskDTO.Time);
                if(convertedTime == TimeOnly.MinValue)
                {
                    throw new ArgumentException("Time is 0. What you want add?");
                }

                TimeOnly currentLoggedTime = await taskRepository.GetLoggedTimeByIdTask(request.LogTimeTaskDTO.TaskId);
                TimeSpan timeSpan = currentLoggedTime.ToTimeSpan().Add(convertedTime.ToTimeSpan());
                TimeOnly newLoggedTime = TimeOnly.FromTimeSpan(timeSpan);
                LogTimeTask logTimeTask = new LogTimeTask()
                {
                    TaskTime = newLoggedTime,
                    LoggedTime = newLoggedTime.ToShortTimeString(),
                    TaskId = request.LogTimeTaskDTO.TaskId,
                    LoggedDate = DateTime.UtcNow
                };

                bool result = await taskRepository.LogTimeToTaskById(logTimeTask);
                if (!result)
                {
                    throw new ArgumentException("Cant logg time to task");
                }

                return null;
            }
            catch
            {
                throw;
            }
        }
    }
}
