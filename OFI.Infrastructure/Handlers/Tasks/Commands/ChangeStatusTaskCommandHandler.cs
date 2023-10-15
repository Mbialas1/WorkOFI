using Core.Application.Commands.Tasks;
using Core.Dtos;
using Core.InterfaceRepository;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OFI.Infrastructure.Handlers.Tasks.Commands
{
    public class ChangeStatusTaskCommandHandler : IRequestHandler<ChangeStatusTaskCommand, UpdateTaskStatusDTO>
    {
        private readonly ITaskRepository taskRepository;

        public ChangeStatusTaskCommandHandler(ITaskRepository _taskRepository)
        {
            taskRepository = _taskRepository;
        }

        public async Task<UpdateTaskStatusDTO> Handle(ChangeStatusTaskCommand request, CancellationToken cancellationToken)
        {
            try
            {
                bool resultOk = await taskRepository.UpdateTaskStatus(request.ModelDto);
                if (resultOk == false)
                {
                    throw new ValidationException();
                }

                return new UpdateTaskStatusDTO(); //Not need return anything
            }
            catch
            {
                throw;
            }
        }
    }
}
