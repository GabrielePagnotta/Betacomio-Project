using Azure.Core;
using Azure;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using System.Text.Encodings.Web;
using System.Text.RegularExpressions;
using System.Text;
using System.Security.Cryptography;

using Microsoft.IdentityModel.Tokens;

namespace FirstMVC.Auth
{
    public class BasicAuthHandler : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        public BasicAuthHandler(
            IOptionsMonitor<AuthenticationSchemeOptions> options,
            ILoggerFactory logger,
            UrlEncoder encoder,
            ISystemClock clock)
            : base(options, logger, encoder, clock)
        {
        }

        protected override Task<AuthenticateResult> HandleAuthenticateAsync()
        {
           
                Response.Headers.Add("WWW-Authenticate", "Basic"); //middleware, porre basic authorization sulle actions che richiedono protezione

                if (!Request.Headers.ContainsKey("Authorization"))  //header della richiesta contiene chiavi di accesso
                {
                    return Task.FromResult(AuthenticateResult.Fail("Autorizzazione mancante")); //esci dal modulo
                }

                var authorizationHeader = Request.Headers["Authorization"].ToString();  //leggi chiave di accesso: Basic + chiave criptata
                string[] arr = authorizationHeader.Split(" ");
                string UsernamePas = Encoding.UTF8.GetString(Convert.FromBase64String(arr[1]));
                int separatoreIndex = UsernamePas.IndexOf(":");// indice di partenza
                string Username = UsernamePas.Substring(0, separatoreIndex);  //parto dalla posizione zero fino ad arrivare all'indice
                string passWord = UsernamePas.Substring(separatoreIndex + 1); //parto dall'indice +1
                Regex regex = new Regex("^[A-Z]{1,}[a-z\\d\\s]{7,}$");  // inserire lettera maiuscola ed almeno un numero


                try
                {
                 
                    if (regex.IsMatch(passWord))
                    {
                        Console.WriteLine("password corretta");
                    }
                    else
                    {
                        Console.WriteLine("password errata ");
                    }


                    if ((Username != "ciao") || (passWord != "ciao12P1"))
                    {
                       
                        return  Task.FromResult(AuthenticateResult.Fail( new Exception("User e/o password errati !!!")));
                    }

                    var authenticatedUser = new AuthUser("BasicAuthentication", true , "ciao");

                    var claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity(authenticatedUser)); //viene creata una chiave per accedere

                    return Task.FromResult(AuthenticateResult.Success(new AuthenticationTicket(claimsPrincipal, Scheme.Name)));
                }
                catch (Exception err)
                {

                    Console.WriteLine("errore nel metodo Autorization" + err.Message);
                }

          


        }
    }
}
