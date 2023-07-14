using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Betacomio_Project.Models;
using NuGet.Versioning;
using Microsoft.AspNetCore.Authorization;

namespace Betacomio_Project.Controllers
{
    [Authorize("BasicAuthentication")]
    [Route("[controller]")]
    [ApiController]
    public class ProductModelsController : ControllerBase
    {
        private readonly AdventureWorksLt2019Context _context;
        SqlQuery test=new SqlQuery();

        public ProductModelsController(AdventureWorksLt2019Context context)
        {
            _context = context;
        }


             #region chiamata GET
        //https://localhost:7284/ProductModels -- chiamata get 
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductModel>>> GetProductModels()
        {
          if (_context.ProductModels == null)
          {
              return NotFound();
          }
             return await _context.ProductModels.Include(x=>x.ProductModelProductDescriptions).Include(h=>h.Products).ToListAsync();
          //  return  test.Homequery().ToArray();
        }
        #endregion 




        #region
        //https://localhost:7284/ProductModels-- ricerca per ID
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductModel>> GetProductModel(int id)
        {
          if (_context.ProductModels == null)
          {
              return NotFound();
          }
            var productModel = await _context.ProductModels.FindAsync(id);

            if (productModel == null)
            {
                return NotFound();
            }

            return productModel;
        }
        #endregion



        #region
        //https://localhost:7284/ProductModels/HomePage/GetProductByName/{inserire il nome del prodotto}//
        [Route("HomePage/GetProductByName/{name}")]
        [HttpGet] 
    
        public async Task<ActionResult<ProductModel>> GetName(string name)
        {  //da gestire gli errori 
            try
            {
                if (_context.ProductModels == null)
                {
                    return NotFound();
                }
                var productModel = await _context.ProductModels.Include(x => x.ProductModelProductDescriptions).Include(h => h.Products.Where(nam => nam.Name.Contains(name))).FirstAsync();
               
                if (productModel == null)
                {
                    return NotFound();
                }

                return productModel;
            }
            catch (Exception)
            {

                throw;
            }
           
        }
        #endregion



        private bool ProductModelExists(int id)
        {
            return (_context.ProductModels?.Any(e => e.ProductModelId == id)).GetValueOrDefault();
        }
    }
}
