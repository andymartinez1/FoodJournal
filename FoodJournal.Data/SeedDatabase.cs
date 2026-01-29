using FoodJournal.Entities;
using FoodJournal.ServiceContracts.Enums;
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

        var chicken = new Food
        {
            Name = "Chicken Breast",
            Category = nameof(FoodCategory.Meat),
            Calories = 165,
            Protein = 31,
            Fat = 3.6,
            Carbs = 0,
        };
        var broccoli = new Food
        {
            Name = "Broccoli",
            Category = nameof(FoodCategory.Vegetable),
            Calories = 55,
            Protein = 3.7,
            Fat = 0.6,
            Carbs = 11.2,
        };
        var brownRice = new Food
        {
            Name = "Brown Rice",
            Category = nameof(FoodCategory.Grain),
            Calories = 216,
            Protein = 5,
            Fat = 1.8,
            Carbs = 45,
        };
        var salmon = new Food
        {
            Name = "Salmon",
            Category = nameof(FoodCategory.Seafood),
            Calories = 208,
            Protein = 20,
            Fat = 13,
            Carbs = 0,
        };
        var almonds100g = new Food
        {
            Name = "Almonds (100g)",
            Category = nameof(FoodCategory.Nuts),
            Calories = 575,
            Protein = 21,
            Fat = 49,
            Carbs = 22,
        };
        var greekYogurt = new Food
        {
            Name = "Greek Yogurt (plain, 170g)",
            Category = nameof(FoodCategory.Dairy),
            Calories = 100,
            Protein = 17,
            Fat = 0.7,
            Carbs = 6,
        };
        var apple = new Food
        {
            Name = "Apple (medium)",
            Category = nameof(FoodCategory.Fruit),
            Calories = 95,
            Protein = 0.5,
            Fat = 0.3,
            Carbs = 25,
        };
        var avocado = new Food
        {
            Name = "Avocado (medium)",
            Category = nameof(FoodCategory.Fruit),
            Calories = 234,
            Protein = 3,
            Fat = 21,
            Carbs = 12,
        };
        var sweetPotato = new Food
        {
            Name = "Sweet Potato (medium)",
            Category = nameof(FoodCategory.Vegetable),
            Calories = 103,
            Protein = 2,
            Fat = 0.2,
            Carbs = 24,
        };
        var oatmeal = new Food
        {
            Name = "Oatmeal (cooked, 1 cup)",
            Category = nameof(FoodCategory.Grain),
            Calories = 154,
            Protein = 6,
            Fat = 3,
            Carbs = 27,
        };
        var egg = new Food
        {
            Name = "Egg (large)",
            Category = nameof(FoodCategory.Protein),
            Calories = 78,
            Protein = 6,
            Fat = 5,
            Carbs = 0.6,
        };
        var spinach = new Food
        {
            Name = "Spinach (raw, 100g)",
            Category = nameof(FoodCategory.Vegetable),
            Calories = 23,
            Protein = 2.9,
            Fat = 0.4,
            Carbs = 3.6,
        };
        var banana = new Food
        {
            Name = "Banana (medium)",
            Category = nameof(FoodCategory.Fruit),
            Calories = 105,
            Protein = 1.3,
            Fat = 0.4,
            Carbs = 27,
        };
        var tofu = new Food
        {
            Name = "Tofu (100g)",
            Category = nameof(FoodCategory.Protein),
            Calories = 76,
            Protein = 8,
            Fat = 4.8,
            Carbs = 1.9,
        };
        var quinoa = new Food
        {
            Name = "Quinoa (cooked, 1 cup)",
            Category = nameof(FoodCategory.Grain),
            Calories = 222,
            Protein = 8,
            Fat = 3.6,
            Carbs = 39,
        };
        var peanutButter = new Food
        {
            Name = "Peanut Butter (2 tbsp)",
            Category = nameof(FoodCategory.Nuts),
            Calories = 188,
            Protein = 8,
            Fat = 16,
            Carbs = 6,
        };
        var cottageCheese = new Food
        {
            Name = "Cottage Cheese (100g)",
            Category = nameof(FoodCategory.Dairy),
            Calories = 98,
            Protein = 11,
            Fat = 4.3,
            Carbs = 3.4,
        };

        context.FoodItems.AddRange(
            chicken,
            broccoli,
            brownRice,
            salmon,
            almonds100g,
            greekYogurt,
            apple,
            avocado,
            sweetPotato,
            oatmeal,
            egg,
            spinach,
            banana,
            tofu,
            quinoa,
            peanutButter,
            cottageCheese
        );

        var grilledChicken = new Meal
        {
            Name = "Grilled Chicken with Broccoli and Rice",
            Description =
                "A healthy meal with grilled chicken breast, steamed broccoli, and brown rice.",
            MealType = MealType.Lunch,
            Calories = 436,
            Protein = 39.7,
            Fat = 6,
            Carbs = 56.2,
            Ingredients = new List<Food> { chicken, broccoli, brownRice },
        };
        var chickenStirFry = new Meal
        {
            Name = "Chicken Stir Fry",
            Description = "A delicious chicken stir fry with vegetables and rice.",
            MealType = MealType.Dinner,
            Calories = 500,
            Protein = 40,
            Fat = 10,
            Carbs = 60,
            Ingredients = new List<Food> { chicken, broccoli, brownRice, peanutButter },
        };
        var salmonBowl = new Meal
        {
            Name = "Salmon Quinoa Bowl",
            Description = "Seared salmon on a bed of quinoa with avocado and spinach.",
            MealType = MealType.Dinner,
            Calories = 550,
            Protein = 35,
            Fat = 22,
            Carbs = 45,
            Ingredients = new List<Food> { salmon, quinoa, avocado, spinach },
        };
        var proteinBreakfast = new Meal
        {
            Name = "Protein Breakfast",
            Description = "Eggs, oatmeal, and Greek yogurt for a high-protein start.",
            MealType = MealType.Breakfast,
            Calories = 420,
            Protein = 43,
            Fat = 12,
            Carbs = 45,
            Ingredients = new List<Food> { egg, oatmeal, greekYogurt, banana },
        };
        var veganTofuBowl = new Meal
        {
            Name = "Vegan Tofu Bowl",
            Description =
                "Roasted sweet potato, quinoa, tofu, and spinach for a balanced vegan bowl.",
            MealType = MealType.Lunch,
            Calories = 520,
            Protein = 28,
            Fat = 14,
            Carbs = 70,
            Ingredients = new List<Food> { tofu, quinoa, sweetPotato, spinach },
        };
        var snackPlate = new Meal
        {
            Name = "Snack Plate",
            Description = "A simple snack plate with almonds, apple, and cottage cheese.",
            MealType = MealType.Snack,
            Calories = 318,
            Protein = 17.5,
            Fat = 26.6,
            Carbs = 30.4,
            Ingredients = new List<Food> { almonds100g, apple, cottageCheese },
        };

        // link meals back to foods
        chicken.Meals = new List<Meal> { grilledChicken, chickenStirFry };
        broccoli.Meals = new List<Meal> { grilledChicken, chickenStirFry };
        brownRice.Meals = new List<Meal> { grilledChicken, chickenStirFry };
        salmon.Meals = new List<Meal> { salmonBowl };
        quinoa.Meals = new List<Meal> { salmonBowl, veganTofuBowl };
        avocado.Meals = new List<Meal> { salmonBowl };
        spinach.Meals = new List<Meal> { salmonBowl, veganTofuBowl };
        egg.Meals = new List<Meal> { proteinBreakfast };
        oatmeal.Meals = new List<Meal> { proteinBreakfast };
        greekYogurt.Meals = new List<Meal> { proteinBreakfast };
        banana.Meals = new List<Meal> { proteinBreakfast };
        tofu.Meals = new List<Meal> { veganTofuBowl };
        sweetPotato.Meals = new List<Meal> { veganTofuBowl };
        almonds100g.Meals = new List<Meal> { snackPlate };
        apple.Meals = new List<Meal> { snackPlate };
        cottageCheese.Meals = new List<Meal> { snackPlate };
        peanutButter.Meals = new List<Meal> { chickenStirFry };

        context.Meals.AddRange(
            grilledChicken,
            chickenStirFry,
            salmonBowl,
            proteinBreakfast,
            veganTofuBowl,
            snackPlate
        );

        context.Database.EnsureDeleted();
        context.Database.EnsureCreated();
        context.SaveChanges();
    }
}
