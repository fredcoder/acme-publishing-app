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
            var country = new Country();
            await Task.Run(() =>
            {
                country = _webApiContext.Countries.FirstOrDefault(p => p.Id == id);
            });
            return country;
        }

        public async Task<List<Country>> GetCountries()
        {
            var country = new List<Country>();
            await Task.Run(() =>
            {
                country = _webApiContext.Countries.ToList();
            });
            return country;
        }

        public async Task<Country> PostCountry(Country country)
        {
            country.Id = Guid.NewGuid().ToString();
            await Task.Run(() =>
            {
                var newCountry = _webApiContext.Countries.AddAsync(country);
            });
            _webApiContext.SaveChanges();

            return country;
        }

        public async Task<Country> PutCountry(string id, Country newCountry)
        {
            var country = new Country();
            await Task.Run(() =>
            {
                country = _webApiContext.Countries.FirstOrDefault(p => p.Id == id);
                country.Name = newCountry.Name;
            });
            _webApiContext.SaveChanges();

            return country;
        }

        public async Task DeleteCountry(string id)
        {
            var country = new Country();
            await Task.Run(() =>
            {
                country = _webApiContext.Countries.FirstOrDefault(p => p.Id == id);
                var oldCountry = _webApiContext.Countries.Remove(country);
            });
            _webApiContext.SaveChanges();
        }
    }
}