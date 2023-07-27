using NLog;
using System.Data.SqlClient;

namespace Betacomio_Project.Controllers
{
    public class Classprova
    {

        private static readonly Logger logger = LogManager.GetCurrentClassLogger();
        private static readonly Logger log = LogManager.GetLogger("*");
        public  Classprova(Logger ciao = null , Logger piove = null) {

            ciao = logger;
            piove = log;

        }
        public void MyMethod()
        {
            // Esempio di log di un messaggio
            logger.Info("Questo è un messaggio di log.");
            log.Info("ciao a tutti ");
            // Esempio di log di un'eccezione
            try
            {
                // Codice che può generare un'eccezione
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Errore durante l'esecuzione di MyMethod.");
            }
        }


    }
}
