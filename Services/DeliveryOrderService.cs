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
            var deliveryOrder = new DeliveryOrder();
            await Task.Run(() =>
            {
                deliveryOrder = _webApiContext.DeliveryOrders
                .Include(d => d.Subscription)
                .Include(d => d.PrintDistCompany)
                .Include(d => d.DeliveryAddress)
                .Include(d => d.DeliveryAddress.Customer)
                .Include(d => d.DeliveryAddress.Country)
                .FirstOrDefault(p => p.Id == id);
            });
            return deliveryOrder;
        }

        public async Task<List<DeliveryOrder>> GetDeliveryOrders()
        {
            var deliveryOrder = new List<DeliveryOrder>();
            await Task.Run(() =>
            {
                deliveryOrder = _webApiContext.DeliveryOrders
                .Include(d => d.Subscription)
                .Include(d => d.PrintDistCompany)
                .Include(d => d.DeliveryAddress)
                .Include(d => d.DeliveryAddress.Customer)
                .Include(d => d.DeliveryAddress.Country)
                .ToList();
            });
            return deliveryOrder;
        }

        public async Task<DeliveryOrder> PostDeliveryOrder(DeliveryOrder deliveryOrder)
        {
            deliveryOrder.Id = Guid.NewGuid().ToString();
            await Task.Run(() =>
                {
                    var newDeliveryOrder = _webApiContext.DeliveryOrders.AddAsync(deliveryOrder);
                });
            _webApiContext.SaveChanges();

            return deliveryOrder;
        }

        public async Task<DeliveryOrder> PutDeliveryOrder(string id, DeliveryOrder newDeliveryOrder)
        {
            var deliveryOrder = new DeliveryOrder();
            await Task.Run(() =>
            {
                deliveryOrder = _webApiContext.DeliveryOrders.FirstOrDefault(p => p.Id == id);
                deliveryOrder.DeliveryAddressId = newDeliveryOrder.DeliveryAddressId;
                deliveryOrder.SubscriptionId = newDeliveryOrder.SubscriptionId;
            });
            _webApiContext.SaveChanges();

            return deliveryOrder;
        }

        public async Task DeleteDeliveryOrder(string id)
        {
            var deliveryOrder = new DeliveryOrder();
            await Task.Run(() =>
            {
                deliveryOrder = _webApiContext.DeliveryOrders.FirstOrDefault(p => p.Id == id);
                var oldDeliveryOrder = _webApiContext.DeliveryOrders.Remove(deliveryOrder);
            });
            _webApiContext.SaveChanges();
        }

    }
}