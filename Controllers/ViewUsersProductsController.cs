
using Betacomio_Project.ConnectDb;
using Betacomio_Project.LogModels;
using Betacomio_Project.NewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RegexCheck;

namespace Betacomio_Project.Controllers
{
   
    [Route("api/[controller]")]
    [ApiController]
    public class ViewUserProductsController : ControllerBase
    {
        private readonly BetacomioCyclesContext _context;
        private readonly RegexCh _regex;


        public ViewUserProductsController(BetacomioCyclesContext context,  RegexCh regex)
        {
            _context = context;
            _regex = regex;
        }

        
        [HttpGet("GetUserProducts")]
        public async Task<ActionResult<IEnumerable<ViewUserProduct>>> GetUserProducts()
        {
            if (_context.ViewUserProducts == null)
            {
                return NotFound();
            }
            return await _context.ViewUserProducts.Take(100).ToListAsync();
        }

        [HttpGet("GetUserProductsByLanguage")]
        public async Task<ActionResult<IEnumerable<ViewUserProduct>>> GetUserProductsByLanguage(MainSingleton connectao, int nationality)
        {

            try
            {
                if (_context.ViewUserProducts == null)
                {
                    return NotFound();
                }

                var productsByLanguage = await _regex.ProductsWithLanguage(connectao, nationality);
                return Ok(productsByLanguage);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
                // mettere Nlog
            }
        }


        [HttpGet("{name}")]
        public async Task<ActionResult<ViewUserProduct>> GetUserProductsByID(string name)
        {
            if (_context.ViewUserProducts == null)
            {
                return NotFound();
            }
            var userProduct = await _context.ViewUserProducts.Where(val => val.Name == name).FirstAsync();

            if (userProduct == null)
            {
                return NotFound();
            }

            return userProduct;
        }



        private bool UserProductExists(string name)
        {
            return (_context.ViewUserProducts?.Any(val => val.Name == name)).GetValueOrDefault();
        }
    }
}
