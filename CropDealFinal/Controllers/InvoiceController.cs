using CaseStudy.Dtos;
using CaseStudy.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net.Mail;
using System.Net;
using System.Runtime.InteropServices;
using CaseStudy.Repository;
using System.Security.Cryptography;
using CaseStudy.Services;

namespace CaseStudy.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvoiceController : ControllerBase
    {
        DatabaseContext _context;
        InvoiceService _service;
        public InvoiceController(DatabaseContext context, InvoiceService service)
        {
            _context = context;
            _service = service;
        }

        [HttpPost("addInvoice")]
        [Authorize]
        public async Task<ActionResult<Invoice>> CreateInvoice(InvoiceDto data)
        {
            var invoice = await _service.CreateInvoice(data);

            if (invoice == null) return BadRequest();

            return invoice;
        }

        
        [HttpGet("farmerInvoices/{fid}")]
        public async Task<ActionResult<IEnumerable<FarmerReceipt>>> GetFarmerInvoices(int fid)
        {
            var invoices = await _service.FarmerInvoices(fid);
            if (invoices == null)
            {
                return NotFound();
            }
            else return Ok(invoices);
        }

        [HttpGet("dealerInvoices/{did}")]
        public async Task<ActionResult<IEnumerable<Invoice>>> getDealerInvoices(int did)
        {
            var invoices = await _service.DealerInvoices(did);
            if (invoices == null)
            {
                return NotFound();
            }
            else return Ok(invoices);
        }

        //[HttpGet("invoice/{id}")]
        //public async Task<ActionResult<FarmerReceipt>> GetInvoice(int id)
        //{
        //    var invoice = await _context.Invoices
        //        .SingleOrDefaultAsync(a => a.InvoiceId == id);

        //    if (invoice == null)
        //    {
        //        return NotFound();
        //    }

        //    var receipt = new FarmerReceipt();
        //    var dealer = _context.Users.Include("Account")
        //        .SingleOrDefault(a => a.UserId == invoice.DealerId);

        //    var farmer = _context.Users.Include("Account")
        //        .SingleOrDefault(a => a.UserId == invoice.FarmerId);

        //    var crop = _context.CropDetails.Include("CropType")
        //        .SingleOrDefault(a => a.CropId == invoice.CropId);

        //    receipt.DealerAccountNumber = dealer.Account.AccountNumber;
        //    receipt.AccountNumber =  farmer.Account.AccountNumber;
        //    receipt.CropName = crop.CropName;
        //    receipt.TypeName = crop.CropType.TypeName;
        //    receipt.QtyAvailable = crop.QtyAvailable;

        //    return receipt;
        //}


    }
}
