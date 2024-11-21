using Hash.Models;
using Hash.Models.DTOs;
using Hash.Services;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

namespace Hash.Data
{
    public static class DataSeeder
    {
        public static async Task SeedData(ApplicationDbContext context, IConfiguration configuration)
        {
            await SeedProducts(context, configuration);
            await SeedInitialUsers(context);
        }

        private static async Task SeedProducts(ApplicationDbContext context, IConfiguration configuration)
        {
            if (!context.Products.Any())
            {
                var seedData = configuration.GetSection("SeedData:Products").Get<List<Product>>();
                if (seedData != null)
                {
                    await context.Products.AddRangeAsync(seedData);
                    await context.SaveChangesAsync();
                }
            }
        }

        private static async Task SeedInitialUsers(ApplicationDbContext context)
        {
            if (!context.Users.Any())
            {
                // Create admin user
                var adminUser = new User
                {
                    Username = "admin",
                    Email = "admin@hash.com",
                    Password = HashPassword("Admin123!"),
                    Role = UserRoles.Admin
                };

                // Create regular user
                var regularUser = new User
                {
                    Username = "user1",
                    Email = "user1@hash.com",
                    Password = HashPassword("User123!"),
                    Role = UserRoles.User
                };

                await context.Users.AddRangeAsync(adminUser, regularUser);
                await context.SaveChangesAsync();
            }
        }

        private static string HashPassword(string password)
        {
            using var sha256 = SHA256.Create();
            var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
            return Convert.ToBase64String(hashedBytes);
        }
    }
} 