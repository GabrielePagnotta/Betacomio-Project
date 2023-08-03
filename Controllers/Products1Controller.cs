﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Betacomio_Project.Models;
using Microsoft.AspNetCore.Authorization;
using Betacomio_Project.ConnectDb;
using System.Data.SqlClient;

namespace Betacomio_Project.Controllers
{
    [Authorize("BasicAuthentication")]
    [Route("api/[controller]")]
    [ApiController]
    public class Products1Controller : ControllerBase
    {
        SqlConnection connection = new SqlConnection();
        private readonly AdventureWorksLt2019Context _context;
        private readonly LoginUser _login;
        private readonly SingleTonConnectDB _connession;
        public Products1Controller(AdventureWorksLt2019Context context, LoginUser login, SingleTonConnectDB connession)
        {
            _context = context;
            _login = login;
            _connession = connession;
        }

        // GET: api/Products1
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
          Classprova classprova = new Classprova();
            classprova.MyMethod();
          if (_context.Products == null)
          {
              return NotFound();
          }
           
                return await _context.Products.Take(1).Include(cat => cat.ProductCategory).Include(prod => prod.ProductModel).ThenInclude(proAnn => proAnn.ProductModelProductDescriptions).ThenInclude(descr => descr.ProductDescription).ToListAsync();
          
            return BadRequest(404);
        }


        // PUT: api/Products1/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProduct(int id, Product product)
        {
            if (id != product.ProductId)
            {
                return BadRequest();
            }

            _context.Entry(product).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductExists(id))
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

        // POST: api/Products1
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Product>> PostProduct(Product product)
        {
          if (_context.Products == null)
          {
              return Problem("Entity set 'AdventureWorksLt2019Context.Products'  is null.");
          }
           
    
             
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetProduct", new { id = product.ProductId }, product);
        }

        // DELETE: api/Products1/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            if (_context.Products == null)
            {
                return NotFound();
            }
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProductExists(int id)
        {
            return (_context.Products?.Any(e => e.ProductId == id)).GetValueOrDefault();
        }
    }
}