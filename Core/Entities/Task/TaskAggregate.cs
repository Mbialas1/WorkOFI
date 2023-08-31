using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities.Task
{
    public class TaskAggregate
    {
        public long Id { get; set; }
        public long UserId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public TaskAggregate BaseTask { get; set; }
        public List<TaskAggregate> SubTasks { get; set; }
        public int Progress { get; set; }
        public TaskAggregate() { }
    }
}
