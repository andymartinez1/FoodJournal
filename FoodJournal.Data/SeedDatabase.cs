using FoodJournal.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace FoodJournal.Data;

public static class SeedDatabase
{
    public static void InitializeDatabase(IServiceProvider serviceProvider)
    {
        using var context = new ApplicationDbContext(
            serviceProvider.GetRequiredService<DbContextOptions<ApplicationDbContext>>()
        );

        // Seeding a test user
        var hasher = new PasswordHasher<ApplicationUser>();
        context.Users.Add(
            new ApplicationUser
            {
                Id = Guid.NewGuid().ToString(),
                UserName = "test@test.com",
                NormalizedUserName = "TEST@TEST.COM",
                Email = "test@test.com",
                NormalizedEmail = "TEST@TEST.COM",
                EmailConfirmed = true,
                LockoutEnabled = false,
                SecurityStamp = Guid.NewGuid().ToString(),
                PasswordHash = hasher.HashPassword(null, "Password1!"),
            }
        );

        context.FoodItems.AddRange(
            new Food
            {
                Name = "Chicken Breast",
                Category = nameof(FoodCategory.Meat),
                Calories = 165,
                Protein = 31,
                Fat = 3.6,
                Carbs = 0,
            },
            new Food
            {
                Name = "Broccoli",
                Category = nameof(FoodCategory.Vegetable),
                Calories = 55,
                Protein = 3.7,
                Fat = 0.6,
                Carbs = 11.2,
            },
            new Food
            {
                Name = "Brown Rice",
                Category = nameof(FoodCategory.Grain),
                Calories = 216,
                Protein = 5,
                Fat = 1.8,
                Carbs = 45,
            }
        );

        context.Meals.AddRange(
            new Meal
            {
                Name = "Grilled Chicken with Broccoli and Rice",
                Description =
                    "A healthy meal with grilled chicken breast, steamed broccoli, and brown rice.",
                MealType = MealType.Lunch,
                Calories = 436,
                Protein = 39.7,
                Fat = 6,
                Carbs = 56.2,
            },
            new Meal
            {
                Name = "Chicken Stir Fry",
                Description = "A delicious chicken stir fry with vegetables and rice.",
                MealType = MealType.Dinner,
                Calories = 500,
                Protein = 40,
                Fat = 10,
                Carbs = 60,
            }
        );

        context.Database.EnsureDeleted();
        context.Database.EnsureCreated();
        context.SaveChanges();
    }
}
