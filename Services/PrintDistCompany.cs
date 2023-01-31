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
            var PrintDistCompany = new PrintDistCompany();
            await Task.Run(() =>
            {
                PrintDistCompany = _webApiContext.PrintDistCompanies.FirstOrDefault(p => p.Id == id);
            });
            return PrintDistCompany;
        }

        public async Task<List<PrintDistCompany>> GetPrintDistCompanies()
        {
            var PrintDistCompany = new List<PrintDistCompany>();
            await Task.Run(() =>
            {
                PrintDistCompany = _webApiContext.PrintDistCompanies.ToList();
            });
            return PrintDistCompany;
        }

        public async Task<PrintDistCompany> PostPrintDistCompany(PrintDistCompany PrintDistCompany)
        {
            PrintDistCompany.Id = Guid.NewGuid().ToString();
            await Task.Run(() =>
            {
                var newPrintDistCompany = _webApiContext.PrintDistCompanies.AddAsync(PrintDistCompany);
            });
            _webApiContext.SaveChanges();

            return PrintDistCompany;
        }

        public async Task<PrintDistCompany> PutPrintDistCompany(string id, PrintDistCompany newPrintDistCompany)
        {
            var PrintDistCompany = new PrintDistCompany();
            await Task.Run(() =>
            {
                PrintDistCompany = _webApiContext.PrintDistCompanies.FirstOrDefault(p => p.Id == id);
            });
            _webApiContext.SaveChanges();

            return PrintDistCompany;
        }

        public async Task DeletePrintDistCompany(string id)
        {
            var PrintDistCompany = new PrintDistCompany();
            await Task.Run(() =>
            {
                PrintDistCompany = _webApiContext.PrintDistCompanies.FirstOrDefault(p => p.Id == id);
                var oldPrintDistCompany = _webApiContext.PrintDistCompanies.Remove(PrintDistCompany);
            });
            _webApiContext.SaveChanges();
        }
    }
}