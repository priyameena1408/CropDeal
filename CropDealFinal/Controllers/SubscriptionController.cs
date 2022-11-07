using CaseStudy.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Scaffolding.Metadata;

namespace CaseStudy.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubscriptionController : ControllerBase
    {
        DatabaseContext _context;
        public SubscriptionController(DatabaseContext context)
        {
            _context = context;
        }

       // [Authorize(Roles ="Dealer")]
        [HttpPost("addSubs/{uid}")]
        public async Task<ActionResult> AddSubs(int uid,[FromBody]int cid)
        {
            var subs = await _context.Subscriptions
                .SingleOrDefaultAsync(p => p.CropTypeId == cid && p.DealerId == uid);
            if (subs == null)
            {
                var sub = new Subscription()
                {
                    DealerId = uid,
                    CropTypeId = cid
                };
                _context.Subscriptions.Add(sub);
                await _context.SaveChangesAsync();
                return Ok();
            }
            else
            {
                return BadRequest("Already Subscribed");
            }
        }
        [HttpDelete("deleteSubs/{uid}")]
        public async Task<ActionResult> DeleteSubs(int uid, [FromBody] int cid)
        {
            if (cid > 3 || cid < 1) return BadRequest("Invalid Sub");
            var subs = await _context.Subscriptions
                .SingleOrDefaultAsync(p => p.CropTypeId == cid && p.DealerId == uid);
            if(subs == null)
            {
                return BadRequest("Not Subscribed");
            }
            else
            {
                _context.Subscriptions.Remove(subs);
                await _context.SaveChangesAsync();
                return Ok();
            }
            
        }
        [HttpGet("getSubs/{uid}")]
        public async Task<ActionResult<IEnumerable<Subscription>>> GetSubByUser(int uid)
        {
            var subs = await _context.Subscriptions
                .Where(p => p.DealerId == uid)
                .ToListAsync();
            if (subs == null)
            {
                return null;
            }
            return subs;
        }
    }
}
