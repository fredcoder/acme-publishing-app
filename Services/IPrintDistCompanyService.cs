using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using acme_publishing_app.Models;

namespace acme_publishing_app.Services
{
    public interface IPrintDistCompanyService
    {
        public Task<PrintDistCompany> GetPrintDistCompany(string id);
        public Task<List<PrintDistCompany>> GetPrintDistCompanies();
        public Task<PrintDistCompany> PostPrintDistCompany(PrintDistCompany newPrintDistCompany);
        public Task<PrintDistCompany> PutPrintDistCompany(string id, PrintDistCompany newPrintDistCompany);
        public Task DeletePrintDistCompany(string id);
    }
}