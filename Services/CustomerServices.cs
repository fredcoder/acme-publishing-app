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
            var customer = new Customer();
            await Task.Run(() =>
            {
                customer = _webApiContext.Customers
                .FirstOrDefault(p => p.Id == id);
            });
            return customer;
        }

        public async Task<List<Customer>> GetCustomers()
        {
            var customer = new List<Customer>();
            await Task.Run(() =>
            {
                customer = _webApiContext.Customers
                .ToList();
            });
            return customer;
        }

        public async Task<Customer> PostCustomer(Customer customer)
        {
            customer.Id = Guid.NewGuid().ToString();
            await Task.Run(() =>
            {
                var newCustomer = _webApiContext.Customers.AddAsync(customer);
            });
            _webApiContext.SaveChanges();

            return customer;
        }

        public async Task<Customer> PutCustomer(string id, Customer newCustomer)
        {
            var customer = new Customer();
            await Task.Run(() =>
            {
                customer = _webApiContext.Customers.FirstOrDefault(p => p.Id == id);
                customer.FirstName = newCustomer.FirstName;
                customer.LastName = newCustomer.LastName;
            });
            _webApiContext.SaveChanges();

            return customer;
        }

        public async Task DeleteCustomer(string id)
        {
            var customer = new Customer();
            await Task.Run(() =>
            {
                customer = _webApiContext.Customers.FirstOrDefault(p => p.Id == id);
                var oldCustomer = _webApiContext.Customers.Remove(customer);
            });
            _webApiContext.SaveChanges();
        }
    }
}