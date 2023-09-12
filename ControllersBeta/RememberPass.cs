using Betacomio_Project.ConnectDb;
using Betacomio_Project.RemPass;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.CodeAnalysis.Elfie.Model.Strings;
using NLog;
using RegexCheck;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Betacomio_Project.ControllersBeta
{

    [Route("api/[controller]")]
    [ApiController]
    public class RememberPass : ControllerBase
    {
        private readonly SingleTonConnectDB _connect;
        private static Logger logger = LogManager.GetCurrentClassLogger();
        private readonly RegexCh _Regex;

        public RememberPass(SingleTonConnectDB connnect, RegexCh reg)
        {
            _connect = connnect;
            _Regex = reg;
        }
        // GET: api/<RememberPass>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<RememberPass>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<RememberPass>
        [HttpPost]
        public void Post(Remember value)
        {
            try
            {
                if (value == null)
                {
                    BadRequest(400);

                }

                if (_Regex.ExistUser(_connect, value.email) == true)
                {
                    SendEmail send = new SendEmail();
                    int key = send.CreateTestMessage2(value.email);
                    LogicRemember logic = new LogicRemember(_connect);
                    logic.SaveKey(_connect, value.email, key);

                }


            }
            catch (Exception ex)
            {

                logger.WithProperty("ErrorCode", ex.HResult)
                   .WithProperty("ErrorClass", ex.TargetSite.DeclaringType.ToString())
                   .Error("{Message}", ex.Message);
            }

        }

        [HttpPost]
        [Route("key/[controller]")]
        public async Task<IActionResult> Post2(CodiceRemember value) //IACTIONRESULT si aspetta una risposta ad una chiamata -- 
        {
            string resp = "operazione effettuata con successo";
            try
            {
                if (value.codice == null && value.password == null)
                {

                    return BadRequest();
                }
                
                LogicRemember logic = new LogicRemember(_connect);
                bool CheckCode =  logic.ChecKey(_connect , value.codice);
                if (CheckCode == true){ Console.WriteLine("errore nel metodo Checkey"); }
                
                 bool GeneratPass = logic.GenerateNewPassWithSaltHsh(_connect , value.password, value.email);
                  var removeCodeDb =  await logic.dropKey(_connect, value.codice);
                
                    _connect.Dispose();
                if (CheckCode == true && removeCodeDb == true && GeneratPass == true)
                {

                    return Ok();
                }
                else
                {
                    return BadRequest();
                }

            }
            catch (Exception ex)
            {

                logger.WithProperty("ErrorCode", ex.HResult)
                   .WithProperty("ErrorClass", ex.TargetSite.DeclaringType.ToString())
                   .Error("{Message}", ex.Message);
                return BadRequest();
            }

        }


    }
}
