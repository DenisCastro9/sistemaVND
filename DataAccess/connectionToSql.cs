using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace DataAccess
{
    public abstract class connectionToSql
    {
        private readonly string connectionString;
        public connectionToSql()
        {
            connectionString = "data source= DESKTOP-949RM2I; initial catalog=sistemaVND;integrated security=true";
        }
        protected SqlConnection GetConnection()
        {
            return new SqlConnection(connectionString);
        }
    }
}
