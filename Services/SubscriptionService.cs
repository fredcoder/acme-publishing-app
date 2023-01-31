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
    public class SubscriptionService : ISubscriptionService
    {
        public readonly WebApiContext _webApiContext;
        public SubscriptionService(WebApiContext context)
        {
            _webApiContext = context;
            _webApiContext.Database.EnsureCreated();
        }

        public async Task<Subscription> GetSubscription(string id)
        {
            var subscription = new Subscription();
            await Task.Run(() =>
            {
                subscription = _webApiContext.Subscriptions.FirstOrDefault(p => p.Id == id);
            });
            return subscription;
        }

        public async Task<List<Subscription>> GetSubscriptions()
        {
            var subscription = new List<Subscription>();
            await Task.Run(() =>
            {
                subscription = _webApiContext.Subscriptions.ToList();
            });
            return subscription;
        }

        public async Task<Subscription> PostSubscription(Subscription subscription)
        {
            subscription.Id = Guid.NewGuid().ToString();
            await Task.Run(() =>
            {
                var newSubscription = _webApiContext.Subscriptions.AddAsync(subscription);
            });
            _webApiContext.SaveChanges();

            return subscription;
        }

        public async Task<Subscription> PutSubscription(string id, Subscription newSubscription)
        {
            var subscription = new Subscription();
            await Task.Run(() =>
            {
                subscription = _webApiContext.Subscriptions.FirstOrDefault(p => p.Id == id);
                subscription.Name = newSubscription.Name;
            });
            _webApiContext.SaveChanges();

            return subscription;
        }

        public async Task DeleteSubscription(string id)
        {
            var subscription = new Subscription();
            await Task.Run(() =>
            {
                subscription = _webApiContext.Subscriptions.FirstOrDefault(p => p.Id == id);
                var oldSubscription = _webApiContext.Subscriptions.Remove(subscription);
            });
            _webApiContext.SaveChanges();
        }
    }
}