using Betacomio_Project.ConnectDb;
using Microsoft.Data.SqlClient;
using RegexCheck;
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
             

                SqlCommand sql = sqlConnection.CreateCommand();
                sql.CommandText = $"INSERT INTO [dbo].[RememberPass]\r\n  ([email]\r\n ,[ssKey])\r\n VALUES\r\n  ({email}\r\n ,{key}) ";
                sql.ExecuteNonQuery();
            }
            catch (Exception err)
            {

                Console.WriteLine("errore nel metodo SAVEKEY  " + err.Message);
            }
            
        }

        public void ChecKey()
        {

        }
        public void dropKey()
        {

        }
        public void GenerateNewPassWithSaltHsh()
        {

        }
    }
}
