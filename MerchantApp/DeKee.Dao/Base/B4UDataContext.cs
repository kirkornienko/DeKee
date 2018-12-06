using DeKee.Base.Entities.Audit;
using DeKee.ElasticContext.Base;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeKee.Dao.Base
{
    public class B4UQueryLib
    {
        public class Select
        {
            public static string ClientMainWallet(out string paramName)
            {
                paramName = "@phone";
                return string.Format(@"SELECT  top 1 clw.tcw_address
      FROM [dbo].[T_CLIENT] cli
	  inner join dbo.T_CLIENT_WALLET clw on cli.tcl_id = clw.tcw_cliend_Id
	  where cli.tcl_phone = {0} and (tcw_is_deleted <> 1 or tcw_is_deleted is null)
	  
	  order by tcw_is_main desc", paramName);

            }

            public static string ClientEmailInUse(out string paramName)
            {
                paramName = "@email";
                return string.Format(@"SELECT  1 
      FROM [dbo].[T_CLIENT] cli
	  where cli.tcl_email = {0}", paramName);

            }
        }
    }
    public class B4UDataContext : IDbContext
    {
        public B4UDataContext()
        {
            connectionString = ConfigurationManager.ConnectionStrings["B4U"].ConnectionString;
        }
        string connectionString;
        HandyElasticRepository<Event> repository = new HandyElasticRepository<Event>();

        public TResult Execute<TResult>(string q, Action<SqlParameterCollection> action, Func<SqlDataReader, TResult> readerlamda)
        {
            string level = "info";
            string type = "query";
            string message = $"SQL: {q}";
            WrightToLog(message, level, type);

            using (SqlConnection connection =
            new SqlConnection(connectionString))
            {
                // Create the Command and Parameter objects.
                SqlCommand command = new SqlCommand(q, connection);
                action(command.Parameters);
                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    var result = readerlamda(reader);
                    reader.Close();
                    return result;
                }
                catch (Exception ex)
                {
                    level = "Error";
                    type = "Exception";
                    message += $"\nException: {ex}";
                    return default(TResult);
                    //Console.WriteLine(ex.Message);
                }
                finally
                {
                    WrightToLog(message, level, type);

                }
            }
        }
        public TResult Execute<TResult>(string q, Func<SqlDataReader, TResult> readerlamda)
        {
            string level = "info";
            string type = "query";
            string message = $"SQL: {q}";
            WrightToLog(message, level, type);

            using (SqlConnection connection =
            new SqlConnection(connectionString))
            {
                // Create the Command and Parameter objects.
                try
                {
                    SqlCommand command = new SqlCommand(q, connection);
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    var result = readerlamda(reader);
                    reader.Close();
                    return result;
                }
                catch (Exception ex)
                {
                    level = "Error";
                    type = "Exception";
                    message += $"\nException: {ex}";
                    //throw;
                    return default(TResult);
                    //Console.WriteLine(ex.Message);
                }
                finally
                {
                    WrightToLog(message, level, type);

                }
            }
        }

        private void WrightToLog(string message, string level, string type)
        {
            repository.Push(new Event() { DateCreated = DateTime.Now, Message = $"Message: {message}", Type = type, Level = level, Module = GetType().Name, SessonId = "DBSession" });
        }
    }
}
