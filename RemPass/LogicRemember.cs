using Betacomio_Project.ConnectDb;
using Microsoft.Data.SqlClient;
using NLog;
using RegexCheck;
using System.Data;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Betacomio_Project.RemPass
{
    public class LogicRemember
    {
        SqlConnection sqlConnection = new SqlConnection();
        private readonly SingleTonConnectDB _connect;
        private static Logger logger = LogManager.GetCurrentClassLogger();

        public LogicRemember(SingleTonConnectDB connect )
        {
            _connect = connect;
        }

         public void connectDB(string connection)
        {
            try
            {
                if (sqlConnection.State == System.Data.ConnectionState.Closed)
                {
                    sqlConnection.ConnectionString = connection;
                    sqlConnection.Open();
               

                }

            }
            catch (Exception ex)
            {
               
                Console.WriteLine($"si è verificato un errore : {ex.Message}");
            }

        }
        public void SaveKey(SingleTonConnectDB connect , string email, int key)
        {
            try
            {
                connectDB(connect.ConnectDb());
               
                SqlCommand sql = sqlConnection.CreateCommand();
                sql.CommandType = System.Data.CommandType.StoredProcedure; 
                sql.CommandText = "InsertCode";
                sql.Parameters.AddWithValue("@email", email);
                sql.Parameters.AddWithValue("@codice", key);
                sql.ExecuteNonQuery();
              
            }
            catch (Exception ex)
            {

                Console.WriteLine("errore nel metodo SAVEKEY  " + ex.Message);
                logger.WithProperty("ErrorCode", ex.HResult)
                        .WithProperty("ErrorClass", ex.TargetSite.DeclaringType.ToString())
                        .Error("{Message}", ex.Message);

            }
            finally { connect.Dispose(); }

        }

        public bool ChecKey(SingleTonConnectDB connection , int codice)
        {
          
            connectDB(connection.ConnectDb());
            SqlCommand sql = sqlConnection.CreateCommand();
            sql.CommandType = System.Data.CommandType.StoredProcedure;
            sql.CommandText = "[dbo].[Checkey]";
            sql.Parameters.AddWithValue("@key",codice);
            SqlDataReader sqlData = sql.ExecuteReader();
            using (sqlData)
            {
                if (sqlData.HasRows == true)
                {

                    return true;
                }
                else
                {
                    Console.WriteLine("il codice inserito non esiste");
                }

            }

            return false;
        }
        public  Task<bool> dropKey(SingleTonConnectDB connection , int codice)
        {
            try
            {
               
                connectDB(connection.ConnectDb());
                SqlCommand sql = sqlConnection.CreateCommand();
                sql.CommandType = System.Data.CommandType.StoredProcedure;
                sql.CommandText = "DeleteCode";
                sql.Parameters.AddWithValue("@code", codice);
                sql.ExecuteNonQuery();

                return Task.FromResult(true);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Errore nel metodo DropKey" + ex.Message);
                logger.WithProperty("ErrorCode", ex.HResult)
                        .WithProperty("ErrorClass", ex.TargetSite.DeclaringType.ToString())
                        .Error("{Message}", ex.Message);
                return Task.FromResult(false);
            }
          
        }
        public bool GenerateNewPassWithSaltHsh(SingleTonConnectDB connection , string password , string email)
        {
            try
            {
                RegexCh regex = new RegexCh();
                var NewPassHash = regex.EncryptSalt(password);
                connectDB(connection.ConnectDb());
                SqlCommand sql = sqlConnection.CreateCommand();
                sql.CommandType = System.Data.CommandType.StoredProcedure;
                sql.CommandText = "PassUpdate";
                sql.Parameters.AddWithValue("@key", NewPassHash.Key);
                sql.Parameters.AddWithValue("@value", NewPassHash.Value);
                sql.Parameters.AddWithValue("@email", email);
                sql.ExecuteNonQuery();
                connection.Dispose();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("errore nel metodo GenerateNePassHash");
                logger.WithProperty("ErrorCode", ex.HResult)
                        .WithProperty("ErrorClass", ex.TargetSite.DeclaringType.ToString())
                        .Error("{Message}", ex.Message);
                return false;
              
            }
          
        }
    }
}
