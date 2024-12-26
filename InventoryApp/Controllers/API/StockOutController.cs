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
    public class StockOutController : ControllerBase
    {
        private readonly MyInventoryContext _context;

        public StockOutController(MyInventoryContext context)
        {
            _context = context;
        }

        // GET: api/StockOut
        [HttpGet]
        public async Task<ActionResult<IEnumerable<StockOut>>> GetStockOuts()
        {
            return await _context.StockOuts.ToListAsync();
        }

        // GET: api/StockOut/5
        [HttpGet("{id}")]
        public async Task<ActionResult<StockOut>> GetStockOut(int id)
        {
            var stockOut = await _context.StockOuts.FindAsync(id);

            if (stockOut == null)
            {
                return NotFound();
            }

            return stockOut;
        }

        // PUT: api/StockOut/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutStockOut(int id, StockOut stockOut)
        {
            if (id != stockOut.Id)
            {
                return BadRequest();
            }

            _context.Entry(stockOut).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StockOutExists(id))
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

        // POST: api/StockOut
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<StockOut>> PostStockOut(StockOut stockOut)
        {
            _context.StockOuts.Add(stockOut);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetStockOut", new { id = stockOut.Id }, stockOut);
        }

        // DELETE: api/StockOut/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStockOut(int id)
        {
            var stockOut = await _context.StockOuts.FindAsync(id);
            if (stockOut == null)
            {
                return NotFound();
            }

            _context.StockOuts.Remove(stockOut);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool StockOutExists(int id)
        {
            return _context.StockOuts.Any(e => e.Id == id);
        }
    }
}
