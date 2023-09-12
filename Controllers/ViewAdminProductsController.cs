
using Betacomio_Project.NewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Betacomio_Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ViewAdminProductsController : ControllerBase
    {
        private readonly BetacomioCyclesContext _context;


        public ViewAdminProductsController(BetacomioCyclesContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ViewAdminProduct>>> GetAdminProducts()
        {
            if (_context.ViewAdminProducts == null)
            {
                return NotFound();
            }
            return await _context.ViewAdminProducts.ToListAsync();
        }

        [HttpGet("{name}")]
        public async Task<ActionResult<ViewAdminProduct>> GetAdminProductsByID(string name)
        {
            if (_context.ViewAdminProducts == null)
            {
                return NotFound();
            }
            var adminProduct = await _context.ViewAdminProducts.Where(val => val.Name == name).FirstAsync();

            if (adminProduct == null)
            {
                return NotFound();
            }

            return adminProduct;
        }



        private bool AdminProductExists(int id)
        {
            return (_context.ViewAdminProducts?.Any(e => e.ProductId == id)).GetValueOrDefault();
        }
    }
}
