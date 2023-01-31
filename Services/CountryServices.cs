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
    public class CountryService : ICountryService
    {
        public readonly WebApiContext _webApiContext;
        public CountryService(WebApiContext context)
        {
            _webApiContext = context;
            _webApiContext.Database.EnsureCreated();
        }

        public async Task<Country> GetCountry(string id)
        {
            var Country = new Country();
            await Task.Run(() =>
            {
                Country = _webApiContext.Countries.FirstOrDefault(p => p.Id == id);
            });
            return Country;
        }

        public async Task<List<Country>> GetCountries()
        {
            var Country = new List<Country>();
            await Task.Run(() =>
            {
                Country = _webApiContext.Countries.ToList();
            });
            return Country;
        }

        public async Task<Country> PostCountry(Country Country)
        {
            Country.Id = Guid.NewGuid().ToString();
            await Task.Run(() =>
            {
                var newCountry = _webApiContext.Countries.AddAsync(Country);
            });
            _webApiContext.SaveChanges();

            return Country;
        }

        public async Task<Country> PutCountry(string id, Country newCountry)
        {
            var Country = new Country();
            await Task.Run(() =>
            {
                Country = _webApiContext.Countries.FirstOrDefault(p => p.Id == id);
            });
            _webApiContext.SaveChanges();

            return Country;
        }

        public async Task DeleteCountry(string id)
        {
            var Country = new Country();
            await Task.Run(() =>
            {
                Country = _webApiContext.Countries.FirstOrDefault(p => p.Id == id);
                var oldCountry = _webApiContext.Countries.Remove(Country);
            });
            _webApiContext.SaveChanges();
        }
    }
}