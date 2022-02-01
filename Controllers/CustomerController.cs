using System.Linq;
using Advantage.API.Models;
using Advantage.API.Models.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Advantage.API.Controllers
{
    [Route("api/[controller]")]
    public class CustomerController : Controller
    {
        private readonly ApiContext _context;

        public CustomerController(ApiContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var data = _context.Customers.OrderBy(c => c.Id);

            return Ok(data);
        }

        [HttpGet("{id}", Name = "GetCustomer")]
        public IActionResult Get(int id)
        {
            var customer = _context.Customers.Find(id);
            return Ok(customer);
        }

        [HttpPost]
        public IActionResult Post([FromBody] Customer customer)
        {
            if (customer is null)
            {
                return BadRequest();
            }

            _context.Customers.Add(customer);
            _context.SaveChanges();
            return CreatedAtRoute("GetCustomer", new {id = customer.Id}, customer);
        }
    }
}