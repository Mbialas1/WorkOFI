using Core.Dtos;
using Core.Entities.Task;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Core.Application.Commands
{
    public class AddTaskCommand : IRequest<TaskAggregate>
    {
        public AddTaskDto TaskDto { get; set; } 
        public AddTaskCommand(AddTaskDto taskDto) { 
            TaskDto = taskDto;
        }
    }
}
