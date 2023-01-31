using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using acme_publishing_app.Models;

namespace acme_publishing_app.Services
{
    public interface ISubscriptionService
    {
        public Task<Subscription> GetSubscription(string id);
        public Task<List<Subscription>> GetSubscriptions();
        public Task<Subscription> PostSubscription(Subscription newSubscription);
        public Task<Subscription> PutSubscription(string id, Subscription newSubscription);
        public Task DeleteSubscription(string id);
    }
}