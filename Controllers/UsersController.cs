using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Betacomio_Project.Models;
using Betacomio_Project.ConnectDb;
using Microsoft.AspNetCore.Authorization;
using RegexCheck;

namespace Betacomio_Project.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly UserRegistryContext _context;
        private readonly SingleTonConnectDB _connession;
        public UsersController(UserRegistryContext context , SingleTonConnectDB connession)
        {
            _context = context;
            _connession = connession;
        }

        // GET: api/Users
       
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            try
            {
                if (_context.Users == null)
                {
                    return NotFound();
                }
                _connession.ConnectDb();
                return await _context.Users.ToListAsync();
            }
            catch (Exception ex)
            {

                Console.WriteLine("Errore nel metodo Get Users " + ex.Message);
                return BadRequest(ex.Message);
            }
        
         
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
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
        [HttpPost]
        public async Task<ActionResult<User>> PostUser(User user )
        {
          if (_context.Users == null)
          {
              return Problem("Entity set 'UserRegistryContext.Users'  is null.");
          }
            InsertUS insertUS = new InsertUS();
            RegexCh regexCh = new RegexCh();
            regexCh.checkUsername(_connession , user);
            insertUS.Usnew(user);
            
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return Ok();
            //_context.Users.Add(user);
            //await _context.SaveChangesAsync();

            //return CreatedAtAction("GetUser", new { id = user.UserId }, user);
        }

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
