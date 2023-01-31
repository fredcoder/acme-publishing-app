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

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // List<ProductType> productTypes = new List<ProductType>();
            // productTypes.Add(new ProductType { Id = "b00db9eb-7650-4878-b814-8a96d5a8220e", Name = "Books" });
            // productTypes.Add(new ProductType { Id = "f30e74cd-2494-4fc8-8eb3-8de05c4a821e", Name = "Electronics" });
            // productTypes.Add(new ProductType { Id = "070e30e7-488b-47e3-ad28-4379b9be6185", Name = "Food" });
            // productTypes.Add(new ProductType { Id = "74ca9e4c-2f51-4bfb-8ee0-12efe0db187f", Name = "Furniture" });
            // productTypes.Add(new ProductType { Id = "d6d124a9-df5b-4ae8-848d-d15bb33bbd19", Name = "Toys" });

            // builder.Entity<ProductType>().ToTable("ProductType").HasKey(p => p.Id);
            // builder.Entity<ProductType>().ToTable("ProductType").HasData(productTypes);

            // List<Product> products = new List<Product>();
            // products.Add(new Product { Id = "50909b94-5134-4392-be80-13bcccd2086c", Name = "Don Quixote", Price = 12.00F, ProductTypeId = productTypes[0].Id, IsActive = true });
            // products.Add(new Product { Id = "7a852c93-ca4f-4ab5-84ac-c8b70927506e", Name = "Microwave", Price = 30.00F, ProductTypeId = productTypes[1].Id, IsActive = true });
            // products.Add(new Product { Id = "7caba613-eaa2-4c83-9d47-97b438169f95", Name = "Pizza", Price = 15.00F, ProductTypeId = productTypes[2].Id, IsActive = true });
            // products.Add(new Product { Id = "cf994cb1-dc3c-4822-93f7-a7422a016ca9", Name = "Chair", Price = 25.00F, ProductTypeId = productTypes[3].Id, IsActive = true });
            // products.Add(new Product { Id = "f6095b43-57f1-49a7-9acf-ac3d145fef4f", Name = "Lego", Price = 30.00F, ProductTypeId = productTypes[4].Id, IsActive = true });


            // builder.Entity<Product>().ToTable("Product").HasKey(p => p.Id);
            // builder.Entity<Product>().ToTable("Product").HasData(products);

            // builder.Entity<Product>().HasOne<ProductType>("ProductType");

        }

    }
}