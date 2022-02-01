using System;
using System.Linq;
using Advantage.API.Models;
using Advantage.API.Models.Entities;
using Advantage.API.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Advantage.API.Controllers
{
    [Route("api/[controller]")]
    public class OrderController : Controller
    {
        private readonly ApiContext _context;

        public OrderController(ApiContext context)
        {
            _context = context;
        }

        [HttpGet("{pageIndex:int}/{pageSize:int}")]
        public IActionResult Get(int pageIndex, int pageSize)
        {
            var data = _context.Orders
                .Include(x => x.Customer)
                .OrderByDescending(x => x.Placed);

            var page = new PaginateResponse<Order>(data, pageIndex, pageSize);
            var totalCount = data.Count();
            var totalPages = Math.Ceiling((float)totalCount / pageSize);

            var response = new
            {
                Page = page,
                TotalPages = totalPages,
            };
            
            return Ok(response);
        }


        [HttpGet("ByState")]
        public IActionResult ByState()
        {
            var orders = _context.Orders.Include(x => x.Customer).ToList();
            var groupedResult = orders
                .GroupBy(x => x.Customer.State)
                .Select(x => new
                {
                    State = x.Key,
                    Total = x.Sum(y => y.Total)
                }).OrderByDescending(x => x.Total)
                .ToList();
            
            return Ok(groupedResult);
        }
        
        [HttpGet("ByCustomer/{n}")]
        public IActionResult ByCustomer(int n = 50)
        {
            var orders = _context.Orders.Include(x => x.Customer).ToList();
            var groupedResult = orders
                .GroupBy(x => x.Customer.Id)
                .Select(x => new
                {
                    Name = _context.Customers.Find(x.Key).Name,
                    Total = x.Sum(y => y.Total)
                }).OrderByDescending(x => x.Total)
                .Take(n)
                .ToList();
                
            return Ok(groupedResult);
        }
        
        [HttpGet("GetOrder/{id}", Name = "GetOrder")]
        public IActionResult GetOrder(int id)
        {
            var order = _context.Orders.Include(x => x.Customer).First(x => x.Id == id);
            return Ok(order);
        }
    }
}