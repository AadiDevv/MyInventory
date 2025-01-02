using InventoryApp.Models;
using InventoryApp.Models.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace InventoryApp.Controllers
{
    public class DashboardController : BaseController
    {

        private readonly ILogger<HomeController> _logger;
        private readonly MyInventoryContext _context;

        public DashboardController (ILogger<HomeController> logger, MyInventoryContext context)
        {
            _logger = logger;
            _context = context;
        }

        public  async Task<IActionResult> ProductTypeView()
        {
            // Récupérer UserId depuis HttpContext.Items
            var userId = HttpContext.Items["UserId"] as string;

            if (userId == null || !int.TryParse(userId, out int userIdInt))
            {
                return Unauthorized("Utilisateur non connecté ou UserId invalide.");
            }

            var productTypes = await _context.ProductTypes
                .Where(pt => pt.UserId == userIdInt) // Filtrer par UserId
                .Select(pt => new StatistiquesDTO
                {
                    Id = pt.Id,
                    Name = pt.Name,
                    TypeCount = pt.Categories.Count(),
                    TotalQT = pt.Categories.Sum(c => c.Products.Sum(pdt => pdt.Quantity)),

                    InStock = pt.Categories
                    .SelectMany(c => c.Products)
                    .Count(p => p.Quantity > 0), 

                    LastUpdate = pt.Categories.SelectMany( c => c.Products).Max(p => (DateTime?)p.CreatedAt),
                      TotalValue = pt.Categories
                    .SelectMany(c => c.Products)
                    .Sum(p => (decimal?)p.PriceSale * p.Quantity) ?? 0,

                }).ToListAsync();

            if (productTypes == null || !productTypes.Any())
            {
                _logger.LogWarning("⚠️ Aucun ProductType trouvé dans la base de données !");
            }
            else
            {
                // Log des valeurs
                foreach (var pt in productTypes)
                {
                    _logger.LogInformation($"Id: {pt.Id}, Name: {pt.Name}, TypeCount: {pt.TypeCount}, " +
                                           $"TotalQuantity: {pt.TotalQT}, InStock: {pt.InStock}, " +
                                           $"LastUpdate: {pt.LastUpdate}, TotalValue: {pt.TotalValue}");
                }

            }

            //ViewData["ProductTypes"] = productTypes;

            return View(productTypes);
        }
    }
}
