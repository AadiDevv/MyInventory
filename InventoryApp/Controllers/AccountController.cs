using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using InventoryApp.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

public class AccountController : Controller
{
    private readonly MyInventoryContext _context;
    private readonly string _jwtKey;


    public AccountController(MyInventoryContext context)
    {
        _context = context;
    }

    // GET: Register
    [HttpGet]
    public IActionResult Register() => View();

    // POST: Register
    [HttpPost]
    public async Task<IActionResult> Register(string Username, string Email, string Password)
    {
        string passwordHashed = HashPassword(Password);

        var user = new User
        {
            Username = Username,
            Email = Email,
            Password = passwordHashed
        };

        _context.Users.Add(user);
        _context.SaveChanges();

        return RedirectToAction("Login");
    }
    private string HashPassword(string password)
    {
        using var sha256 = SHA256.Create();
        var bytes = Encoding.UTF8.GetBytes(password);
        var hash = sha256.ComputeHash(bytes);
        return Convert.ToBase64String(hash);
    }

    // GET: Login
    [HttpGet]
    public IActionResult Login() => View();

    // POST: Login
    [HttpPost]

    public async Task<IActionResult> Login (string Email, string Password)
    {
        var user = _context.Users.FirstOrDefault(u => u.Email == Email);

        if(user == null)
        {
            return Unauthorized("Invalid Email");

        }
        if(user.Password != HashPassword(Password))
        {
            return Unauthorized("Invalid Password");
        }

        HttpContext.Session.SetString("UserId", user.Id.ToString());

        // Passer les informations utilisateur via TempData
        TempData["Connected"] = true;
        TempData["Username"] = user.Username;
        TempData["Email"] = user.Email;

        // Retourner la vue Index du dossier Home
        return View("~/Views/Home/Index.cshtml");

    }
    [HttpGet]
    public async Task<IActionResult> Profile()
    {
        var userId = HttpContext.Items["UserId"]?.ToString();

        if (userId == null || !int.TryParse(userId, out int userIdInt))
            return await Task.FromResult(Unauthorized());

        var user = await _context.Users
                .Where(u => u.Id == userIdInt)
                .FirstOrDefaultAsync();

        if (user == null)
            return NotFound("Utilisateur non trouvé");

        return Ok(user);


    }





    [HttpPost]
    public IActionResult Logout()
    {
        HttpContext.Session.Remove("UserId");
        return RedirectToAction("Login");
    }

}
