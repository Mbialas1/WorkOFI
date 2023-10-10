﻿using Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.Dtos
{
    public class TaskForDashboardDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string TaskStatus { get; set; }
        public TimeOnly TotalRemaing { get; set; }
    }
}
