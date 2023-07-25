using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Betacomio_Project.Models;

namespace Betacomio_Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserProductsViewsController : ControllerBase
    {
        private readonly AdventureWorksLt2019Context _context;

        SqlQueryViews user = new();

        public UserProductsViewsController(AdventureWorksLt2019Context context)
        {
            _context = context;
        }

        // GET: api/UserProductsViews
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserProductsView>>> GetUserProductsViews()
        {
          if (_context.UserProductsViews == null)
          {
              return NotFound();
          }
            return user.UserProductsV();
                   //await _context.UserProductsViews.ToListAsync();
        }

        // GET: api/UserProductsViews/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UserProductsView>> GetUserProductsView(string id)
        {
          if (_context.UserProductsViews == null)
          {
              return NotFound();
          }
            var userProductsView = await _context.UserProductsViews.FindAsync(id);

            if (userProductsView == null)
            {
                return NotFound();
            }

            return userProductsView;
        }

        // PUT: api/UserProductsViews/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUserProductsView(string id, UserProductsView userProductsView)
        {
            if (id != userProductsView.Name)
            {
                return BadRequest();
            }

            _context.Entry(userProductsView).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserProductsViewExists(id))
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

        // POST: api/UserProductsViews
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<UserProductsView>> PostUserProductsView(UserProductsView userProductsView)
        {
          if (_context.UserProductsViews == null)
          {
              return Problem("Entity set 'AdventureWorksLt2019Context.UserProductsViews'  is null.");
          }
            _context.UserProductsViews.Add(userProductsView);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (UserProductsViewExists(userProductsView.Name))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetUserProductsView", new { id = userProductsView.Name }, userProductsView);
        }

        // DELETE: api/UserProductsViews/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUserProductsView(string id)
        {
            if (_context.UserProductsViews == null)
            {
                return NotFound();
            }
            var userProductsView = await _context.UserProductsViews.FindAsync(id);
            if (userProductsView == null)
            {
                return NotFound();
            }

            _context.UserProductsViews.Remove(userProductsView);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UserProductsViewExists(string id)
        {
            return (_context.UserProductsViews?.Any(e => e.Name == id)).GetValueOrDefault();
        }
    }
}
