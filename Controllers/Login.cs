using Betacomio_Project.Login;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Betacomio_Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Login : ControllerBase
    {


        //https://localhost:7284/api/Login // link post 
        // POST api/<Login>
        [HttpPost]
        [Authorize("BasicAuthentication")]
        public List<LoginSchem> Post()
        {
            var user = HttpContext.User;
            try
            {
                if (user.Identity.IsAuthenticated == true)
                {
                    Console.WriteLine($"dati utente autenticato {user.Identity.Name} , {user.Identity.AuthenticationType}  ");
                    Random random = new Random();
                    var token1 = random.Next();
                    List<LoginSchem> users = new List<LoginSchem>();
                    users.Add(new LoginSchem(user.Identity.Name, token1));
                  
                   
                    return users;
                }
                else
                {
                    throw new Exception("utente non auenticato prova a rifare la login ");
                }
            }
            catch (Exception err)
            {
                Console.WriteLine("errore in fase di autenticazione " + "  " +  err.Message + "  " + err.Data );
               
            }
            List<LoginSchem> empty = new List<LoginSchem>();
            return empty;
        }


    }
}
