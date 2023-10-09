using Core.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.Commands.Tasks
{
    public class LogTimeTaskCommand : IRequest<LogTimeTaskDTO>
    {
        public LogTimeTaskDTO LogTimeTaskDTO { get; set; }
        public long IdUser { get; set; }    //TODO Future add information in user api too.

        public LogTimeTaskCommand(LogTimeTaskDTO logTimeTaskDTO)
        {
            LogTimeTaskDTO = logTimeTaskDTO;
        }
    }
}
