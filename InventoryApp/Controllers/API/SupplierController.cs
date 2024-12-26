using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using InventoryApp.Models;
using InventoryApp.Models.DTOs;

namespace InventoryApp.Controllers_API
{
    [Route("api/[controller]")]
    [ApiController]
    public class SupplierController : ControllerBase
    {
        private readonly MyInventoryContext _context;

        public SupplierController(MyInventoryContext context)
        {
            _context = context;
        }

        // GET: api/Supplier
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Supplier>>> GetSuppliers()
        {
            return await _context.Suppliers.ToListAsync();
        }

        // GET: api/Supplier/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Supplier>> GetSupplier(int id)
        {
            var supplier = await _context.Suppliers.FindAsync(id);

            if (supplier == null)
            {
                return NotFound();
            }

            return supplier;
        }

        // GET: api/Category?Query
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Supplier>>> GetSupplierById([FromQuery] int ProductTypeId)
        {
            var suppliers = await _context.Suppliers
                .Where(s => s.ProductTypeId == ProductTypeId)
                .ToListAsync();

            return Ok(suppliers);
        }


        // PUT: api/Supplier/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSupplier(int id, Supplier supplier)
        {
            if (id != supplier.Id)
            {
                return BadRequest();
            }

            _context.Entry(supplier).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SupplierExists(id))
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
        // POST: api/Supplier
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Supplier>> PostSupplier(PostSupplierRequest request)
        {
            try
            {
                // Check if supplier with same name exists
                var existingSupplier = await _context.Suppliers.FirstOrDefaultAsync(c => c.Name.ToLower() == request.NewSupplierName.ToLower());


                if (existingSupplier != null)
                {
                    return BadRequest(new
                    {
                        success = false,
                        message = "Supplier already exists."
                    });
                }

                // Add supplier to database
                var supplier = new Supplier { Name = request.NewSupplierName };
                _context.Suppliers.Add(supplier);
                await _context.SaveChangesAsync();

                return Ok(new
                {
                    success = true,
                    id = supplier.Id,
                    name = supplier.Name,
                    message = supplier.Name + " added with succes."

                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    success = false,
                    message = "An error occurred.",
                    details = ex.Message
                });
            }
        }


        // DELETE: api/Supplier/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSupplier(int id)
        {
            var supplier = await _context.Suppliers.FindAsync(id);
            if (supplier == null)
            {
                return NotFound();
            }

            _context.Suppliers.Remove(supplier);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SupplierExists(int id)
        {
            return _context.Suppliers.Any(e => e.Id == id);
        }
    }
}
