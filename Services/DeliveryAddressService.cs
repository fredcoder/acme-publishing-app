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
    public class DeliveryAddressService : IDeliveryAddressService
    {
        public readonly WebApiContext _webApiContext;
        public DeliveryAddressService(WebApiContext context)
        {
            _webApiContext = context;
            _webApiContext.Database.EnsureCreated();
        }

        public async Task<DeliveryAddress> GetDeliveryAddress(string id)
        {
            var deliveryAddress = new DeliveryAddress();
            await Task.Run(() =>
            {
                deliveryAddress = _webApiContext.DeliveryAddresses.FirstOrDefault(p => p.Id == id);
            });
            return deliveryAddress;
        }

        public async Task<List<DeliveryAddress>> GetDeliveryAddresses()
        {
            var deliveryAddress = new List<DeliveryAddress>();
            await Task.Run(() =>
            {
                deliveryAddress = _webApiContext.DeliveryAddresses.ToList();
            });
            return deliveryAddress;
        }

        public async Task<DeliveryAddress> PostDeliveryAddress(DeliveryAddress deliveryAddress)
        {
            deliveryAddress.Id = Guid.NewGuid().ToString();
            await Task.Run(() =>
            {
                var newDeliveryAddress = _webApiContext.DeliveryAddresses.AddAsync(deliveryAddress);
            });
            _webApiContext.SaveChanges();

            return deliveryAddress;
        }

        public async Task<DeliveryAddress> PutDeliveryAddress(string id, DeliveryAddress newDeliveryAddress)
        {
            var deliveryAddress = new DeliveryAddress();
            await Task.Run(() =>
            {
                deliveryAddress = _webApiContext.DeliveryAddresses.FirstOrDefault(p => p.Id == id);
                deliveryAddress.Description = newDeliveryAddress.Description;
                deliveryAddress.CustomerId = newDeliveryAddress.CustomerId;
                deliveryAddress.CountryId = newDeliveryAddress.CountryId;
            });
            _webApiContext.SaveChanges();

            return deliveryAddress;
        }

        public async Task DeleteDeliveryAddress(string id)
        {
            var deliveryAddress = new DeliveryAddress();
            await Task.Run(() =>
            {
                deliveryAddress = _webApiContext.DeliveryAddresses.FirstOrDefault(p => p.Id == id);
                var oldDeliveryAddress = _webApiContext.DeliveryAddresses.Remove(deliveryAddress);
            });
            _webApiContext.SaveChanges();
        }
    }
}