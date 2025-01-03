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
    public class CopieProductController : ControllerBase
    {
        private readonly MyInventoryContext _context;

        public CopieProductController(MyInventoryContext context)
        {
            _context = context;
        }

        // GET: api/Product
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            return await _context.Products.ToListAsync();
        }

        // GET: api/Product/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            var product = await _context.Products.FindAsync(id);

            if (product == null)
            {
                return NotFound();
            }

            return product;
        }

        // PUT: api/Product/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProduct(int id, Product product)
        {
            if (id != product.Id)
            {
                return BadRequest();
            }

            _context.Entry(product).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductExists(id))
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

        // POST: api/Product
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Product>> PostProduct(PostProductRequest request)
        {
            try
            {
                var existingProduct = await _context.Products.FirstOrDefaultAsync(el => el.Name.ToLower() == request.NewProductName.ToLower());

                if (existingProduct != null)
                {
                    existingProduct.Quantity += request.Quantity;
                    await _context.SaveChangesAsync();

                    return Ok(new
                    {
                        success = true,
                        message = "Product quantity updated.",
                        data = existingProduct
                    });


                }else
                {
                    return Ok(new
                    {
                        success = true,
                        message = "Product quantity updated.",
                        data = existingProduct
                    });
                }

                //var product = new Product
                //{
                //    Name = request.NewProductName,
                //    Quantity = request.Quantity, // Ajoutez la quantit� initiale
                //    CategoryId = request.CategoryId,
                //    SupplierId = request.SupplierId,
                //    Description = request.Description

                //};

                //_context.Products.Add(product);
                //await _context.SaveChangesAsync();

                //var category = _context.Categories.FindAsync(product.CategoryId);
                //category.ProductCount = await _context.Products.CountAsync(p => p.CategoryId == product.CategoryId);
                //await _.SaveChangesAsync();

                //return CreatedAtAction("GetCategory", new { id = product.Id }, new
                //{
                //    success = true,
                //    message = "Product created successfully.",
                //    data = product
                //});
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    success = false,
                    message = "Error catched",
                    error = ex.Message,
                    stackTrace = ex.StackTrace
                });
            }
            

        }

        // DELETE: api/Product/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProductExists(int id)
        {
            return _context.Products.Any(e => e.Id == id);
        }
    }
}
