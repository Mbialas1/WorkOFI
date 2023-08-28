using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities.Task
{
    public class TaskModel
    {
        public long Id { get; set; }
        public long UserId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public Task BaseTask { get; set; }
        public List<Task> SubTasks { get; set; }
        public Task() { }
    }
}
