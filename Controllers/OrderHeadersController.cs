using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Betacomio_Project.NewModels;
using NLog;

namespace Betacomio_Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderHeadersController : ControllerBase
    { 

        private readonly BetacomioCyclesContext _context;
        private static Logger logger = LogManager.GetCurrentClassLogger();


        public OrderHeadersController(BetacomioCyclesContext context)
        {
            _context = context;
        }

        // GET: api/OrderHeaders
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderHeader>>> GetOrderHeaders([FromQuery] int userid)
        {
          if (_context.OrderHeaders == null)
          {
              return NotFound();
          }
            List<OrderHeader> getOrders = null;
            try
            {
                getOrders = await _context.OrderHeaders.Where(el => el.CustomerId == userid)
                                            .Include(el => el.OrderDetails).ThenInclude(det => det.Product).ToListAsync();
            }
            catch (Exception ex)
            {
                logger.WithProperty("ErrorCode", ex.HResult)
                    .WithProperty("ErrorClass", ex.TargetSite.DeclaringType.ToString())
                    .Error("{Message}", ex.Message);

                return BadRequest("Errore durante la lettura degli ordini.");
            }

            // Restituisci getOrders se tutto è andato bene
            return getOrders;
        }

        // GET: api/OrderHeaders/5
        [HttpGet("{id}")]
        public async Task<ActionResult<OrderHeader>> GetOrderHeader(int id)
        {
          if (_context.OrderHeaders == null)
          {
              return NotFound();
          }
            var orderHeader = await _context.OrderHeaders.FindAsync(id);

            if (orderHeader == null)
            {
                return NotFound();
            }

            return orderHeader;
        }

        // PUT: api/OrderHeaders/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOrderHeader(int id, OrderHeader orderHeader)
        {
            if (id != orderHeader.OrderId)
            {
                return BadRequest();
            }

            _context.Entry(orderHeader).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrderHeaderExists(id))
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

        // POST: api/OrderHeaders
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<OrderHeader>> PostOrderHeader(OrderHeader orderHeader)
        {
          if (_context.OrderHeaders == null)
          {
              return Problem("Entity set 'BetacomioCyclesContext.OrderHeaders'  is null.");
          }
            _context.OrderHeaders.Add(orderHeader);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetOrderHeader", new { id = orderHeader.OrderId }, orderHeader);
        }

        // DELETE: api/OrderHeaders/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrderHeader(int id)
        {
            if (_context.OrderHeaders == null)
            {
                return NotFound();
            }
            var orderHeader = await _context.OrderHeaders.FindAsync(id);
            if (orderHeader == null)
            {
                return NotFound();
            }

            _context.OrderHeaders.Remove(orderHeader);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool OrderHeaderExists(int id)
        {
            return (_context.OrderHeaders?.Any(e => e.OrderId == id)).GetValueOrDefault();
        }
    }
}
