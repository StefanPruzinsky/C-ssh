using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace DbORM
{
    public class DatabaseCoreHelper
    {
        public string ConnectionString { get; private set; }

        private SqlConnection SqlConnection;

        public DatabaseCoreHelper(string connectionString)
        {
            ConnectionString = connectionString;
        }

        public void Connect()
        {
            SqlConnection = new SqlConnection(ConnectionString);
            SqlConnection.Open();
        }

        public Object RunCommand(string commandText)
        {
            SqlCommand sqlCommand = new SqlCommand(commandText, SqlConnection);

            return sqlCommand.ExecuteScalar();
        }
    }
}
