using Betacomio_Project.ConnectDb;

using Microsoft.AspNetCore.Mvc;
using RegexCheck;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Betacomio_Project.RemPass
{
  
    [Route("api/[controller]")]
    [ApiController]
    public class RememberPass : ControllerBase
    {
        private readonly SingleTonConnectDB _connect;
        
        private readonly RegexCh _Regex;
        public RememberPass(SingleTonConnectDB connnect , RegexCh reg)
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
        public void Post( Remember value)
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
                    int key = send.CreateTestMessage2( value.email);
                    LogicRemember logic = new LogicRemember(_connect);
                    logic.SaveKey(_connect , value.email, key);

                }
                

            }
            catch (Exception)
            {

                throw;
            }
           
        }

      
    }
}
