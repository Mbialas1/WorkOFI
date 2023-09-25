using Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Dtos
{
    public class AddTaskDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime Created { get; set; }

            //Future: Project name, asigne user etc.

    }
}
