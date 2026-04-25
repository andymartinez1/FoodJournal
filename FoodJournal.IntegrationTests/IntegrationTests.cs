using FoodJournal.Data;
using FoodJournal.Entities;
using FoodJournal.ServiceContracts.DTOs.FoodDTOs;
using FoodJournal.ServiceContracts.DTOs.MealDTOs;
using FoodJournal.ServiceContracts.Enums;
using FoodJournal.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;

namespace FoodJournal.IntegrationTests;

public class IntegrationTests
{
    private static ApplicationDbContext CreateDbContext()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        return new ApplicationDbContext(options);
    }

    [Fact]
    public async Task MealAndFoodFlow_PersistsRelationsAndImagePath()
    {
        await using var context = CreateDbContext();
        var mealService = new MealService(context, new Mock<ILogger<MealService>>().Object);
        var foodService = new FoodService(context, new Mock<ILogger<FoodService>>().Object);

        var createdMeal = await mealService.AddAsync(
            new AddMealRequest
            {
                Name = "Salad Bowl",
                MealType = MealType.Lunch,
                ImagePath = "/img/salad-bowl.png",
            }
        );

        var createdFood = await foodService.AddAsync(
            new AddFoodRequest
            {
                Name = "Lettuce",
                Category = FoodCategory.Vegetable,
                MealIds = new List<int> { createdMeal.MealId },
            }
        );

        var fetchedFood = await foodService.GetByIdAsync(createdFood.FoodId);
        var fetchedMeal = await mealService.GetByIdAsync(createdMeal.MealId);

        Assert.NotNull(fetchedFood);
        Assert.Single(fetchedFood!.Meals);
        Assert.Equal(createdMeal.MealId, fetchedFood.Meals[0].Id);

        Assert.NotNull(fetchedMeal);
        Assert.Equal("/img/salad-bowl.png", fetchedMeal!.ImagePath);
    }

    [Fact]
    public async Task UpdatingMeal_WithIngredientIds_UpdatesIngredientCollection()
    {
        await using var context = CreateDbContext();
        var mealService = new MealService(context, new Mock<ILogger<MealService>>().Object);

        var ingredient1 = new Food { Name = "Chicken" };
        var ingredient2 = new Food { Name = "Rice" };
        var meal = new Meal { Name = "Prep Meal", MealType = MealType.Dinner };

        context.FoodItems.AddRange(ingredient1, ingredient2);
        context.Meals.Add(meal);
        await context.SaveChangesAsync();

        var updated = await mealService.UpdateAsync(
            new UpdateMealRequest
            {
                MealId = meal.Id,
                Name = "Prep Meal",
                MealType = MealType.Dinner,
                IngredientIds = new List<int> { ingredient1.Id, ingredient2.Id },
                ImagePath = "/img/prep-meal.png",
            }
        );

        Assert.NotNull(updated);
        Assert.Equal(2, updated!.Ingredients.Count);
        Assert.Equal("/img/prep-meal.png", updated.ImagePath);
    }
}
