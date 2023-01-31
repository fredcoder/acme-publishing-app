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
            var DeliveryAddress = new DeliveryAddress();
            await Task.Run(() =>
            {
                DeliveryAddress = _webApiContext.DeliveryAddresses.FirstOrDefault(p => p.Id == id);
            });
            return DeliveryAddress;
        }

        public async Task<List<DeliveryAddress>> GetDeliveryAddresses()
        {
            var DeliveryAddress = new List<DeliveryAddress>();
            await Task.Run(() =>
            {
                DeliveryAddress = _webApiContext.DeliveryAddresses.ToList();
            });
            return DeliveryAddress;
        }

        public async Task<DeliveryAddress> PostDeliveryAddress(DeliveryAddress DeliveryAddress)
        {
            DeliveryAddress.Id = Guid.NewGuid().ToString();
            await Task.Run(() =>
            {
                var newDeliveryAddress = _webApiContext.DeliveryAddresses.AddAsync(DeliveryAddress);
            });
            _webApiContext.SaveChanges();

            return DeliveryAddress;
        }

        public async Task<DeliveryAddress> PutDeliveryAddress(string id, DeliveryAddress newDeliveryAddress)
        {
            var DeliveryAddress = new DeliveryAddress();
            await Task.Run(() =>
            {
                DeliveryAddress = _webApiContext.DeliveryAddresses.FirstOrDefault(p => p.Id == id);
            });
            _webApiContext.SaveChanges();

            return DeliveryAddress;
        }

        public async Task DeleteDeliveryAddress(string id)
        {
            var DeliveryAddress = new DeliveryAddress();
            await Task.Run(() =>
            {
                DeliveryAddress = _webApiContext.DeliveryAddresses.FirstOrDefault(p => p.Id == id);
                var oldDeliveryAddress = _webApiContext.DeliveryAddresses.Remove(DeliveryAddress);
            });
            _webApiContext.SaveChanges();
        }
    }
}