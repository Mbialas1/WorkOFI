using Core.Application.Queries.Tasks;
using Core.Dtos;
using Core.Entities.Task;
using Core.InterfaceRepository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OFI.Infrastructure.Handlers.Tasks.Queries
{
    public class GetLogTimeDetailByTaskIdQueryHandler : IRequestHandler<GetLogTimeDetailByTaskIdQuery, IEnumerable<LogDTO>>
    {
        private readonly ITaskRepository taskRepository;

        public GetLogTimeDetailByTaskIdQueryHandler(ITaskRepository _taskRepository)
        {
            this.taskRepository = _taskRepository;
        }

        public async Task<IEnumerable<LogDTO>> Handle(GetLogTimeDetailByTaskIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                IEnumerable<LogTimeTask> result = await taskRepository.GetLogTimeTasksByTaskId(request.TaskId, request.Page, request.PageSize);

                IEnumerable<LogDTO> logs = result.Select(s => new LogDTO()
                {
                    TimeSpent =  TimeOnly.Parse(s.LoggedTime),
                    Date = s.LoggedDate
                });

                return logs;
            }
            catch
            {
                throw;
            }
        }
    }
}
