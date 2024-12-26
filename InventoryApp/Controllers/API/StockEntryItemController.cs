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
    public class StockEntryItemController : ControllerBase
    {
        private readonly MyInventoryContext _context;

        public StockEntryItemController(MyInventoryContext context)
        {
            _context = context;
        }

        // GET: api/StockEntryItem
        [HttpGet]
        public async Task<ActionResult<IEnumerable<StockEntryItem>>> GetStockEntryItems()
        {
            return await _context.StockEntryItems.ToListAsync();
        }

        // GET: api/StockEntryItem/5
        [HttpGet("{id}")]
        public async Task<ActionResult<StockEntryItem>> GetStockEntryItem(int id)
        {
            var stockEntryItem = await _context.StockEntryItems.FindAsync(id);

            if (stockEntryItem == null)
            {
                return NotFound();
            }

            return stockEntryItem;
        }

        // PUT: api/StockEntryItem/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutStockEntryItem(int id, StockEntryItem stockEntryItem)
        {
            if (id != stockEntryItem.Id)
            {
                return BadRequest();
            }

            _context.Entry(stockEntryItem).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StockEntryItemExists(id))
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

        // POST: api/StockEntryItem
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<StockEntryItem>> PostStockEntryItem(StockEntryItem stockEntryItem)
        {
            _context.StockEntryItems.Add(stockEntryItem);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetStockEntryItem", new { id = stockEntryItem.Id }, stockEntryItem);
        }

        // DELETE: api/StockEntryItem/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStockEntryItem(int id)
        {
            var stockEntryItem = await _context.StockEntryItems.FindAsync(id);
            if (stockEntryItem == null)
            {
                return NotFound();
            }

            _context.StockEntryItems.Remove(stockEntryItem);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool StockEntryItemExists(int id)
        {
            return _context.StockEntryItems.Any(e => e.Id == id);
        }
    }
}
