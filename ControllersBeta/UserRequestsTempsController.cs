using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Betacomio_Project.LogModels;

namespace Betacomio_Project.ControllersBeta
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserRequestsTempsController : ControllerBase
    {
        private readonly AdminLogContext _context;

        public UserRequestsTempsController(AdminLogContext context)
        {
            _context = context;
        }

        // GET: api/UserRequestsTemps
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserRequestsTemp>>> GetUserRequestsTemps()
        {
          if (_context.UserRequestsTemps == null)
          {
              return NotFound();
          }
            return await _context.UserRequestsTemps.ToListAsync();
        }

        // GET: api/UserRequestsTemps/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UserRequestsTemp>> GetUserRequestsTemp(int id)
        {
          if (_context.UserRequestsTemps == null)
          {
              return NotFound();
          }
            var userRequestsTemp = await _context.UserRequestsTemps.FindAsync(id);

            if (userRequestsTemp == null)
            {
                return NotFound();
            }

            return userRequestsTemp;
        }

        // PUT: api/UserRequestsTemps/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUserRequestsTemp(int id, UserRequestsTemp userRequestsTemp)
        {
            if (id != userRequestsTemp.RequestId)
            {
                return BadRequest();
            }

            _context.Entry(userRequestsTemp).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserRequestsTempExists(id))
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

        // POST: api/UserRequestsTemps
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<UserRequestsTemp>> PostUserRequestsTemp(UserRequestsTemp userRequestsTemp)
        {
          if (_context.UserRequestsTemps == null)
          {
              return Problem("Entity set 'AdminLogContext.UserRequestsTemps'  is null.");
          }
            _context.UserRequestsTemps.Add(userRequestsTemp);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (UserRequestsTempExists(userRequestsTemp.RequestId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetUserRequestsTemp", new { id = userRequestsTemp.RequestId }, userRequestsTemp);
        }

        // DELETE: api/UserRequestsTemps/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUserRequestsTemp(int id)
        {
            if (_context.UserRequestsTemps == null)
            {
                return NotFound();
            }
            var userRequestsTemp = await _context.UserRequestsTemps.FindAsync(id);
            if (userRequestsTemp == null)
            {
                return NotFound();
            }

            _context.UserRequestsTemps.Remove(userRequestsTemp);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UserRequestsTempExists(int id)
        {
            return (_context.UserRequestsTemps?.Any(e => e.RequestId == id)).GetValueOrDefault();
        }
    }
}
