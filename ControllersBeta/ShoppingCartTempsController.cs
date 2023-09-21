using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Betacomio_Project.LogModels;
using RegexCheck;
using Betacomio_Project.ConnectDb;
using NLog;

namespace Betacomio_Project.ControllersBeta
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShoppingCartTempsController : ControllerBase
    {
        private readonly AdminLogContext _context;
        private readonly RegexCh _regex;
        private static Logger logger = LogManager.GetCurrentClassLogger();


        public ShoppingCartTempsController(AdminLogContext context, RegexCh regex)
        {
            _context = context;
            _regex = regex;
        }

        // GET: api/ShoppingCartTemps
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ShoppingCartTemp>>> GetShoppingCartTemps()
        {
          if (_context.ShoppingCartTemps == null)
          {
              return NotFound();
          }
            return await _context.ShoppingCartTemps.ToListAsync();
        }

        // GET: api/ShoppingCartTemps/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ShoppingCartTemp>> GetShoppingCartTemp(int id)
        {
          if (_context.ShoppingCartTemps == null)
          {
              return NotFound();
          }
            var shoppingCartTemp = await _context.ShoppingCartTemps.FindAsync(id);

            if (shoppingCartTemp == null)
            {
                return NotFound();
            }

            return shoppingCartTemp;
        }

        // PUT: api/ShoppingCartTemps/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutShoppingCartTemp(int id, ShoppingCartTemp shoppingCartTemp)
        {
            if (id != shoppingCartTemp.UserId)
            {
                return BadRequest();
            }

            _context.Entry(shoppingCartTemp).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ShoppingCartTempExists(id))
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

        // POST: api/ShoppingCartTemps
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ShoppingCartTemp>> PostShoppingCartTemp(SingleTonConnectDB connection, ShoppingCartTemp shoppingCartItem)
        {
          if (_context.ShoppingCartTemps == null)
          {
              return Problem("Entity set 'AdminLogContext.ShoppingCartTemps'  is null.");
          }


            _context.ShoppingCartTemps.Add(shoppingCartItem);
            try
            {
                await _context.SaveChangesAsync();
                _regex.PassShoppingCartData(connection, shoppingCartItem.ProductId, shoppingCartItem.UserId);
                
            }
            catch (DbUpdateException ex)
            {
                if (ShoppingCartTempExists(shoppingCartItem.UserId))
                {
                    logger.WithProperty("ErrorCode", ex.HResult)
                           .WithProperty("ErrorClass", ex.TargetSite.DeclaringType.ToString())
                           .Error("{Message}", ex.Message);
                    return Conflict();
                }
                else
                {
                    logger.WithProperty("ErrorCode", ex.HResult)
                            .WithProperty("ErrorClass", ex.TargetSite.DeclaringType.ToString())
                            .Error("{Message}", ex.Message);
                }
            }

            return Ok();
        }

        // DELETE: api/ShoppingCartTemps/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteShoppingCartTemp(int id)
        {
            if (_context.ShoppingCartTemps == null)
            {
                return NotFound();
            }
            var shoppingCartTemp = await _context.ShoppingCartTemps.FindAsync(id);
            if (shoppingCartTemp == null)
            {
                return NotFound();
            }

            _context.ShoppingCartTemps.Remove(shoppingCartTemp);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ShoppingCartTempExists(int id)
        {
            return (_context.ShoppingCartTemps?.Any(e => e.UserId == id)).GetValueOrDefault();
        }
    }
}
