using Domino.Data;
using Domino.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Domino.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServiceController : ControllerBase
    {
        private readonly DominodbContext _context;
        public ServiceController(DominodbContext context)
        {
            _context = context; 
        }
        // GET: api/Carts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Cart>>> Getcart()
        {
            if (_context.cart == null)
            {
                return NotFound();
            }
            return await _context.cart.ToListAsync();
        }

        // GET: api/Carts/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Cart>> GetCart(int id)
        {
            if (_context.cart == null)
            {
                return NotFound();
            }
            var cart = await _context.cart.FindAsync(id);

            if (cart == null)
            {
                return NotFound();
            }

            return cart;
        }

        // PUT: api/Carts/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCart(int id, Cart cart)
        {
            if (id != cart.CartID)
            {
                return BadRequest();
            }

            _context.Entry(cart).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CartExists(id))
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

        // POST: api/Carts
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Cart>> PostCart(Cart cart)
        {
            if (_context.cart == null)
            {
                return Problem("Entity set 'DominodbContext.cart'  is null.");
            }
            _context.cart.Add(cart);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCart", new { id = cart.CartID }, cart);
        }

        // DELETE: api/Carts/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCart(int id)
        {
            if (_context.cart == null)
            {
                return NotFound();
            }
            var cart = await _context.cart.FindAsync(id);
            if (cart == null)
            {
                return NotFound();
            }

            _context.cart.Remove(cart);
            await _context.SaveChangesAsync();

            return NoContent();
        }
        private bool CartExists(int id)
        {
            return (_context.cart?.Any(e => e.CartID == id)).GetValueOrDefault();
        }
        [HttpPost]
        [Route("AddToCart")]
        public async Task<ActionResult<Cart>> AddToCart(int cid,int pid,int qty)
        {
            //if (_context.cart == null)
            //{
            //    return Problem("Entity set 'DominodbContext.cart'  is null.");
            //}
            Cart cart=new Cart();
            cart.CustomerID = cid;
            Customer customer= (from i in _context.customers
                                where i.CustomerID == cart.CustomerID
                                select i).SingleOrDefault();
            //cart.CartTypeID = (from i in _context.customers
            //                   where i.CustomerID == cart.CustomerID
            //                   select i.CartTypeID).SingleOrDefault();
            cart.CartTypeID = customer.CartTypeID;
            if(cart.CartTypeID == null)
            {
                Guid obj = Guid.NewGuid();
                //Console.WriteLine("New Guid is " + obj.ToString());
                cart.CartTypeID = obj.ToString();
                customer.CartTypeID = obj.ToString();
            }
           
            cart.PizzaID=pid;
            cart.Quantity = qty;
            cart.TotalAmount = qty * (from i in _context.pizza
                                      where i.PizzaID == cart.PizzaID
                                      select i.Price).SingleOrDefault();
         
          
            _context.cart.Add(cart);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCart", new { id = cart.CartID }, cart);
        }
    }
}
