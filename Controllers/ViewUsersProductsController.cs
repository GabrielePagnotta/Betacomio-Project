
using Betacomio_Project.ConnectDb;
using Betacomio_Project.LogModels;
using Betacomio_Project.NewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using NLog;
using RegexCheck;

namespace Betacomio_Project.Controllers
{
   
    [Route("api/[controller]")]
    [ApiController]
    public class ViewUserProductsController : ControllerBase
    {
        private readonly BetacomioCyclesContext _context;
        private readonly RegexCh _regex;
        private static Logger logger = LogManager.GetCurrentClassLogger();



        public ViewUserProductsController(BetacomioCyclesContext context,  RegexCh regex)
        {
            _context = context;
            _regex = regex;
        }

        
        [HttpGet("GetUserProducts")]
        public async Task<ActionResult<IEnumerable<ViewUserProduct>>> GetUserProducts()
        {
            List<ViewUserProduct> viewUserProducts;

            try
            {
                if (_context.ViewUserProducts == null)
                {
                    return NotFound();
                }


                viewUserProducts = await _context.ViewUserProducts.ToListAsync();
            }
            catch (Exception ex)
            {

                logger.WithProperty("ErrorCode", ex.HResult)
                    .WithProperty("ErrorClass", ex.TargetSite.DeclaringType.ToString())
                    .Error("{Message}", ex.Message);

                return BadRequest("Errore durante lettura dati view UserProducts");
            }

            return viewUserProducts;

        }

        [HttpGet("GetUserProductsByLanguage")]
        public async Task<ActionResult<IEnumerable<ViewUserProduct>>> GetUserProductsByLanguage([FromQuery] int nationality)
        {
            try
            {
                if (_context.ViewUserProducts == null)
                {
                    return NotFound();
                }

                var nationalLanguages = await _context.LanguageEnums
                    .Where(lang => lang.Id == nationality)
                    .Select(lang => lang.LanguageCode)
                    .FirstAsync(); // Estrae lingua in base a input utente

                var productsByLanguage = await _context.ViewUserProducts
                    .Where(val => nationalLanguages.Equals(val.Culture)) // confronto lingua utente con lingua prodotti in DB
                    .ToListAsync();

                return Ok(productsByLanguage);
                
            }
            catch (Exception ex)
            {

                logger.WithProperty("ErrorCode", ex.HResult)
                    .WithProperty("ErrorClass", ex.TargetSite.DeclaringType.ToString())
                    .Error("{Message}", ex.Message);

                return BadRequest("Errore durante lettura prodotti in lingua (View)");
            }
        }


        [HttpGet("{name}")]  
        public async Task<ActionResult<ViewUserProduct>> GetUserProductsByID(MainSingleton connectao, string name, int nationality)  //dettaglio prodotto
        {
            try
            {
                if (_context.ViewUserProducts == null)
                {
                    return NotFound();
                }

                var allProducts = await _regex.ProductsWithLanguage(connectao, nationality);
                var prodDetail_language = allProducts.Where(val => val.Name == name).First();

                return Ok(prodDetail_language);

            }
            catch (Exception ex)
            {

                logger.WithProperty("ErrorCode", ex.HResult)
                    .WithProperty("ErrorClass", ex.TargetSite.DeclaringType.ToString())
                    .Error("{Message}", ex.Message);

                return BadRequest("Errore durante lettura prodotti in lingua (View id)");
            }

        }


        private bool UserProductExists(string name)
        {
            return (_context.ViewUserProducts?.Any(val => val.Name == name)).GetValueOrDefault();
        }
    }
}
