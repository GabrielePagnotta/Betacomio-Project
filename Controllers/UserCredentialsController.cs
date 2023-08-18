using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Betacomio_Project.LogModels;

namespace Betacomio_Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserCredentialsController : ControllerBase
    {
        private readonly AdminLogContext _context;

        public UserCredentialsController(AdminLogContext context)
        {
            _context = context;
        }

        // GET: api/UserCredentials
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserCredential>>> GetUserCredentials()
        {
          if (_context.UserCredentials == null)
          {
              return NotFound();
          }
            return await _context.UserCredentials.ToListAsync();
        }

        // GET: api/UserCredentials/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UserCredential>> GetUserCredential(int id)
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
        public async Task<ActionResult<UserCredential>> PostUserCredential(UserCredential userCredential)
        {
          if (_context.UserCredentials == null)
          {
              return Problem("Entity set 'AdminLogContext.UserCredentials'  is null.");
          }
            _context.UserCredentials.Add(userCredential);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUserCredential", new { id = userCredential.UserId }, userCredential);
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
