using Microsoft.EntityFrameworkCore;
using SimpleApi.Models;

namespace SimpleApi.Data;

public static class DbSeeder
{
    public static async Task SeedAsync(AppDbContext db)
    {
        // Seed Users
        if (!await db.Users.AnyAsync())
        {
            var users = new List<User>
            {
                new() { Username = "admin", PasswordHash = BCrypt.Net.BCrypt.HashPassword("admin123") },
                new() { Username = "johndoe", PasswordHash = BCrypt.Net.BCrypt.HashPassword("john123") },
            };

            db.Users.AddRange(users);
            await db.SaveChangesAsync();

            Console.WriteLine($"[Seeder] ✅ {users.Count} users seeded.");
        }
        else
        {
            Console.WriteLine("[Seeder] ⏭️  Users already exist, skipping.");
        }

        // Seed Products
        if (!await db.Products.AnyAsync())
        {
            var products = new List<Product>
            {
                new() { Name = "Laptop Asus ROG", Description = "Gaming laptop 15 inch, RTX 4060", Price = 18_500_000, Stock = 10 },
                new() { Name = "Mouse Logitech G502", Description = "Wireless gaming mouse, 25K DPI", Price = 850_000, Stock = 50 },
                new() { Name = "Keyboard Mechanical Keychron", Description = "TKL layout, red switch", Price = 1_250_000, Stock = 30 },
                new() { Name = "Monitor LG 27\" 144Hz", Description = "IPS panel, FHD, 1ms response time", Price = 4_200_000, Stock = 15 },
                new() { Name = "Headset HyperX Cloud II", Description = "7.1 surround sound, memory foam", Price = 1_100_000, Stock = 25 },
            };

            db.Products.AddRange(products);
            await db.SaveChangesAsync();

            Console.WriteLine($"[Seeder] ✅ {products.Count} products seeded.");
        }
        else
        {
            Console.WriteLine("[Seeder] ⏭️  Products already exist, skipping.");
        }
    }
}
