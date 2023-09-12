
using Betacomio_Project.NewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NLog;

namespace Betacomio_Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ViewAdminUserRegistryController : ControllerBase
    {
        private readonly BetacomioCyclesContext _context;
        private static Logger logger = LogManager.GetCurrentClassLogger();



        public ViewAdminUserRegistryController(BetacomioCyclesContext context)
        {
            _context = context;
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<ViewAdminUserRegistry>>> GetAdmin_UserRegistries()
        {
            if (_context.ViewAdminUserRegistries == null)
            {
                return NotFound();
            }

            List<ViewAdminUserRegistry> userRegistry;
            try
            {
                userRegistry = await _context.ViewAdminUserRegistries.ToListAsync();
            }
            catch (Exception ex)
            {
                logger.WithProperty("ErrorCode", ex.HResult)
                        .WithProperty("ErrorClass", ex.TargetSite.DeclaringType.ToString())
                        .Error("{Message}", ex.Message);

                return BadRequest("Errore durante lettura dati view AdminUserRegistries");
            }

            return userRegistry;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ViewAdminUserRegistry>> GetAdmin_UserRegistriesByID(int id)
        {
            if (_context.ViewAdminUserRegistries == null)
            {
                return NotFound();
            }

            ViewAdminUserRegistry userRegistrysingle;
            try
            {
                userRegistrysingle = await _context.ViewAdminUserRegistries.Where(val => val.UserId == id).FirstAsync();
            }
            catch(Exception ex)
            {
                logger.WithProperty("ErrorCode", ex.HResult)
                    .WithProperty("ErrorClass", ex.TargetSite.DeclaringType.ToString())
                    .Error("{Message}", ex.Message);

                return BadRequest("Errore durante lettura dati view AdminUserRegistries 2");
            }
            

            if (userRegistrysingle == null)
            {
                return NotFound();
            }

            return userRegistrysingle;
        }



        private bool UserRegistryExists(int id)
        {
            return (_context.ViewAdminUserRegistries?.Any(e => e.UserId == id)).GetValueOrDefault();
        }
    }
}
