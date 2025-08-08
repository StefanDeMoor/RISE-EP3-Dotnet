using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Rise.Domain.AmountItems;
using Rise.Persistence;

namespace Rise.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AmountController : ControllerBase
    {
        private readonly AppDbContext _context;

        public AmountController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Amount
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AmountItem>>> GetAllAmounts()
        {
            return await _context.AmountItems
                .Include(a => a.SubAmounts)
                .ToListAsync();
        }

        // GET: api/Amount/overview/5
        [HttpGet("overview/{overviewId}")]
        public async Task<ActionResult<IEnumerable<AmountItem>>> GetAmountsByOverview(int overviewId)
        {
            var amounts = await _context.AmountItems
                .Where(a => a.OverviewId == overviewId && a.ParentAmountItemId == null)
                .Include(a => a.SubAmounts)
                .ToListAsync();

            return Ok(amounts);
        }

        // GET: api/Amount/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AmountItem>> GetAmountItem(int id)
        {
            var amountItem = await _context.AmountItems
                .Include(a => a.SubAmounts)
                .FirstOrDefaultAsync(a => a.Id == id);

            if (amountItem == null)
                return NotFound();

            return amountItem;
        }

        // POST: api/Amount
        [HttpPost]
        public async Task<IActionResult> AddAmountItem(AmountItem amountItem)
        {
            _context.AmountItems.Add(amountItem);
            await _context.SaveChangesAsync();
            return Ok(amountItem);
        }

        // PUT: api/Amount/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAmountItem(int id, AmountItem amountItem)
        {
            if (id != amountItem.Id)
                return BadRequest();

            _context.Entry(amountItem).State = EntityState.Modified;

            foreach (var sub in amountItem.SubAmounts)
            {
                if (_context.Entry(sub).State == EntityState.Detached)
                    _context.Entry(sub).State = EntityState.Modified;
            }

            await _context.SaveChangesAsync();
            return NoContent();
        }

        // DELETE: api/Amount/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAmountItem(int id)
        {
            var item = await _context.AmountItems
                .Include(a => a.SubAmounts)
                .FirstOrDefaultAsync(a => a.Id == id);

            if (item == null)
                return NotFound();

            _context.AmountItems.Remove(item);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
