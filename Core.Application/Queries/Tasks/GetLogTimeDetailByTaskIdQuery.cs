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
        public readonly int Page;
        public readonly int PageSize;
        public GetLogTimeDetailByTaskIdQuery(long taskId, int page, int pageSize)
        {
            this.TaskId = taskId;
            Page = page;
            PageSize = pageSize;    
        }
    }
}
