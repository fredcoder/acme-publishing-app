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
    public class PrintDistCompanyService : IPrintDistCompanyService
    {
        public readonly WebApiContext _webApiContext;
        public PrintDistCompanyService(WebApiContext context)
        {
            _webApiContext = context;
            _webApiContext.Database.EnsureCreated();
        }

        public async Task<PrintDistCompany> GetPrintDistCompany(string id)
        {
            var printDistCompany = new PrintDistCompany();
            await Task.Run(() =>
            {
                printDistCompany = _webApiContext.PrintDistCompanies.FirstOrDefault(p => p.Id == id);
            });
            return printDistCompany;
        }

        public async Task<List<PrintDistCompany>> GetPrintDistCompanies()
        {
            var printDistCompany = new List<PrintDistCompany>();
            await Task.Run(() =>
            {
                printDistCompany = _webApiContext.PrintDistCompanies.ToList();
            });
            return printDistCompany;
        }

        public async Task<PrintDistCompany> PostPrintDistCompany(PrintDistCompany printDistCompany)
        {
            printDistCompany.Id = Guid.NewGuid().ToString();
            await Task.Run(() =>
            {
                var newPrintDistCompany = _webApiContext.PrintDistCompanies.AddAsync(printDistCompany);
            });
            _webApiContext.SaveChanges();

            return printDistCompany;
        }

        public async Task<PrintDistCompany> PutPrintDistCompany(string id, PrintDistCompany newPrintDistCompany)
        {
            var printDistCompany = new PrintDistCompany();
            await Task.Run(() =>
            {
                printDistCompany = _webApiContext.PrintDistCompanies.FirstOrDefault(p => p.Id == id);
                printDistCompany.Name = newPrintDistCompany.Name;
            });
            _webApiContext.SaveChanges();

            return printDistCompany;
        }

        public async Task DeletePrintDistCompany(string id)
        {
            var printDistCompany = new PrintDistCompany();
            await Task.Run(() =>
            {
                printDistCompany = _webApiContext.PrintDistCompanies.FirstOrDefault(p => p.Id == id);
                var oldPrintDistCompany = _webApiContext.PrintDistCompanies.Remove(printDistCompany);
            });
            _webApiContext.SaveChanges();
        }
    }
}