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
    public class DeliveryOrderService : IDeliveryOrderService
    {
        public readonly WebApiContext _webApiContext;
        public DeliveryOrderService(WebApiContext context)
        {
            _webApiContext = context;
            _webApiContext.Database.EnsureCreated();
        }

        public async Task<DeliveryOrder> GetDeliveryOrder(string id)
        {
            var DeliveryOrder = new DeliveryOrder();
            await Task.Run(() =>
            {
                DeliveryOrder = _webApiContext.DeliveryOrders.FirstOrDefault(p => p.Id == id);
            });
            return DeliveryOrder;
        }

        public async Task<List<DeliveryOrder>> GetDeliveryOrders()
        {
            var DeliveryOrder = new List<DeliveryOrder>();
            await Task.Run(() =>
            {
                DeliveryOrder = _webApiContext.DeliveryOrders.ToList();
            });
            return DeliveryOrder;
        }

        public async Task<DeliveryOrder> PostDeliveryOrder(DeliveryOrder DeliveryOrder)
        {
            DeliveryOrder.Id = Guid.NewGuid().ToString();
            await Task.Run(() =>
            {
                var newDeliveryOrder = _webApiContext.DeliveryOrders.AddAsync(DeliveryOrder);
            });
            _webApiContext.SaveChanges();

            return DeliveryOrder;
        }

        public async Task<DeliveryOrder> PutDeliveryOrder(string id, DeliveryOrder newDeliveryOrder)
        {
            var DeliveryOrder = new DeliveryOrder();
            await Task.Run(() =>
            {
                DeliveryOrder = _webApiContext.DeliveryOrders.FirstOrDefault(p => p.Id == id);
            });
            _webApiContext.SaveChanges();

            return DeliveryOrder;
        }

        public async Task DeleteDeliveryOrder(string id)
        {
            var DeliveryOrder = new DeliveryOrder();
            await Task.Run(() =>
            {
                DeliveryOrder = _webApiContext.DeliveryOrders.FirstOrDefault(p => p.Id == id);
                var oldDeliveryOrder = _webApiContext.DeliveryOrders.Remove(DeliveryOrder);
            });
            _webApiContext.SaveChanges();
        }
    }
}