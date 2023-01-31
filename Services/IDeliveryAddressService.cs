using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using acme_publishing_app.Models;

namespace acme_publishing_app.Services
{
    public interface IDeliveryAddressService
    {
        public Task<DeliveryAddress> GetDeliveryAddress(string id);
        public Task<List<DeliveryAddress>> GetDeliveryAddresses();
        public Task<DeliveryAddress> PostDeliveryAddress(DeliveryAddress newDeliveryAddress);
        public Task<DeliveryAddress> PutDeliveryAddress(string id, DeliveryAddress newDeliveryAddress);
        public Task DeleteDeliveryAddress(string id);
    }
}