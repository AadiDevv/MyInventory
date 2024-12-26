using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using InventoryApp.Models;

namespace InventoryApp.Controllers_API
{
    [Route("api/[controller]")]
    [ApiController]
    public class StockEntryController : ControllerBase
    {
        private readonly MyInventoryContext _context;

        public StockEntryController(MyInventoryContext context)
        {
            _context = context;
        }

        // GET: api/StockEntry
        [HttpGet]
        public async Task<ActionResult<IEnumerable<StockEntry>>> GetStockEntries()
        {
            return await _context.StockEntries.ToListAsync();
        }

        // GET: api/StockEntry/5
        [HttpGet("{id}")]
        public async Task<ActionResult<StockEntry>> GetStockEntry(int id)
        {
            var stockEntry = await _context.StockEntries.FindAsync(id);

            if (stockEntry == null)
            {
                return NotFound();
            }

            return stockEntry;
        }

        // PUT: api/StockEntry/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutStockEntry(int id, StockEntry stockEntry)
        {
            if (id != stockEntry.Id)
            {
                return BadRequest();
            }

            _context.Entry(stockEntry).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StockEntryExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/StockEntry
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<StockEntry>> PostStockEntry(StockEntry stockEntry)
        {
            _context.StockEntries.Add(stockEntry);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetStockEntry", new { id = stockEntry.Id }, stockEntry);
        }

        // DELETE: api/StockEntry/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStockEntry(int id)
        {
            var stockEntry = await _context.StockEntries.FindAsync(id);
            if (stockEntry == null)
            {
                return NotFound();
            }

            _context.StockEntries.Remove(stockEntry);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool StockEntryExists(int id)
        {
            return _context.StockEntries.Any(e => e.Id == id);
        }
    }
}
