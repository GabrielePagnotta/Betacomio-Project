
using Betacomio_Project.NewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NLog;

namespace Betacomio_Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ViewAdminProductsController : ControllerBase
    {
        private readonly BetacomioCyclesContext _context;
        private static Logger logger = LogManager.GetCurrentClassLogger();



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

            List<ViewAdminProduct> helo;

            try
            {
                helo = await _context.ViewAdminProducts.ToListAsync();
            }
            catch (Exception ex)
            {
                logger.WithProperty("ErrorCode", ex.HResult)
                .WithProperty("ErrorClass", ex.TargetSite.DeclaringType.ToString())
                .Error("{Message}", ex.Message);

                return BadRequest("Errore durante lettura dati view AdminProducts");
            }

            return helo;
        }

        [HttpGet("{name}")]
        public async Task<ActionResult<ViewAdminProduct>> GetAdminProductsByID(string name)
        {
            if (_context.ViewAdminProducts == null)
            {
                return NotFound();
            }

            ViewAdminProduct adminProduct;
            try
            {
                adminProduct = await _context.ViewAdminProducts.Where(val => val.Name == name).FirstAsync();
            }
            catch (Exception ex)
            {

                logger.WithProperty("ErrorCode", ex.HResult)
                .WithProperty("ErrorClass", ex.TargetSite.DeclaringType.ToString())
                .Error("{Message}", ex.Message);

                return BadRequest("Errore durante lettura dati view AdminProducts 2");
            }
        

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
