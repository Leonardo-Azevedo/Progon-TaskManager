using Microsoft.Data.SqlClient;

namespace Progon.Infrastructure.Data
{
    public class DbConnection
    {
        SqlConnection _connection;

        //Construct
        public DbConnection(string stringConnection)
        {
            _connection = new SqlConnection(stringConnection);
        }

        public SqlConnection connect()
        {
            if(_connection.State == System.Data.ConnectionState.Closed)
            {
                _connection.Open();
            }

            return _connection;
        }

        public void disconnect()
        {
            if(_connection.State == System.Data.ConnectionState.Open)
            {
                _connection.Close();
            }
        }
    }
}
