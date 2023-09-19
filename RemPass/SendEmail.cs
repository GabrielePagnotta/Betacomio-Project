using System.Net.Mail;
using System.Net;
using Betacomio_Project.ConnectDb;
using NLog;

namespace Betacomio_Project.RemPass
{
    public class SendEmail
    {

        private static Logger logger = LogManager.GetCurrentClassLogger();


        public int CreateTestMessage2( string email)
        {
            var numRand = new Random().Next(); //creo un codice random 
            var url = "http://localhost:4200/confirmpassword"; //url link paginna 
            string body = $"<p>Clicca <a href=\"{url}\">qui</a> per visitare il sito. \n inserisci questo codice per la reimpostazione della password {numRand}</p>"; //corpo del messaggio
            try
            {
                Console.WriteLine(numRand);
                var client = new SmtpClient("sandbox.smtp.mailtrap.io", 2525) //richiamo il mio client ed inserisco SMTP e la porta 
                {
                    Credentials = new NetworkCredential("660652b60f3638", "d796f412450a6c"), // passo le mie credenziali 
                    EnableSsl = true // va sempre a true 
                };
                var mailmessage = new MailMessage // costruisco dati della mail da inviare
                {
                    From = new MailAddress("from@example.com"), //email 
                    Subject = "to@example.com",
                    IsBodyHtml = true,
                    Body = body
                };
                mailmessage.To.Add(email);
                client.Send(mailmessage); // inserisco il corpo del messaggio 
               
                Console.WriteLine("Sent");
                return numRand;
            }
            catch (Exception ex)
            {

                Console.WriteLine("errore " + ex.Message);
                logger.WithProperty("ErrorCode", ex.HResult)
                       .WithProperty("ErrorClass", ex.TargetSite.DeclaringType.ToString())
                       .Error("{Message}", ex.Message);
                return 0;
            }



        }

    }
}
