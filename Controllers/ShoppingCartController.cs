using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Betacomio_Project.NewModels;
using NLog;
using Betacomio_Project.LogModels;

namespace Betacomio_Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShoppingCartController : ControllerBase
    {
        private readonly BetacomioCyclesContext _context;
        private static Logger logger = LogManager.GetCurrentClassLogger();


        public ShoppingCartController(BetacomioCyclesContext context)
        {
            _context = context;
        }

        // GET: api/ShoppingCart
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ShoppingCart>>> GetShoppingCarts([FromQuery] int userid)
        {
          if (_context.ShoppingCarts == null)
          {
              return NotFound();
          }

            List<ShoppingCart> shoppingCart = null;

            try
            {
                shoppingCart = await _context.ShoppingCarts
                                     .Where(el => el.UserId == userid)
                                     .Include(el => el.Product).Include(el => el.User).ToListAsync();
            }
            catch (Exception ex)
            {

                logger.WithProperty("ErrorCode", ex.HResult)
                 .WithProperty("ErrorClass", ex.TargetSite.DeclaringType.ToString())
                 .Error("{Message}", ex.Message);

                return BadRequest("Errore durante lettura prodotti del carrello.");
            }             

            return shoppingCart;
        }

        // GET: api/ShoppingCart/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ShoppingCart>> GetShoppingCart(int id)
        {
          if (_context.ShoppingCarts == null)
          {
              return NotFound();
          }
            var shoppingCart = await _context.ShoppingCarts.FindAsync(id);

            if (shoppingCart == null)
            {
                return NotFound();
            }

            return shoppingCart;
        }

        // PUT: api/ShoppingCart/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutShoppingCart(int id, ShoppingCart shoppingCart)
        {
            if (id != shoppingCart.UserId)
            {
                return BadRequest();
            }

            _context.Entry(shoppingCart).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ShoppingCartExists(id))
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

        

        // POST: api/ShoppingCart
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ShoppingCart>> PostShoppingCart(ShoppingCart shoppingCart)
        {
          if (_context.ShoppingCarts == null)
          {
              return Problem("Entity set 'BetacomioCyclesContext.ShoppingCarts'  is null.");
          }
            _context.ShoppingCarts.Add(shoppingCart);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (ShoppingCartExists(shoppingCart.UserId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetShoppingCart", new { id = shoppingCart.UserId }, shoppingCart);
        }

        // DELETE: api/DeleteSingleInCart/5/12
        [HttpDelete("{id}/{ProductId}")]
        public async Task<IActionResult> DeleteShoppingCart(int id, int ProductId)
        {
            if (_context.ShoppingCarts == null)
            {
                return NotFound();
            }
            var shoppingCart = await _context.ShoppingCarts.FindAsync(id, ProductId);
            if (shoppingCart == null)
            {
                return NotFound();
            }

            _context.ShoppingCarts.Remove(shoppingCart);
            await _context.SaveChangesAsync();

            return NoContent();
        }



        private bool ShoppingCartExists(int id)
        {
            return (_context.ShoppingCarts?.Any(e => e.UserId == id)).GetValueOrDefault();
        }
    }
}
