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
            var Subscription = new Subscription();
            await Task.Run(() =>
            {
                Subscription = _webApiContext.Subscriptions.FirstOrDefault(p => p.Id == id);
            });
            return Subscription;
        }

        public async Task<List<Subscription>> GetSubscriptions()
        {
            var Subscription = new List<Subscription>();
            await Task.Run(() =>
            {
                Subscription = _webApiContext.Subscriptions.ToList();
            });
            return Subscription;
        }

        public async Task<Subscription> PostSubscription(Subscription Subscription)
        {
            Subscription.Id = Guid.NewGuid().ToString();
            await Task.Run(() =>
            {
                var newSubscription = _webApiContext.Subscriptions.AddAsync(Subscription);
            });
            _webApiContext.SaveChanges();

            return Subscription;
        }

        public async Task<Subscription> PutSubscription(string id, Subscription newSubscription)
        {
            var Subscription = new Subscription();
            await Task.Run(() =>
            {
                Subscription = _webApiContext.Subscriptions.FirstOrDefault(p => p.Id == id);
            });
            _webApiContext.SaveChanges();

            return Subscription;
        }

        public async Task DeleteSubscription(string id)
        {
            var Subscription = new Subscription();
            await Task.Run(() =>
            {
                Subscription = _webApiContext.Subscriptions.FirstOrDefault(p => p.Id == id);
                var oldSubscription = _webApiContext.Subscriptions.Remove(Subscription);
            });
            _webApiContext.SaveChanges();
        }
    }
}