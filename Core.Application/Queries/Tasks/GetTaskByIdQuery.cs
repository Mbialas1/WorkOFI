using Core.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.Queries
{
    public class GetTaskByIdQuery : IRequest<TaskForDetailsDto>
    {
        public long TaskId { get; set; }

        public GetTaskByIdQuery(long taskId)
        {
            TaskId = taskId;
        }
    }
}
