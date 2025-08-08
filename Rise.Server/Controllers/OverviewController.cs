using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Rise.Domain.Overviews;
using Rise.Persistence;

namespace Rise.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OverviewController : ControllerBase
    {
        private readonly AppDbContext _context;

        public OverviewController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Overview
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Overview>>> GetOverviews()
        {
            return await _context.Overviews
                .Include(o => o.Amounts)
                .ThenInclude(a => a.SubAmounts)
                .ToListAsync();
        }

        // GET: api/Overview/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Overview>> GetOverview(int id)
        {
            var overview = await _context.Overviews
                .Include(o => o.Amounts)
                .ThenInclude(a => a.SubAmounts)
                .FirstOrDefaultAsync(o => o.Id == id);

            if (overview == null)
                return NotFound();

            return overview;
        }

        // POST: api/Overview
        [HttpPost]
        public async Task<IActionResult> AddOverview(Overview overview)
        {
            _context.Overviews.Add(overview);
            await _context.SaveChangesAsync();
            return Ok();
        }

        // PUT: api/Overview/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateOverview(int id, Overview overview)
        {
            if (id != overview.Id)
                return BadRequest();

            _context.Entry(overview).State = EntityState.Modified;

            // Also update Amounts and SubAmounts
            foreach (var amount in overview.Amounts)
            {
                if (_context.Entry(amount).State == EntityState.Detached)
                    _context.Entry(amount).State = EntityState.Modified;

                foreach (var sub in amount.SubAmounts)
                {
                    if (_context.Entry(sub).State == EntityState.Detached)
                        _context.Entry(sub).State = EntityState.Modified;
                }
            }

            await _context.SaveChangesAsync();
            return NoContent();
        }

        // PUT: api/Overview/5/totalincome
        [HttpPut("{id}/totalIncome")]
        public async Task<IActionResult> UpdateTotalIncome(int id, [FromBody] double newTotalIncome)
        {
            var overview = await _context.Overviews.FindAsync(id);

            if (overview == null)
                return NotFound();

            overview.TotalIncome = newTotalIncome;

            _context.Entry(overview).Property(o => o.TotalIncome).IsModified = true;

            await _context.SaveChangesAsync();

            return NoContent();
        }


        // DELETE: api/Overview/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOverview(int id)
        {
            var overview = await _context.Overviews
                .Include(o => o.Amounts)
                .ThenInclude(a => a.SubAmounts)
                .FirstOrDefaultAsync(o => o.Id == id);

            if (overview == null)
                return NotFound();

            _context.Overviews.Remove(overview);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
