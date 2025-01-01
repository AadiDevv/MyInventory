using System.Diagnostics;
using InventoryApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace InventoryApp.Controllers
{
    public class HomeController : BaseController
    {
        private readonly ILogger<HomeController> _logger;
        private readonly MyInventoryContext _context;

        public HomeController(ILogger<HomeController> logger, MyInventoryContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }
        //public IActionResult Dashboard()
        //{
        //    var viewModel = new DashboardViewModel
        //    {

        //    }
        //    return View();
        //}
        public IActionResult AddProduct()
        {
            var categories = _context.Products.Select(el => new SelectListItem
            {
                Value = el.Id.ToString(),
                Text = el.Name,
            }).ToList();

            var suppliers = _context.Suppliers.Select(el => new SelectListItem
            {
                Value = el.Id.ToString(),
                Text = el.Name,
            }).ToList();


            ViewBag.Categories = categories;
            ViewBag.Suppliers = suppliers;
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
