using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Betacomio_Project.NewModels;

namespace Betacomio_Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserRequestsController : ControllerBase
    {
        private readonly BetacomioCyclesContext _context;

        public UserRequestsController(BetacomioCyclesContext context)
        {
            _context = context;
        }

        // GET: api/UserRequests
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserRequest>>> GetUserRequests()
        {
          if (_context.UserRequests == null)
          {
              return NotFound();
          }
            return await _context.UserRequests.ToListAsync();
        }

        // GET: api/UserRequests/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UserRequest>> GetUserRequest(int id)
        {
          if (_context.UserRequests == null)
          {
              return NotFound();
          }
            var userRequest = await _context.UserRequests.FindAsync(id);

            if (userRequest == null)
            {
                return NotFound();
            }

            return userRequest;
        }

        // PUT: api/UserRequests/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUserRequest(int id, UserRequest userRequest)
        {
            if (id != userRequest.RequestId)
            {
                return BadRequest();
            }

            _context.Entry(userRequest).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserRequestExists(id))
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

        // POST: api/UserRequests
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<UserRequest>> PostUserRequest(UserRequest userRequest)
        {
          if (_context.UserRequests == null)
          {
              return Problem("Entity set 'BetacomioCyclesContext.UserRequests'  is null.");
          }
            _context.UserRequests.Add(userRequest);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUserRequest", new { id = userRequest.RequestId }, userRequest);
        }

        // DELETE: api/UserRequests/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUserRequest(int id)
        {
            if (_context.UserRequests == null)
            {
                return NotFound();
            }
            var userRequest = await _context.UserRequests.FindAsync(id);
            if (userRequest == null)
            {
                return NotFound();
            }

            _context.UserRequests.Remove(userRequest);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UserRequestExists(int id)
        {
            return (_context.UserRequests?.Any(e => e.RequestId == id)).GetValueOrDefault();
        }
    }
}
