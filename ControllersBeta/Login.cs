using Betacomio_Project.ConnectDb;
using Betacomio_Project.Login;
using Betacomio_Project.LogModels;
using Betacomio_Project.NewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using Newtonsoft.Json;
using NLog;
using RegexCheck;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Betacomio_Project.ControllersBeta
{
    [Route("api/[controller]")]
    [ApiController]
    public class Login : ControllerBase
    {
       
        private readonly SingleTonConnectDB _connectDB;
        private readonly AdminLogContext logContext;
        private static Logger logger = LogManager.GetCurrentClassLogger();

        public Login(SingleTonConnectDB connectDB, AdminLogContext context)
        {
            _connectDB = connectDB;
            logContext = context;
        }

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
                    RegexCh UserData = new RegexCh();
                    var datUsers = UserData.myProfileLogin(_connectDB, user.Identity.Name);

                    users.Add(new LoginSchem(user.Identity.Name, token1, datUsers[0].ToString(), datUsers[1], datUsers[2], datUsers[3], datUsers[4], datUsers[6], datUsers[7]));




                    return users;
                }
                else
                {
                    throw new Exception("utente non auenticato, prova a rifare la login ");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("errore in fase di autenticazione " + "  " + ex.Message + "  " + ex.Data);
                
                logger.WithProperty("ErrorCode", ex.HResult)
                   .WithProperty("ErrorClass", ex.TargetSite.DeclaringType.ToString())
                   .Error("{Message}", ex.Message);

            }

            List<LoginSchem> empty = new List<LoginSchem>();
            return empty;
        }


    }
}
