using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Betacomio_Project.Models;
using Betacomio_Project.ConnectDb;

namespace Betacomio_Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminUsersRegistriesController : ControllerBase
    {
        private readonly AdventureWorksLt2019Context _context;
        private readonly LoginUser _login;
        private readonly SingleTonConnectDB _connession;
        SqlQueryViews registry = new();

        public AdminUsersRegistriesController(AdventureWorksLt2019Context context , LoginUser login , SingleTonConnectDB connession)
        {
            _context = context;
            _login = login;
            _connession = connession;
        }

        // GET: api/AdminUsersRegistries
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AdminUsersRegistry>>> GetAdminUsersRegistries()
        {
          if (_context.AdminUsersRegistries == null)
          {
              return NotFound();
          }
           
                return registry.UserRegistryV();
            
            return BadRequest(404);
            // await _context.AdminUsersRegistries.ToListAsync();
        }

        // GET: api/AdminUsersRegistries/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AdminUsersRegistry>> GetAdminUsersRegistry(int id)
        {
          if (_context.AdminUsersRegistries == null)
          {
              return NotFound();
          }
            var adminUsersRegistry = await _context.AdminUsersRegistries.FindAsync(id);

            if (adminUsersRegistry == null)
            {
                return NotFound();
            }

            return adminUsersRegistry;
        }

        // PUT: api/AdminUsersRegistries/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAdminUsersRegistry(int id, AdminUsersRegistry adminUsersRegistry)
        {
            if (id != adminUsersRegistry.CustomerId)
            {
                return BadRequest();
            }

            _context.Entry(adminUsersRegistry).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AdminUsersRegistryExists(id))
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

        // POST: api/AdminUsersRegistries
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<AdminUsersRegistry>> PostAdminUsersRegistry(AdminUsersRegistry adminUsersRegistry)
        {
          if (_context.AdminUsersRegistries == null)
          {
              return Problem("Entity set 'AdventureWorksLt2019Context.AdminUsersRegistries'  is null.");
          }
            _context.AdminUsersRegistries.Add(adminUsersRegistry);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAdminUsersRegistry", new { id = adminUsersRegistry.CustomerId }, adminUsersRegistry);
        }

        // DELETE: api/AdminUsersRegistries/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAdminUsersRegistry(int id)
        {
            if (_context.AdminUsersRegistries == null)
            {
                return NotFound();
            }
            var adminUsersRegistry = await _context.AdminUsersRegistries.FindAsync(id);
            if (adminUsersRegistry == null)
            {
                return NotFound();
            }

            _context.AdminUsersRegistries.Remove(adminUsersRegistry);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AdminUsersRegistryExists(int id)
        {
            return (_context.AdminUsersRegistries?.Any(e => e.CustomerId == id)).GetValueOrDefault();
        }
    }
}
