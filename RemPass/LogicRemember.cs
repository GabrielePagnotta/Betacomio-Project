using Betacomio_Project.ConnectDb;
using Microsoft.Data.SqlClient;
using RegexCheck;
using System.Data;
using System.Text.RegularExpressions;

namespace Betacomio_Project.RemPass
{
    public class LogicRemember
    {
        SqlConnection sqlConnection = new SqlConnection();
        private readonly SingleTonConnectDB _connect;
        public LogicRemember(SingleTonConnectDB connect)
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
                sql.CommandText = $"";
                sql.ExecuteNonQuery();
              
            }
            catch (Exception err)
            {

                Console.WriteLine("errore nel metodo SAVEKEY  " + err.Message);
                
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
            if (sqlData.HasRows == true)
            {
                
                return true;
            }
            else
            {
                Console.WriteLine("il codice inserito non esiste");
            }

            return false;
        }
        public bool dropKey(SingleTonConnectDB connection , int codice)
        {
            try
            {
                connectDB(connection.ConnectDb());
                SqlCommand sql = sqlConnection.CreateCommand();
                sql.CommandText = $"delete from dbo.RememberPass where ssKey = {codice} ";
                sql.ExecuteNonQuery();
                connection.Dispose();
                return true;
            }
            catch (Exception err)
            {
                Console.WriteLine("Errore nel metodo DropKey" + err.Message);
                return false;
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
                sql.CommandText = $"UPDATE [dbo].[UserCredentials] SET [Password_Hash] = {NewPassHash.Key} , [Password_Salt] = {NewPassHash.Value} where Email = {email}";
                sql.ExecuteNonQuery();
                connection.Dispose();
                return true;
            }
            catch (Exception err)
            {
                Console.WriteLine("errore nel metodo GenerateNePassHash");
                return false;
              
            }
          
        }
    }
}
