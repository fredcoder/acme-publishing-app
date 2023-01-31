using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using acme_publishing_app.Models;

namespace acme_publishing_app.Services
{
    public interface ICustomerService
    {
        public Task<Customer> GetCustomer(string id);
        public Task<List<Customer>> GetCustomers();
        public Task<Customer> PostCustomer(Customer newCustomer);
        public Task<Customer> PutCustomer(string id, Customer newCustomer);
        public Task DeleteCustomer(string id);
    }
}