using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa.AccesoDatos
{
    public abstract class GenericRepository<T> where T : class
    {
        protected readonly string _connectionString;

        protected GenericRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        protected SqlConnection GetConnection()
        {
            return new SqlConnection(_connectionString);
        }

        protected object GetDbValue(object value)
        {
            return value ?? DBNull.Value;
        }
    }


}
