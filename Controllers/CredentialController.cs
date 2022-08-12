using Domino.Data;
using Domino.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Domino.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CredentialController : ControllerBase
    {
        private readonly DominodbContext db;
        public CredentialController(DominodbContext db)
        {
            this.db = db;
        }   
        [HttpPost]
        [Route("CustomerRegn")]
        public async Task<ActionResult<Customer>> CustomerRegn(Customer c)
        {
            try
            {
                await db.customers.AddAsync(c);
                await db.SaveChangesAsync();
                return Ok(c);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error creating new Customer record");
            }
        }
        [HttpPost]
        [Route("CustomerLogin")]
        public async Task<ActionResult<Customer>> CustomerLogin(Customer c)
        {
            try
            {
                var customer = (from i in db.customers
                                where i.EmailID == c.EmailID && i.Password == c.Password
                                select i).SingleOrDefault();
                if(customer == null)
                {
                    return BadRequest("Invalid Credential");
                }
                return customer;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                   "Wrong Entry");
            }
        }
        [HttpPost]
        [Route("AdminLogin")]
        public async Task<ActionResult<Admin>> AdminLogin(Admin a)
        {
            try
            {
                var admin = (from i in db.admin
                             where i.EmailID == a.EmailID && i.Password == a.Password
                             select i).SingleOrDefault();
                return admin;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                  "Wrong Entry");
            }
        }
    }
}
