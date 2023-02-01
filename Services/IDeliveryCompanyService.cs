using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using acme_publishing_app.Models;

namespace acme_publishing_app.Services
{
    public interface IDeliveryCompanyService
    {
        public Task<string> GetDeliveryCompanyId(string subscriptionId, string countryId);
        public Task<DeliveryCompany> GetDeliveryCompany(string subscriptionId, string countryId);
        public Task<List<DeliveryCompany>> GetDeliveryCompanies();
        public Task<DeliveryCompany> PostDeliveryCompany(DeliveryCompany newDeliveryCompany);
        public Task<DeliveryCompany> PutDeliveryCompany(string subscriptionId, string countryId, DeliveryCompany newDeliveryCompany);
        public Task DeleteDeliveryCompany(string subscriptionId, string countryId);
    }
}