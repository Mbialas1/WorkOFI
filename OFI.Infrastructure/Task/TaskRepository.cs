using Core.InterfaceRepository;
using Microsoft.Extensions.Configuration;
using OFI.Infrastructure.Helpers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OFI.Infrastructure.Task
{
    public class TaskRepository : ITaskRepository
    {
        private readonly IDbConnection dbConnection;
        public TaskRepository(IConfiguration configuration) 
        { 
            dbConnection = ConnectionHelper.GetSqlConnection(configuration);
        }
    }
}
