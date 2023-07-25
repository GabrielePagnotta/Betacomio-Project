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
    public class AdminProductsViewsController : ControllerBase
    {
        private readonly AdventureWorksLt2019Context _context;

        SqlQuery admin = new();

        public AdminProductsViewsController(AdventureWorksLt2019Context context)
        {
            _context = context;
        }

        // GET: api/AdminProductsViews
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AdminProductsView>>> GetAdminProductsViews()
        {
          if (_context.AdminProductsViews == null)
          {
              return NotFound();
          }
            return admin.AdminProductsV();
                /*await _context.AdminProductsViews.ToListAsync();*/
        }

        // GET: api/AdminProductsViews/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AdminProductsView>> GetAdminProductsView(int id)
        {
          if (_context.AdminProductsViews == null)
          {
              return NotFound();
          }
            var adminProductsView = await _context.AdminProductsViews.FindAsync(id);

            if (adminProductsView == null)
            {
                return NotFound();
            }

            return adminProductsView;
        }

        // PUT: api/AdminProductsViews/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAdminProductsView(int id, AdminProductsView adminProductsView)
        {
            if (id != adminProductsView.ProductId)
            {
                return BadRequest();
            }

            _context.Entry(adminProductsView).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AdminProductsViewExists(id))
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

        // POST: api/AdminProductsViews
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<AdminProductsView>> PostAdminProductsView(AdminProductsView adminProductsView)
        {
          if (_context.AdminProductsViews == null)
          {
              return Problem("Entity set 'AdventureWorksLt2019Context.AdminProductsViews'  is null.");
          }
            _context.AdminProductsViews.Add(adminProductsView);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAdminProductsView", new { id = adminProductsView.ProductId }, adminProductsView);
        }

        // DELETE: api/AdminProductsViews/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAdminProductsView(int id)
        {
            if (_context.AdminProductsViews == null)
            {
                return NotFound();
            }
            var adminProductsView = await _context.AdminProductsViews.FindAsync(id);
            if (adminProductsView == null)
            {
                return NotFound();
            }

            _context.AdminProductsViews.Remove(adminProductsView);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AdminProductsViewExists(int id)
        {
            return (_context.AdminProductsViews?.Any(e => e.ProductId == id)).GetValueOrDefault();
        }
    }
}
