using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using acme_publishing_app.Models;

namespace acme_publishing_app.Services
{
    public interface ICountryService
    {
        public Task<Country> GetCountry(string id);
        public Task<List<Country>> GetCountries();
        public Task<Country> PostCountry(Country newCountry);
        public Task<Country> PutCountry(string id, Country newCountry);
        public Task DeleteCountry(string id);
    }
}