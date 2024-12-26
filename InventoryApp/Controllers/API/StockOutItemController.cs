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
    public class StockOutItemController : ControllerBase
    {
        private readonly MyInventoryContext _context;

        public StockOutItemController(MyInventoryContext context)
        {
            _context = context;
        }

        // GET: api/StockOutItem
        [HttpGet]
        public async Task<ActionResult<IEnumerable<StockOutItem>>> GetStockOutItems()
        {
            return await _context.StockOutItems.ToListAsync();
        }

        // GET: api/StockOutItem/5
        [HttpGet("{id}")]
        public async Task<ActionResult<StockOutItem>> GetStockOutItem(int id)
        {
            var stockOutItem = await _context.StockOutItems.FindAsync(id);

            if (stockOutItem == null)
            {
                return NotFound();
            }

            return stockOutItem;
        }


        // PUT: api/StockOutItem/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutStockOutItem(int id, StockOutItem stockOutItem)
        {
            if (id != stockOutItem.Id)
            {
                return BadRequest();
            }

            _context.Entry(stockOutItem).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StockOutItemExists(id))
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

        // POST: api/StockOutItem
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<StockOutItem>> PostStockOutItem(StockOutItem stockOutItem)
        {
            _context.StockOutItems.Add(stockOutItem);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetStockOutItem", new { id = stockOutItem.Id }, stockOutItem);
        }

        // DELETE: api/StockOutItem/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStockOutItem(int id)
        {
            var stockOutItem = await _context.StockOutItems.FindAsync(id);
            if (stockOutItem == null)
            {
                return NotFound();
            }

            _context.StockOutItems.Remove(stockOutItem);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool StockOutItemExists(int id)
        {
            return _context.StockOutItems.Any(e => e.Id == id);
        }
    }
}
