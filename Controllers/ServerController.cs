using System.Linq;
using Advantage.API.Models;
using Advantage.API.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Advantage.API.Controllers
{
    [Route("api/[controller]")]
    public class ServerController : Controller
    {
        private readonly ApiContext _context;

        public ServerController(ApiContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var response = _context.Servers.OrderBy(x => x.Id).ToList();
            return Ok(response);
        }

        [HttpGet("{id}", Name = "GetServer")]
        public IActionResult Get(int id)
        {
            var response = _context.Servers.FirstOrDefault(x => x.Id == id);
            return Ok(response);
        }

        [HttpPut("{id}")]
        public IActionResult Message(int id, [FromBody] ServerMessage msg)
        {
            var server = _context.Servers.FirstOrDefault(x => x.Id == id);

            if (server is null)
            {
                return NotFound();
            }
            
            //Refactor: move into a service
            if (msg.Payload == "activate")
            {
                server.IsOnline = true;
                _context.SaveChanges();
            }

            if (msg.Payload == "deactivate")
            {
                server.IsOnline = false;
                _context.SaveChanges();
            }

            return new NoContentResult();
        }
    }
}