using Core.InterfaceRepository;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using OFI.Infrastructure.Helpers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace OFI.Infrastructure.User
{
    public class UserRepository : IUserRepository
    {
        private readonly IDbConnection dbConnection;

        public UserRepository(IConfiguration configuration)
        {
            dbConnection = ConnectionHelper.GetSqlConnection(configuration);
        }
    }
}
