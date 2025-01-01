using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using InventoryApp.Models;
using InventoryApp.Models.DTOs;
using Azure.Core;

namespace InventoryApp.Controllers_API
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductTypeController : ControllerBase
    {
        private readonly MyInventoryContext _context;

        public ProductTypeController(MyInventoryContext context)
        {
            _context = context;
        }

        // GET: api/ProductType
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductType>>> GetProductTypes()
        {
            return await _context.ProductTypes.ToListAsync();
        }

        // GET: api/ProductType/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductType>> GetProductType(int id)
        {
            var productType = await _context.ProductTypes.FindAsync(id);

            if (productType == null)
            {
                return NotFound();
            }

            return productType;
        }

        [HttpGet("ByFilters")]
        public async Task<ActionResult<IEnumerable<ProductType>>> GetProductTypeById([FromQuery] int UserId)
        {
            var productTtype = await _context.ProductTypes
                .Where(c => c.UserId == UserId)
                .ToListAsync();

            return Ok(productTtype);
        }


        // PUT: api/ProductType/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProductType(int id, ProductType productType)
        {
            if (id != productType.Id)
            {
                return BadRequest();
            }

            _context.Entry(productType).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductTypeExists(id))
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

        // POST: api/ProductType
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ProductType>> PostProductType(PostProductTypeRequest request)
        {
            try
            {
                // Check if supplier with same name exists
                var existingPdtType = await _context.ProductTypes.FirstOrDefaultAsync(c => c.Name.ToLower() == request.Name.ToLower() && c.UserId == request.UserId);


                if (existingPdtType != null)
                {
                    return BadRequest(new
                    {
                        success = false,
                        message = "ProductType already exists."
                    });
                }

                // Add supplier to database
                var productType = new ProductType { Name = request.Name,
                    UserId = request.UserId
                };
                _context.ProductTypes.Add(productType);
                await _context.SaveChangesAsync();

                return Ok(new
                {
                    success = true,
                    id = productType.Id,
                    name = productType.Name,
                    message = productType.Name + " added with succes."

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

        // DELETE: api/ProductType/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProductType(int id)
        {
            var productType = await _context.ProductTypes.FindAsync(id);
            if (productType == null)
            {
                return NotFound();
            }

            _context.ProductTypes.Remove(productType);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProductTypeExists(int id)
        {
            return _context.ProductTypes.Any(e => e.Id == id);
        }
    }
}
