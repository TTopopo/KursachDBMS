using System;
using System.Data;
using System.Data.SqlClient;

namespace courseDataBase.Connection
{
    class DataBase
    {
        SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-J9BJ5LB\SQLEXPRESS;Initial Catalog=Construction_management;Integrated Security=True");

        public void openConnection()
        {
            if (con.State == System.Data.ConnectionState.Closed)
            {
                con.Open();
            }
        }

        public void closeConnection()
        {
            if (con.State == System.Data.ConnectionState.Open)
            {
                con.Close();
            }
        }

        public SqlConnection getConnection()
        {
            return con;
        }
    }
}
