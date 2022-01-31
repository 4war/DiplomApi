using System;
using System.Collections.Generic;
using System.Linq;
using Advantage.API.Models.Entities;

namespace Advantage.API.Models.Seeds
{
    public class DataSeed
    {
        private readonly ApiContext _context;

        public DataSeed(ApiContext context)
        {
            _context = context;
        }

        public void SeedData(int numberCustomers, int numberOrders)
        {
            _context.Database.EnsureCreated();
            if (!_context.Customers.Any())
            {
                SeedCustomers(numberCustomers);
                _context.SaveChanges();
            }

            if (!_context.Orders.Any())
            {
                SeedOrders(numberOrders);
                _context.SaveChanges();
            }
            
            if (!_context.Servers.Any())
            {
                SeedServers();
                _context.SaveChanges();
            }
        }

        private void SeedCustomers(int numberCustomers)
        {
            var list = GenerateCustomerList(numberCustomers);
            foreach (var customer in list)
            {
                _context.Customers.Add(customer);
            }
        }
        

        private List<Customer> GenerateCustomerList(int number)
        {
            var list = new List<Customer>();
            var nameSet = new HashSet<string>();
            
            for (var i = 1; i <= number; i++)
            {
                var name = Helpers.MakeUniqueCustomerName(nameSet);
                nameSet.Add(name);

                list.Add(new Customer()
                {
                    Id = i,
                    Name = name,
                    Email = Helpers.MakeCustomerEmail(name),
                    State = Helpers.GetRandomState() 
                });
            }

            return list;
        }

        private void SeedOrders(int numberOrders)
        {
            var list = GenerateOrderList(numberOrders);
            foreach (var order in list)
            {
                _context.Orders.Add(order);
            }
        }
        
        private List<Order> GenerateOrderList(int number)
        {
            var list = new List<Order>();
            var random = new Random();

            for (var i = 1; i <= number; i++)
            {
                var randomCustomerId = random.Next(1,_context.Customers.Count());
                var placed = Helpers.GetRandomOrderPlaced();
                var completed = Helpers.GetRandomOrderCompleted(placed);
                
                list.Add(new Order()
                {
                    Id = i,
                    Customer = _context.Customers.First(x => x.Id == randomCustomerId),
                    Total = Helpers.GetRandomOrderTotal(),
                    Placed = placed,
                    Completed = completed,
                });
            }

            return list;
        }

        private void SeedServers()
        {
            var list = GenerateServerList();
            foreach (var server in list)
            {
                _context.Servers.Add(server);
            }
        }

        private List<Server> GenerateServerList()
        {
            return new List<Server>()
            {
                new Server()
                {
                    Id = 1,
                    Name = "Dev-Web",
                    IsOnline = true
                },
                new Server()
                {
                    Id = 2,
                    Name = "Dev-Mail",
                    IsOnline = false
                },
                new Server()
                {
                    Id = 3,
                    Name = "Dev-Services",
                    IsOnline = true
                },
                new Server()
                {
                    Id = 4,
                    Name = "QA-Web",
                    IsOnline = true
                },
                new Server()
                {
                    Id = 5,
                    Name = "QA-Mail",
                    IsOnline = false
                },
                new Server()
                {
                    Id = 6,
                    Name = "QA-Services",
                    IsOnline = true
                },
                new Server()
                {
                    Id = 7,
                    Name = "Prod-Web",
                    IsOnline = true
                },
                new Server()
                {
                    Id = 8,
                    Name = "Prod-Mail",
                    IsOnline = true
                },
                new Server()
                {
                    Id = 9,
                    Name = "Prod-Services",
                    IsOnline = true
                }
            };
        }
    }
}