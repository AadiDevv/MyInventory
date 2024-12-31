using System.Text;
using InventoryApp.Middleware;
using InventoryApp.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace InventoryApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // =============================
            // 🚀 CONFIGURATION DES SERVICES
            // =============================

            // 🔗 Configuration de la base de données
            builder.Services.AddDbContext<MyInventoryContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
            );

            // Session
            builder.Services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(90); // Durée d'expiration de la session
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });


            // 🔒 Configuration de l'autorisation
            builder.Services.AddAuthorization();

            // 📦 Ajout des contrôleurs et des vues
            builder.Services.AddControllersWithViews();

            // =============================
            // 🚀 CONFIGURATION DE L'APPLICATION
            // =============================

            var app = builder.Build();

            // 🌐 Gestion des erreurs et sécurité
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            // 📁 Configuration des fichiers statiques
            var provider = new FileExtensionContentTypeProvider();
            provider.Mappings[".js"] = "application/javascript";

            app.UseStaticFiles(new StaticFileOptions
            {
                ContentTypeProvider = provider
            });

            // 🛡️ Middleware d'authentification et d'autorisation
            app.UseRouting();
            app.UseSession();
            app.UseMiddleware<AuthMiddleware>();

            app.UseAuthentication();
            app.UseAuthorization();


            // 🗺️ Configuration des routes
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            // 🚀 Démarrage de l'application
            app.Run();
        }
    }
}
