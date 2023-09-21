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
using Betacomio_Project.ConnectDb;
using RegexCheck;
using NLog;

namespace Betacomio_Project.ControllersBeta
{
    [Route("api/[controller]")]
    [ApiController]
    public class WishlistTempsController : ControllerBase
    {
        private readonly AdminLogContext _context;
        private readonly SingleTonConnectDB _db;
        private readonly RegexCh _regex;
        private static Logger logger = LogManager.GetCurrentClassLogger();

        SqlConnection sqlConnection = new SqlConnection();

        public WishlistTempsController(AdminLogContext context, SingleTonConnectDB db, RegexCh regex)
        {
            _context = context;
            _regex = regex;
            _db = db;
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
                if (!WishlistTempExists(id))
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
        public async Task<IActionResult> PostWishlist(SingleTonConnectDB connection, WishlistTemp wishItem)
        {
            if (_context.WishlistTemps == null)
            {
                return Problem("Entity set 'AdminLog.WishlistTemp'  is null.");
            }

            _context.WishlistTemps.Add(wishItem);
            try
            {
                await _context.SaveChangesAsync();
                _regex.PassWishlistData(connection, wishItem.ProductId, wishItem.UserId);


            }
            catch (DbUpdateException ex)
            {
                if (WishlistTempExists(wishItem.UserId))
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

        private bool WishlistTempExists(int id)
        {
            return (_context.WishlistTemps?.Any(e => e.UserId == id)).GetValueOrDefault();
        }
    }
}

