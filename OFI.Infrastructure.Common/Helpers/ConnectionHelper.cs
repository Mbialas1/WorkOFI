﻿using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Protocols;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OFI.Infrastructure.Helpers
{
    public class ConnectionHelper
    {
        public static SqlConnection GetSqlConnection(IConfiguration configuration) 
            => new SqlConnection(configuration.GetConnectionString(DBHelper.DB_CONNECTION));
    }
}
