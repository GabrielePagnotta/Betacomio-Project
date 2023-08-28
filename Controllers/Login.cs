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
        public void Post()
        {
            var user = HttpContext.User;
            try
            {
                if (user.Identity.IsAuthenticated == true)
                {
                    Console.WriteLine($"dati utente autenticato {user.Identity.Name} , {user.Identity.AuthenticationType}  ");
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
          
        }


    }
}
