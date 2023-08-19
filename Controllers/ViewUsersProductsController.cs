
using Betacomio_Project.NewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Betacomio_Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ViewUserProductsController : ControllerBase
    {
        private readonly BetacomioCyclesContext _context;


        public ViewUserProductsController(BetacomioCyclesContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ViewUserProduct>>> GetUserProducts()
        {
            if (_context.ViewUserProducts == null)
            {
                return NotFound();
            }
            return await _context.ViewUserProducts.ToListAsync();
        }

        [HttpGet("{name}")]
        public async Task<ActionResult<List<ViewUserProduct>>> GetUserProductsByID(string name)
        {
            if (_context.ViewUserProducts == null)
            {
                return NotFound();
            }
            var userProduct = await _context.ViewUserProducts.Where(val => val.Name.ToLower().Contains(name)).ToListAsync();

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
