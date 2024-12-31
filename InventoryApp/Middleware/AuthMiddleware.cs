namespace InventoryApp.Middleware
{
    public class AuthMiddleware
    {
        private readonly RequestDelegate _next;

        public AuthMiddleware(RequestDelegate next) {  _next = next; }

        public async Task Invoke (HttpContext context)
        {
            var userId = context.Session.GetInt32("UserId");

            if (userId == null)
            {
                context.Items["UserId"] = userId;
            }

            await _next(context);

        }
    }
}
