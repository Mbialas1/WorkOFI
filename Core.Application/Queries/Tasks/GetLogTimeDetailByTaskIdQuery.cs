using Core.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.Queries.Tasks
{
    public class GetLogTimeDetailByTaskIdQuery : IRequest<IEnumerable<LogDTO>>
    {
        public readonly long TaskId;

        public GetLogTimeDetailByTaskIdQuery(long taskId)
        {
            this.TaskId = taskId;
        }
    }
}
