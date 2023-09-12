using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using Betacomio_Project.ConnectDb;
using System.Text.RegularExpressions;
using RegexCheck;
using Microsoft.AspNetCore.Authorization;
using Betacomio_Project.NewModels;
using NLog;

namespace Betacomio_Project.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly BetacomioCyclesContext _context;
        private readonly SingleTonConnectDB _connession;
        private static Logger logger = LogManager.GetCurrentClassLogger();

        public UsersController(BetacomioCyclesContext context , SingleTonConnectDB connession)
        {
            _context = context;
            _connession = connession;
        }

        // GET: api/Users
        [Authorize("BasicAuthentication")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
          if (_context.Users == null)
          {
              return NotFound();
          }
            return await _context.Users.ToListAsync();
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
          if (_context.Users == null)
          {
              return NotFound();
          }

            User user = null;

            try
            {
                user = await _context.Users.FindAsync(id);
            }
            catch (Exception ex)
            {

                logger.WithProperty("ErrorCode", ex.HResult)
                .WithProperty("ErrorClass", ex.TargetSite.DeclaringType.ToString())
                .Error("{Message}", ex.Message);

                return BadRequest("Errore durante lettura utenti");
            }
            
            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        // PUT: api/Users/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(int id, User user)
        {
            if (id != user.UserId)
            {
                return BadRequest();
            }

            _context.Entry(user).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
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

        // POST: api/Users
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
       
        //[HttpPost]
        //public async Task<ActionResult<User>> PostUser(User user)
        //{
        //  if (_context.Users == null)
        //  {
        //      return Problem("Entity set 'UserRegistryContext.Users'  is null.");
        //  }
        //    RegexCh regex = new RegexCh();
        //    bool existUser = regex.Checkusername(_connession, user.Username, user.Email);
        //    if (existUser == true){ return BadRequest(404); }
          
        //    InsertUS insertUS = new InsertUS();
        //    insertUS.Usnew(user);
        //    _context.Users.Add(user);
        //    await _context.SaveChangesAsync();
        //    return Ok();
        //    //_context.Users.Add(user);
        //    //await _context.SaveChangesAsync();

        //    //return CreatedAtAction("GetUser", new { id = user.UserId }, user);
        //}

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            if (_context.Users == null)
            {
                return NotFound();
            }
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UserExists(int id)
        {
            return (_context.Users?.Any(e => e.UserId == id)).GetValueOrDefault();
        }
    }
}
