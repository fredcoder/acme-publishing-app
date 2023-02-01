using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using acme_publishing_app.Models;
using Microsoft.EntityFrameworkCore;

namespace acme_publishing_app.Context
{
    public class WebApiContext : DbContext
    {
        public WebApiContext(DbContextOptions<WebApiContext> options) : base(options)
        {

        }

        public DbSet<Country> Countries { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Subscription> Subscriptions { get; set; }
        public DbSet<PrintDistCompany> PrintDistCompanies { get; set; }
        public DbSet<DeliveryAddress> DeliveryAddresses { get; set; }
        public DbSet<DeliveryOrder> DeliveryOrders { get; set; }
        public DbSet<DeliveryCompany> DeliveryCompanies { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            List<Country> countries = new List<Country>();
            countries.Add(new Country { Id = "b00db9eb-7650-4878-b814-8a96d5a8220e", Name = "Australia" });
            countries.Add(new Country { Id = "f30e74cd-2494-4fc8-8eb3-8de05c4a821e", Name = "New Zealand" });
            countries.Add(new Country { Id = "070e30e7-488b-47e3-ad28-4379b9be6185", Name = "United States" });
            countries.Add(new Country { Id = "74ca9e4c-2f51-4bfb-8ee0-12efe0db187f", Name = "United Kingdom" });
            countries.Add(new Country { Id = "d6d124a9-df5b-4ae8-848d-d15bb33bbd19", Name = "South Africa" });

            builder.Entity<Country>().ToTable("Country").HasKey(p => p.Id);
            builder.Entity<Country>().ToTable("Country").HasData(countries);

            List<Subscription> subscriptions = new List<Subscription>();
            subscriptions.Add(new Subscription { Id = "50909b94-5134-4392-be80-13bcccd2086c", Name = "Magazine 1" });
            subscriptions.Add(new Subscription { Id = "7a852c93-ca4f-4ab5-84ac-c8b70927506e", Name = "Magazine 2" });
            subscriptions.Add(new Subscription { Id = "7caba613-eaa2-4c83-9d47-97b438169f95", Name = "Magazine 3" });

            builder.Entity<Subscription>().ToTable("Subscription").HasKey(p => p.Id);
            builder.Entity<Subscription>().ToTable("Subscription").HasData(subscriptions);

            List<Customer> customers = new List<Customer>();
            customers.Add(new Customer { Id = "c208fdd5-75d5-4cb9-b202-ed668b77222c", FirstName = "Laura", LastName = "Simmons" });
            customers.Add(new Customer { Id = "cf994cb1-dc3c-4822-93f7-a7422a016ca9", FirstName = "John", LastName = "Thomas" });
            customers.Add(new Customer { Id = "d5d4c8e5-c7c1-4e4a-8ba5-49765082f826", FirstName = "David", LastName = "Rogers" });

            builder.Entity<Customer>().ToTable("Customer").HasKey(p => p.Id);
            builder.Entity<Customer>().ToTable("Customer").HasData(customers);

            List<PrintDistCompany> printDistCompanies = new List<PrintDistCompany>();
            printDistCompanies.Add(new PrintDistCompany { Id = "e094d643-1e9b-4fe5-a051-e4e7a4753a48", Name = "Printer Company 1", ApiUrl = "http://printer1/api" });
            printDistCompanies.Add(new PrintDistCompany { Id = "eeed9a4a-e29f-403c-9437-eb34f45948b4", Name = "Printer Company 2", ApiUrl = "http://printer2/api" });

            builder.Entity<PrintDistCompany>().ToTable("PrintDistCompany").HasKey(p => p.Id);
            builder.Entity<PrintDistCompany>().ToTable("PrintDistCompany").HasData(printDistCompanies);

            List<DeliveryAddress> deliveryAddresses = new List<DeliveryAddress>();
            deliveryAddresses.Add(new DeliveryAddress { Id = "ef813fe9-8ef1-4809-8822-6c9eb5feefea", Description = "Av 10", CustomerId = "c208fdd5-75d5-4cb9-b202-ed668b77222c", CountryId = "b00db9eb-7650-4878-b814-8a96d5a8220e" });
            deliveryAddresses.Add(new DeliveryAddress { Id = "f6095b43-57f1-49a7-9acf-ac3d145fef4f", Description = "Av 20", CustomerId = "c208fdd5-75d5-4cb9-b202-ed668b77222c", CountryId = "f30e74cd-2494-4fc8-8eb3-8de05c4a821e" });
            deliveryAddresses.Add(new DeliveryAddress { Id = "fd0e69f0-a1af-4091-b8a4-815a058d927a", Description = "Av 30", CustomerId = "cf994cb1-dc3c-4822-93f7-a7422a016ca9", CountryId = "070e30e7-488b-47e3-ad28-4379b9be6185" });
            deliveryAddresses.Add(new DeliveryAddress { Id = "74ca9e4c-2f51-4bfb-8ee0-12efe0db187f", Description = "Av 40", CustomerId = "cf994cb1-dc3c-4822-93f7-a7422a016ca9", CountryId = "74ca9e4c-2f51-4bfb-8ee0-12efe0db187f" });
            deliveryAddresses.Add(new DeliveryAddress { Id = "d6d124a9-df5b-4ae8-848d-d15bb33bbd19", Description = "Av 50", CustomerId = "d5d4c8e5-c7c1-4e4a-8ba5-49765082f826", CountryId = "d6d124a9-df5b-4ae8-848d-d15bb33bbd19" });

            builder.Entity<DeliveryAddress>().ToTable("DeliveryAddress").HasKey(p => p.Id);
            builder.Entity<DeliveryAddress>().ToTable("DeliveryAddress").HasData(deliveryAddresses);

            List<DeliveryOrder> deliveryOrders = new List<DeliveryOrder>();
            deliveryOrders.Add(new DeliveryOrder { Id = "100db9eb-7650-4878-b814-8a96d5a82201", SubscriptionId = "50909b94-5134-4392-be80-13bcccd2086c", DeliveryAddressId = "ef813fe9-8ef1-4809-8822-6c9eb5feefea", PrintDistCompanyId = "e094d643-1e9b-4fe5-a051-e4e7a4753a48" });
            deliveryOrders.Add(new DeliveryOrder { Id = "230e74cd-2494-4fc8-8eb3-8de05c4a8212", SubscriptionId = "50909b94-5134-4392-be80-13bcccd2086c", DeliveryAddressId = "f6095b43-57f1-49a7-9acf-ac3d145fef4f", PrintDistCompanyId = "e094d643-1e9b-4fe5-a051-e4e7a4753a48" });
            deliveryOrders.Add(new DeliveryOrder { Id = "370e30e7-488b-47e3-ad28-4379b9be6183", SubscriptionId = "50909b94-5134-4392-be80-13bcccd2086c", DeliveryAddressId = "fd0e69f0-a1af-4091-b8a4-815a058d927a", PrintDistCompanyId = "e094d643-1e9b-4fe5-a051-e4e7a4753a48" });
            deliveryOrders.Add(new DeliveryOrder { Id = "44ca9e4c-2f51-4bfb-8ee0-12efe0db1874", SubscriptionId = "7a852c93-ca4f-4ab5-84ac-c8b70927506e", DeliveryAddressId = "74ca9e4c-2f51-4bfb-8ee0-12efe0db187f", PrintDistCompanyId = "eeed9a4a-e29f-403c-9437-eb34f45948b4" });
            deliveryOrders.Add(new DeliveryOrder { Id = "56d124a9-df5b-4ae8-848d-d15bb33bbd15", SubscriptionId = "7caba613-eaa2-4c83-9d47-97b438169f95", DeliveryAddressId = "d6d124a9-df5b-4ae8-848d-d15bb33bbd19", PrintDistCompanyId = "eeed9a4a-e29f-403c-9437-eb34f45948b4" });

            builder.Entity<DeliveryOrder>().ToTable("DeliveryOrder").HasKey(p => p.Id);
            builder.Entity<DeliveryOrder>().ToTable("DeliveryOrder").HasData(deliveryOrders);

            List<DeliveryCompany> deliveryCompanies = new List<DeliveryCompany>();
            deliveryCompanies.Add(new DeliveryCompany { SubscriptionId = "50909b94-5134-4392-be80-13bcccd2086c", CountryId = "ef813fe9-8ef1-4809-8822-6c9eb5feefea", PrintDistCompanyId = "e094d643-1e9b-4fe5-a051-e4e7a4753a48" });
            deliveryCompanies.Add(new DeliveryCompany { SubscriptionId = "50909b94-5134-4392-be80-13bcccd2086c", CountryId = "f6095b43-57f1-49a7-9acf-ac3d145fef4f", PrintDistCompanyId = "e094d643-1e9b-4fe5-a051-e4e7a4753a48" });
            deliveryCompanies.Add(new DeliveryCompany { SubscriptionId = "50909b94-5134-4392-be80-13bcccd2086c", CountryId = "fd0e69f0-a1af-4091-b8a4-815a058d927a", PrintDistCompanyId = "e094d643-1e9b-4fe5-a051-e4e7a4753a48" });
            deliveryCompanies.Add(new DeliveryCompany { SubscriptionId = "7a852c93-ca4f-4ab5-84ac-c8b70927506e", CountryId = "74ca9e4c-2f51-4bfb-8ee0-12efe0db187f", PrintDistCompanyId = "eeed9a4a-e29f-403c-9437-eb34f45948b4" });
            deliveryCompanies.Add(new DeliveryCompany { SubscriptionId = "7caba613-eaa2-4c83-9d47-97b438169f95", CountryId = "d6d124a9-df5b-4ae8-848d-d15bb33bbd19", PrintDistCompanyId = "eeed9a4a-e29f-403c-9437-eb34f45948b4" });

            builder.Entity<DeliveryCompany>().ToTable("DeliveryCompany").HasKey(p => new { p.SubscriptionId, p.CountryId });
            builder.Entity<DeliveryCompany>().ToTable("DeliveryCompany").HasData(deliveryCompanies);

        }
    }
}