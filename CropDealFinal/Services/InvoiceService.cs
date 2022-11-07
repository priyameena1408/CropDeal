using CaseStudy.Dtos;
using CaseStudy.Models;
using CaseStudy.Repository;
using Microsoft.AspNetCore.Mvc;

namespace CaseStudy.Services
{
    public class InvoiceService
    {
        InvoiceRepository _repo;
        public InvoiceService(InvoiceRepository repository)
        {
            _repo = repository;
        }
        public async Task<ActionResult<Invoice>> CreateInvoice(InvoiceDto data)
        {
            return await _repo.CreateInvoice(data);
        }

        public async Task<IEnumerable<FarmerReceipt>> FarmerInvoices(int uid)
        {
            return await _repo.FarmerInvoices(uid);
        }
        public async Task<IEnumerable<FarmerReceipt>> DealerInvoices(int did)
        {
            return await _repo.DealerInvoices(did);
        }
    }
}
