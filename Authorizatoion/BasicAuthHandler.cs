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
using Microsoft.VisualBasic;
using Betacomio_Project.Controllers;
using RegexCheck;
namespace FirstMVC.Auth
{
    public class BasicAuthHandler : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        public RegexCh regexCh = new RegexCh();
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
                string[] credential = regexCh.convertInsertCredential(authorizationHeader);
                try
                {
  
                string[] passUSer  = regexCh.CheckUsernameAndPassword(credential[0]);
                regexCh.CheckLogin(credential[1], passUSer[0] , passUSer[1].ToString() );

                if (passUSer[0] == null)
                {
                       
                        return  Task.FromResult(AuthenticateResult.Fail( new Exception("User e/o password errati !!!")));
                }
                else
                {
                    Console.WriteLine("user corretta");
                    var authenticatedUser = new AuthUser("BasicAuthentication", true, credential[0]);

                    var claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity(authenticatedUser)); //viene creata una chiave per accedere

                    return Task.FromResult(AuthenticateResult.Success(new AuthenticationTicket(claimsPrincipal, Scheme.Name)));
                }

                   
                }
                catch (Exception err)
                {

                    Console.WriteLine("errore nel metodo Autorization" + err.Message);

                return Task.FromResult(AuthenticateResult.Fail(err));
                }
        
            


        }
    }
}
