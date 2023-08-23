using Microsoft.Data.SqlClient;

namespace Betacomio_Project.ConnectDb
{
    public class SingleTonConnectDB
    {
        SqlConnection conn = new SqlConnection();
        private static SingleTonConnectDB _instance;
        private string? strinConn;

        private SingleTonConnectDB() { }

        public SingleTonConnectDB(string? strinConn)
        {
            this.strinConn = strinConn;
        }

        public static SingleTonConnectDB Instance
        {
            get
            {
                // Se l'istanza non è ancora stata creata, la crea
                if (_instance == null)
                {
                    _instance = new SingleTonConnectDB();
                }
                return _instance;
            }
        }
        public string ConnectDb()
        {
            bool Isok = false;
            try
            {
                if (conn.State == System.Data.ConnectionState.Closed)
                {
                    conn.ConnectionString = strinConn;
                    conn.Open();
                    return conn.ConnectionString;

                }

            }
            catch (Exception ex)
            {

                Console.WriteLine($"si è verificato un errore : {ex.Message}");

            }
            return null;
        }
    }
}
