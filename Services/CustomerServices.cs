using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using acme_publishing_app.Context;
using acme_publishing_app.Models;

namespace acme_publishing_app.Services
{
    public class CustomerService : ICustomerService
    {
        public readonly WebApiContext _webApiContext;
        public CustomerService(WebApiContext context)
        {
            _webApiContext = context;
            _webApiContext.Database.EnsureCreated();
        }

        public async Task<Customer> GetCustomer(string id)
        {
            var Customer = new Customer();
            await Task.Run(() =>
            {
                Customer = _webApiContext.Customers.FirstOrDefault(p => p.Id == id);
            });
            return Customer;
        }

        public async Task<List<Customer>> GetCustomers()
        {
            var Customer = new List<Customer>();
            await Task.Run(() =>
            {
                Customer = _webApiContext.Customers.ToList();
            });
            return Customer;
        }

        public async Task<Customer> PostCustomer(Customer Customer)
        {
            Customer.Id = Guid.NewGuid().ToString();
            await Task.Run(() =>
            {
                var newCustomer = _webApiContext.Customers.AddAsync(Customer);
            });
            _webApiContext.SaveChanges();

            return Customer;
        }

        public async Task<Customer> PutCustomer(string id, Customer newCustomer)
        {
            var Customer = new Customer();
            await Task.Run(() =>
            {
                Customer = _webApiContext.Customers.FirstOrDefault(p => p.Id == id);
            });
            _webApiContext.SaveChanges();

            return Customer;
        }

        public async Task DeleteCustomer(string id)
        {
            var Customer = new Customer();
            await Task.Run(() =>
            {
                Customer = _webApiContext.Customers.FirstOrDefault(p => p.Id == id);
                var oldCustomer = _webApiContext.Customers.Remove(Customer);
            });
            _webApiContext.SaveChanges();
        }
    }
}