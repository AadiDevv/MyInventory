namespace InventoryApp.Middleware
{
    public class AuthMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<AuthMiddleware> _logger;

        public AuthMiddleware(RequestDelegate next, ILogger<AuthMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            var userId = context.Session.GetString("UserId");
            var userName = context.Session.GetString("UserName");

            _logger.LogInformation($"[Middleware] UserId from session: {userId}, UserName: {userName}");

            if (userId != null)
            {
                context.Items["IsConnected"] = true;
                context.Items["UserId"] = userId;
                context.Items["UserName"] = userName;
                _logger.LogInformation("[Middleware] User data added to HttpContext.Items");
            }
            else
            {
                _logger.LogWarning("[Middleware] No user data found in session.");
            }

            await _next(context);
        }
    }

}
