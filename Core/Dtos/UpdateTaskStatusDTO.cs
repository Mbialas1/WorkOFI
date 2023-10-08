using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Dtos
{
    public class UpdateTaskStatusDTO
    {
        public long TaskId {  get; set; }
        public int StatusTaskId { get; set; }
    }
}
