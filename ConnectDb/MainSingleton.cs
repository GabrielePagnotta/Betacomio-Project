using System;
using Microsoft.Data.SqlClient;
using NLog;

namespace Betacomio_Project.ConnectDb
{
    public class MainSingleton : IDisposable  //interfaccia che rilascia risorse impiegate
    {
        private SqlConnection mainConnection = new();
        private static  MainSingleton instance;
        private string connectionString; 
        private static readonly object _lock = new object();
        private static Logger logger = LogManager.GetCurrentClassLogger();


        private MainSingleton() { }
        public MainSingleton(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public static MainSingleton GetInstance()
        {
            if (instance == null)
            {
                lock (_lock) // thread safety: istruzione lock garantisce accesso esclusivo alla risorsa (solo un thread per volta può accedere alla connessione)
                {
                    if (instance == null)
                    {
                        instance = new MainSingleton();
                    }
                }
            }
            return instance;
        }

        public string ConnectToMainDB()
        {
            
            try
            {
                if (mainConnection.State == System.Data.ConnectionState.Closed)
                {
                    mainConnection.ConnectionString = connectionString;
                    mainConnection.Open();
                    return mainConnection.ConnectionString;

                }

            }
            catch (Exception ex)
            {
                Console.WriteLine($"si è verificato un errore : {ex.Message}");

                logger.WithProperty("ErrorCode", ex.HResult)
                  .WithProperty("ErrorClass", ex.TargetSite.DeclaringType.ToString())  
                  .Error("{Message}", ex.Message);

            }

            return mainConnection.ConnectionString;
        }


        public void Dispose()  //rilascia risorse impiegate per non accumulare garbage in cache
        {
            mainConnection.Dispose();
        }
    }
}
