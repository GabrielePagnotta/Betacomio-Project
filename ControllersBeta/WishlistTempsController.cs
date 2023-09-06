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
using Betacomio_Project.LogModels;
using Microsoft.Data.SqlClient;

namespace Betacomio_Project.ControllersBeta
{
    [Route("api/[controller]")]
    [ApiController]
    public class WishlistTempsController : ControllerBase
    {
        private readonly AdminLogContext _context;
        SqlConnection sqlConnection = new SqlConnection();

        public WishlistTempsController(AdminLogContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<WishlistTemp>>> GetWishlistTemps()
        {
            if (_context.WishlistTemps == null)
            {
                return NotFound();
            }
            return await _context.WishlistTemps.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<WishlistTemp>> GetWishlist(int id)
        {
            if (_context.WishlistTemps == null)
            {
                return NotFound();
            }
            var wishlist = await _context.WishlistTemps.FindAsync(id);

            if (wishlist == null)
            {
                return NotFound();
            }

            return wishlist;
        }

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

        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<IActionResult> PostWishlist(WishlistTemp wishItem)
        {
            if (_context.WishlistTemps == null)
            {
                return Problem("Entity set 'AdminLog.WishlistTemp'  is null.");
            }

            _context.WishlistTemps.Add(wishItem);
            await _context.SaveChangesAsync();

            //Avvia stored procedure per passare dati a wishlist originale
            SqlCommand sql = sqlConnection.CreateCommand();
            sql.CommandType = System.Data.CommandType.StoredProcedure;
            sql.CommandText = "WishlistDataToMainDB"; //SP che inserisce dati da wishlist temporanea a quella originale
            sql.Parameters.AddWithValue("@userIdentifier", wishItem.UserId);
            sql.Parameters.AddWithValue("@prodIdentifier", wishItem.ProductId);

            return Ok($"Wishlist aggiunta con successo per utente: {wishItem.UserId}");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteWishlist(int id)
        {
            if (_context.WishlistTemps == null)
            {
                return NotFound();
            }
            var wishlist = await _context.WishlistTemps.FindAsync(id);
            if (wishlist == null)
            {
                return NotFound();
            }

            _context.WishlistTemps.Remove(wishlist);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool WishlistExists(int id)
        {
            return (_context.WishlistTemps?.Any(e => e.UserId == id)).GetValueOrDefault();
        }
    }
}

