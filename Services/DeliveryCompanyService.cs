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
    public class DeliveryCompanyService : IDeliveryCompanyService
    {
        public readonly WebApiContext _webApiContext;
        public DeliveryCompanyService(WebApiContext context)
        {
            _webApiContext = context;
            _webApiContext.Database.EnsureCreated();
        }

        public async Task<string> GetDeliveryCompanyId(string subscriptionId, string countryId)
        {
            var deliveryCompany = new DeliveryCompany();
            await Task.Run(() =>
            {
                deliveryCompany = _webApiContext.DeliveryCompanies
                .FirstOrDefault(p => p.SubscriptionId == subscriptionId && p.CountryId == countryId);
            });
            return deliveryCompany.PrintDistCompanyId;
        }
        public async Task<DeliveryCompany> GetDeliveryCompany(string subscriptionId, string countryId)
        {
            var deliveryCompany = new DeliveryCompany();
            await Task.Run(() =>
            {
                deliveryCompany = _webApiContext.DeliveryCompanies
                .FirstOrDefault(p => p.SubscriptionId == subscriptionId && p.CountryId == countryId);
            });
            return deliveryCompany;
        }

        public async Task<List<DeliveryCompany>> GetDeliveryCompanies()
        {
            var deliveryCompany = new List<DeliveryCompany>();
            await Task.Run(() =>
            {
                deliveryCompany = _webApiContext.DeliveryCompanies
                .ToList();
            });
            return deliveryCompany;
        }

        public async Task<DeliveryCompany> PostDeliveryCompany(DeliveryCompany deliveryCompany)
        {
            await Task.Run(() =>
            {
                var newDeliveryCompany = _webApiContext.DeliveryCompanies.AddAsync(deliveryCompany);
            });
            _webApiContext.SaveChanges();

            return deliveryCompany;
        }

        public async Task<DeliveryCompany> PutDeliveryCompany(string subscriptionId, string countryId, DeliveryCompany newDeliveryCompany)
        {
            var deliveryCompany = new DeliveryCompany();
            await Task.Run(() =>
            {
                deliveryCompany = _webApiContext.DeliveryCompanies.FirstOrDefault(p => p.SubscriptionId == subscriptionId && p.CountryId == countryId);
                deliveryCompany.PrintDistCompanyId = newDeliveryCompany.PrintDistCompanyId;
            });
            _webApiContext.SaveChanges();

            return deliveryCompany;
        }

        public async Task DeleteDeliveryCompany(string subscriptionId, string countryId)
        {
            var deliveryCompany = new DeliveryCompany();
            await Task.Run(() =>
            {
                deliveryCompany = _webApiContext.DeliveryCompanies.FirstOrDefault(p => p.SubscriptionId == subscriptionId && p.CountryId == countryId);
                var oldDeliveryCompany = _webApiContext.DeliveryCompanies.Remove(deliveryCompany);
            });
            _webApiContext.SaveChanges();
        }
    }
}