using Hash.Models;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace Hash.Data
{
    public static class DataSeeder
    {
        public static async Task SeedData(ApplicationDbContext context, IConfiguration configuration)
        {
            await SeedProducts(context, configuration);
            await SeedUsers(context, configuration);
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

        private static async Task SeedUsers(ApplicationDbContext context, IConfiguration configuration)
        {
            if (!context.Users.Any())
            {
                var seedData = configuration.GetSection("SeedData:Users").Get<List<User>>();
                if (seedData != null)
                {
                    await context.Users.AddRangeAsync(seedData);
                    await context.SaveChangesAsync();
                }
            }
        }
    }
} 