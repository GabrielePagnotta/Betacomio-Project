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
    public class OrderProxiesController : ControllerBase
    {
        private readonly AdminLogContext _context;

        public OrderProxiesController(AdminLogContext context)
        {
            _context = context;
        }

        // GET: api/OrderProxies
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderProxy>>> GetOrderProxies()
        {
          if (_context.OrderProxies == null)
          {
              return NotFound();
          }
            return await _context.OrderProxies.ToListAsync();
        }

        // GET: api/OrderProxies/5
        [HttpGet("{id}")]
        public async Task<ActionResult<OrderProxy>> GetOrderProxy(int id)
        {
          if (_context.OrderProxies == null)
          {
              return NotFound();
          }
            var orderProxy = await _context.OrderProxies.FindAsync(id);

            if (orderProxy == null)
            {
                return NotFound();
            }

            return orderProxy;
        }

        // PUT: api/OrderProxies/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOrderProxy(int id, OrderProxy orderProxy)
        {
            if (id != orderProxy.GenericId)
            {
                return BadRequest();
            }

            _context.Entry(orderProxy).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrderProxyExists(id))
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

        // POST: api/OrderProxies
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<OrderProxy>> PostOrderProxy(OrderProxy orderProxy)
        {
          if (_context.OrderProxies == null)
          {
              return Problem("Entity set 'AdminLogContext.OrderProxies'  is null.");
          }
            _context.OrderProxies.Add(orderProxy);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetOrderProxy", new { id = orderProxy.GenericId }, orderProxy);
        }

        // DELETE: api/OrderProxies/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrderProxy(int id)
        {
            if (_context.OrderProxies == null)
            {
                return NotFound();
            }
            var orderProxy = await _context.OrderProxies.FindAsync(id);
            if (orderProxy == null)
            {
                return NotFound();
            }

            _context.OrderProxies.Remove(orderProxy);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool OrderProxyExists(int id)
        {
            return (_context.OrderProxies?.Any(e => e.GenericId == id)).GetValueOrDefault();
        }
    }
}
