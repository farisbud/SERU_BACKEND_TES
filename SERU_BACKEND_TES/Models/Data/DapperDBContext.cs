using Microsoft.Data.SqlClient;
using System.Data;

namespace SERU_BACKEND_TES.Models.Data
{
    public class DapperDBContext
    {
        private readonly IConfiguration _configuration;
        private readonly string _connection;

        public DapperDBContext(IConfiguration configuration)
        {
            this._configuration = configuration;
            this._connection = this._configuration.GetConnectionString("DefaultConn");
        }

        public IDbConnection CreateConnection() => new SqlConnection(_connection);
    }
}
