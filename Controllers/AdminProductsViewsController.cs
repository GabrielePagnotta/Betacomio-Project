using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Betacomio_Project.Models;

namespace Betacomio_Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminProductsViewsController : ControllerBase
    {
        private readonly AdventureWorksLt2019Context _context;


        public AdminProductsViewsController(AdventureWorksLt2019Context context)
        {
            _context = context;
        }

        // GET: api/AdminProductsViews
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AdminProductsView>>> GetAdminProductsViews()
        {
          if (_context.AdminProductsViews == null)
          {
              return NotFound();
          }
            return await _context.AdminProductsViews.ToListAsync();
        }

        // GET: api/AdminProductsViews/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AdminProductsView>> GetAdminProductsView(int id)
        {
          if (_context.AdminProductsViews == null)
          {
              return NotFound();
          }
            var adminProductsView = await _context.AdminProductsViews.FindAsync(id);

            if (adminProductsView == null)
            {
                return NotFound();
            }

            return adminProductsView;
        }

    

        private bool AdminProductsViewExists(int id)
        {
            return (_context.AdminProductsViews?.Any(e => e.ProductId == id)).GetValueOrDefault();
        }
    }
}
