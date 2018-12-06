using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeKee.Dao.Base
{
    public class BaseDao
    {
        public object Execute(string queryString)
        {
            string connectionString = "Data Source=(local);Initial Catalog=b4u;"
            + "Integrated Security=true";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                // Create the Command and Parameter objects.
                SqlCommand command = new SqlCommand(queryString, connection);
                
                // Open the connection in a try/catch block. 
                // Create and execute the DataReader, writing the result
                // set to the console window.
                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        
                    }
                    reader.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                return new object();
                //Console.ReadLine();
            }
        }
    }
}
