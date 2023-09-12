using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Betacomio_Project.NewModels;
using System.Text.Json.Nodes;
using Newtonsoft.Json;
using NLog;

namespace Betacomio_Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WishlistController : ControllerBase
    {
        private readonly BetacomioCyclesContext _context;
        private static Logger logger = LogManager.GetCurrentClassLogger();

        public WishlistController(BetacomioCyclesContext context)
        {
            _context = context;
        }

        // GET: api/Wishlist
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Wishlist>>> GetWishlists([FromQuery] int userid)
        {
            try
            {
                if (_context.Wishlists == null)
                {
                    return NotFound();
                }
                return await _context.Wishlists
                            .Where(el => el.UserId == userid)
                            .Include(el => el.Product).Include(el => el.User).ToListAsync();
            }

            catch (Exception ex)
            {
                logger.WithProperty("ErrorCode", ex.HResult)
                   .WithProperty("ErrorClass", ex.TargetSite.DeclaringType.ToString())
                   .Error("{Message}", ex.Message);

                return BadRequest("Errore durante lettura dati wishlist");

            }

        }

        // GET: api/Wishlist/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Wishlist>> GetWishlist(int id)
        {
          if (_context.Wishlists == null)
          {
              return NotFound();
          }
            var wishlist = await _context.Wishlists.FindAsync(id);

            if (wishlist == null)
            {
                return NotFound();
            }

            return wishlist;
        }

        // PUT: api/Wishlist/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutWishlist(int id, Wishlist wishlist)
        {
            if (id != wishlist.UserId)
            {
                return BadRequest();
            }

            _context.Entry(wishlist).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!WishlistExists(id))
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

        // POST: api/Wishlist
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<IActionResult> PostWishlist( Wishlist wishItem)
        {

            //Recupero dati utente attraverso ID
            var userData = await _context.Users.FirstAsync(el => el.UserId == wishItem.UserId);

            //Recupero dati prodotto attraverso ID
            var productData = await _context.Products.FirstAsync(el => el.ProductId == wishItem.ProductId);

            if(userData == null)
            {
                return BadRequest("Utente non valido");
            }
            else if(productData == null)
            {
                return BadRequest("Prodotto non trovato");
            }

            try
            {
                var wishlist = new Wishlist
                {
                    UserId = wishItem.UserId,
                    ProductId = wishItem.ProductId,
                    AddedDate = DateTime.Now,
                    Rowguid = new Guid(),
                    User = userData,
                    Product = productData

                };

                _context.Wishlists.Add(wishlist);
                Console.WriteLine(wishlist);
                await _context.SaveChangesAsync();
                return Ok("Prodotto aggiunto con successo a Wishlist");
            }

            catch (Exception ex)
            {

                return StatusCode(500, $"Si è verificato un errore: {ex.Message}");
            }

        }

        // DELETE: api/Wishlist/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteWishlist(int id)
        {
            if (_context.Wishlists == null)
            {
                return NotFound();
            }
            var wishlist = await _context.Wishlists.FindAsync(id);
            if (wishlist == null)
            {
                return NotFound();
            }

            _context.Wishlists.Remove(wishlist);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool WishlistExists(int id)
        {
            return (_context.Wishlists?.Any(e => e.UserId == id)).GetValueOrDefault();
        }
    }
}
