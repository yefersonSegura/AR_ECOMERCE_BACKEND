
using Microsoft.Data.SqlClient;
using System.Data;

namespace AR.Repository.Base
{
    public class BaseRepository
    {
        protected readonly string connectionString;
        protected BaseRepository(string connectionString)
        {
            this.connectionString = connectionString;
            DbConnection = new SqlConnection(this.connectionString);
        }
        protected SqlConnection DbConnection { get; }
    }
}