using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Betacomio_Project.LogModels;
using Betacomio_Project.ConnectDb;
using RegexCheck;
using Microsoft.AspNetCore.Authorization;
using Betacomio_Project.BusinessLogic;
using NLog;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Betacomio_Project.ControllersBeta
{

    [Route("api/[controller]")]
    [ApiController]
    public class UserCredentialsController : ControllerBase
    {
        private readonly AdminLogContext _context;
        private readonly SingleTonConnectDB _connession;
        private readonly RegexCh _regex;
        private static Logger logger = LogManager.GetCurrentClassLogger();


        public UserCredentialsController(AdminLogContext context, SingleTonConnectDB connession, RegexCh regex)
        {
            _context = context;
            _connession = connession;
            _regex = regex;
        }

        // GET: api/UserCredentials
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserCredential>>> GetUserCredentials()
        {
            try
            {
                if (_context.UserCredentials == null)
                {
                    return NotFound();
                }
                return await _context.UserCredentials.ToListAsync();
            }
            catch (Exception ex)
            {
                logger.WithProperty("ErrorCode", ex.HResult)
                         .WithProperty("ErrorClass", ex.TargetSite.DeclaringType.ToString())
                         .Error("{Message}", ex.Message);
                return BadRequest("errore nella lettura degli utenti-log");
            }
          
        }

        // GET: api/UserCredentials/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UserCredential>> GetUserCredential(int id)
        {

            if (_context.UserCredentials == null)
            {
                return NotFound();
            }

            UserCredential userCredential;

            try
            {
               userCredential = await _context.UserCredentials.FindAsync(id);
            }
            catch (Exception ex)
            {
                logger.WithProperty("ErrorCode", ex.HResult)
                                        .WithProperty("ErrorClass", ex.TargetSite.DeclaringType.ToString())
                                        .Error("{Message}", ex.Message);
                return BadRequest("errore nella lettura degli utenti-log 2");
            }
        

            if (userCredential == null)
            {
                return NotFound();
            }

            return userCredential;
        }

        // PUT: api/UserCredentials/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUserCredential(int id, UserCredential userCredential)
        {
            if (id != userCredential.UserId)
            {
                return BadRequest();
            }

            _context.Entry(userCredential).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserCredentialExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/UserCredentials
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<UserCredential>> PostUserCredential(SingleTonConnectDB connection, UserCredential userCredential)
        {
            var responsedata = new { userCredential.Email, userCredential.PasswordHash };
            
            
            if (_context.UserCredentials == null)
            {
                return Problem("Entity set 'UserRegistryContext.Users'  is null.");
            }

            bool existUser = _regex.Checkusername(_connession, userCredential.Username, userCredential.Email);
            if (existUser == true) { return BadRequest(404); }

            InsertUS insertUS = new InsertUS();
            bool OkUsers =  insertUS.Usnew(userCredential);
            _context.UserCredentials.Add(userCredential);
             await _context.SaveChangesAsync();

            if(_context.SaveChangesAsync().IsCompletedSuccessfully == true)
            {
                //Attraverso SP passo utenti da DB isolato a quello originale solo se record è stato salvato sul DB isolato
                _regex.PassUserCredentials(connection, userCredential.UserId);

            }

            Console.WriteLine(userCredential.PasswordHash);

            return Ok(responsedata);
        }

        // DELETE: api/UserCredentials/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUserCredential(int id)
        {
            if (_context.UserCredentials == null)
            {
                return NotFound();
            }
            var userCredential = await _context.UserCredentials.FindAsync(id);
            if (userCredential == null)
            {
                return NotFound();
            }

            _context.UserCredentials.Remove(userCredential);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UserCredentialExists(int id)
        {
            return (_context.UserCredentials?.Any(e => e.UserId == id)).GetValueOrDefault();
        }
    }
}
