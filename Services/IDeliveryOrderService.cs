using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using acme_publishing_app.Models;

namespace acme_publishing_app.Services
{
    public interface IDeliveryOrderService
    {
        public Task<DeliveryOrder> GetDeliveryOrder(string id);
        public Task<List<DeliveryOrder>> GetDeliveryOrders();
        public Task<DeliveryOrder> PostDeliveryOrder(DeliveryOrder newDeliveryOrder);
        public Task<DeliveryOrder> PutDeliveryOrder(string id, DeliveryOrder newDeliveryOrder);
        public Task DeleteDeliveryOrder(string id);
    }
}