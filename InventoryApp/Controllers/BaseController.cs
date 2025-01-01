using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

public class BaseController : Controller
{
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        if (HttpContext.Items["IsConnected"] is bool isConnected && isConnected)
        {
            ViewData["Connected"] = true;
            ViewData["Username"] = HttpContext.Items["UserName"] as string;
        }
        else
        {
            ViewData["Connected"] = false;
        }

        base.OnActionExecuting(context);
    }
}
