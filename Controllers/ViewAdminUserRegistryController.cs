using Betacomio_Project.Models;
using Betacomio_Project.NewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Betacomio_Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ViewAdminUserRegistryController : ControllerBase
    {
        private readonly BetacomioCyclesContext _context;


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
            return await _context.ViewAdminUserRegistries.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ViewAdminUserRegistry>> GetAdmin_UserRegistriesByID(int id)
        {
            if (_context.ViewAdminUserRegistries == null)
            {
                return NotFound();
            }
            var userRegistry = await _context.ViewAdminUserRegistries.Where(val => val.UserId == id).FirstAsync();

            if (userRegistry == null)
            {
                return NotFound();
            }

            return userRegistry;
        }



        private bool UserRegistryExists(int id)
        {
            return (_context.ViewAdminUserRegistries?.Any(e => e.UserId == id)).GetValueOrDefault();
        }
    }
}
