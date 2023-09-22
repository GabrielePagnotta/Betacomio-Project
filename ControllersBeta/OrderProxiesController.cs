using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Betacomio_Project.LogModels;
using Microsoft.IdentityModel.Tokens;
using RegexCheck;
using Betacomio_Project.ConnectDb;
using System.Runtime.InteropServices;

namespace Betacomio_Project.ControllersBeta
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderProxiesController : ControllerBase
    {
        private readonly SingleTonConnectDB _connect;
        private readonly MainSingleton _connectao;
        private readonly AdminLogContext _context;
        private readonly RegexCh _reg;

        public OrderProxiesController(AdminLogContext context , RegexCh reg , SingleTonConnectDB connect, MainSingleton connectao)
        {
            _context = context;
            _reg = reg;
            _connect = connect;
            _connectao = connectao;
        }

        // GET: api/OrderProxies
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderProxy>>> GetOrderProxies()
        {
          if (_context.OrderProxies == null)
          {
              return NotFound();
          }
            return await _context.OrderProxies.ToListAsync();
        }

        // GET: api/OrderProxies/5
        [HttpGet("{id}")]
        public async Task<ActionResult<OrderProxy>> GetOrderProxy(int id)
        {
          if (_context.OrderProxies == null)
          {
              return NotFound();
          }
            var orderProxy = await _context.OrderProxies.FindAsync(id);

            if (orderProxy == null)
            {
                return NotFound();
            }

            return orderProxy;
        }

        // PUT: api/OrderProxies/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOrderProxy(int id, OrderProxy orderProxy)
        {
            if (id != orderProxy.GenericId)
            {
                return BadRequest();
            }

            _context.Entry(orderProxy).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrderProxyExists(id))
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

        // POST: api/OrderProxies
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<OrderProxy>> PostOrderProxy(OrderProxy orderproxy)
        {
            int IDAddress = 0;
            int AddressExist = _reg.CheckAddress(_connect, orderproxy.userUniqueData.CustomerId, orderproxy.userUniqueData.Address);
            // 1) inserimento dati in Addresses
            if ( AddressExist == 0)
            {
                IDAddress = _reg.NewAddress(_connect, orderproxy.userUniqueData.Address, orderproxy.userUniqueData.AddressDetail, orderproxy.userUniqueData.City, orderproxy.userUniqueData.Region, orderproxy.userUniqueData.Country, orderproxy.userUniqueData.PostalCode);

               // 2) Controllo se esiste associazione utente - indirizzo in UserAddress
                _reg.BindUSerAndAddress(_connect, orderproxy.userUniqueData.CustomerId, IDAddress);
            }
            else
            {
                IDAddress = AddressExist;
            }
         
         
           // 3) inserimento dati in Order Header + ottengo OrderID 
           int OrderID =  _reg.OrderHeaderInsert(_connect, IDAddress, orderproxy.userUniqueData.CustomerId, (int)orderproxy.userUniqueData.SubTotal);
         
          //  4) Inserisco i dati in OrderDetail (ciclando prodotti carrello)
            foreach (var item in orderproxy.detailData)
            {
                _reg.OrderDetilInsert(_connect, item.OrderQty , item.ProductId, item.UnitPrice, item.TotalPrice , OrderID);
            }


            
            return Ok();
        }

        // DELETE: api/OrderProxies/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrderProxy(int id)
        {
            if (_context.OrderProxies == null)
            {
                return NotFound();
            }
            var orderProxy = await _context.OrderProxies.FindAsync(id); 
            if (orderProxy == null)
            {
                return NotFound();
            }

            _context.OrderProxies.Remove(orderProxy);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool OrderProxyExists(int id)
        {
            return (_context.OrderProxies?.Any(e => e.GenericId == id)).GetValueOrDefault();
        }
    }
}
