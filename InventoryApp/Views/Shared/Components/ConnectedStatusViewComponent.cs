using Microsoft.AspNetCore.Mvc;

namespace InventoryApp.Views.Shared.Components
{
    public class ConnectedStatusViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            ViewData["IsConnected"] = HttpContext.Items["IsConnected"];
            ViewData["UserName"] = HttpContext.Items["UserName"];
            Console.WriteLine("User name = " + ViewData["UserName"]);
            return Content(string.Empty); // Retour vide, mais valide
        }
    }
}
