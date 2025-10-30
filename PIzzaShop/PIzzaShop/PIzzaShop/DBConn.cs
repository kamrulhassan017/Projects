using System;
using System.Data.SqlClient;

namespace PIzzaShop
{
    public class DBConn
    {
        // Connection string for SQL Server
        private static string connectionString = "Data Source=LAPTOP-B3L9CGRC\\DIPKHASTAGIR;Initial Catalog=Pizza;Integrated Security=True;Encrypt=False";

        // Method to get a new SQL connection
        public static SqlConnection GetConnection()
        {
            return new SqlConnection(connectionString);
        }
    }
}
