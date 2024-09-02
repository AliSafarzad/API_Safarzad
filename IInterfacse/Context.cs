using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IInterfacse
{
    public class Context
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString = null;
        public Context(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("SqlConnection");
        }
        public System.Data.IDbConnection CreateConnection() => new SqlConnection(_connectionString);
    }
}
