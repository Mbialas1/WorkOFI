using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OFI.Infrastructure.User.RabbitMQ.Models
{
    public class TaskMessage
    {
        public int TaskId { get; set; }
        public int UserId { get; set; }
    }
}
