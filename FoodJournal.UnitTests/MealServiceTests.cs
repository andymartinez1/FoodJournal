using FoodJournal.Data;
using FoodJournal.Entities;
using FoodJournal.ServiceContracts.DTOs.MealDTOs;
using FoodJournal.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;

namespace FoodJournal.UnitTests;

public class MealServiceTests
{
    private readonly ApplicationDbContext _dbContext;
    private readonly MealService _mealService;

    public MealServiceTests()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        _dbContext = new ApplicationDbContext(options);
        var mockLogger = new Mock<ILogger<MealService>>();
        _mealService = new MealService(_dbContext, mockLogger.Object);
    }

    [Fact]
    public async Task AddMeal_WhenImagePathProvided_PersistsImagePath()
    {
        var request = new AddMealRequest
        {
            Name = "Pasta Bowl",
            MealType = MealType.Dinner,
            ImagePath = "/img/pasta.png",
        };

        var result = await _mealService.AddAsync(request);

        Assert.Equal(request.ImagePath, result.ImagePath);
        var persisted = await _dbContext.Meals.FirstAsync();
        Assert.Equal(request.ImagePath, persisted.ImagePath);
    }

    [Fact]
    public async Task UpdateMeal_WhenIngredientIdsProvided_AssignsIngredients()
    {
        var ingredientA = new Food { Name = "Egg" };
        var ingredientB = new Food { Name = "Cheese" };
        var meal = new Meal { Name = "Omelette", MealType = MealType.Breakfast };

        _dbContext.FoodItems.AddRange(ingredientA, ingredientB);
        _dbContext.Meals.Add(meal);
        await _dbContext.SaveChangesAsync();

        var request = new UpdateMealRequest
        {
            MealId = meal.Id,
            Name = meal.Name,
            MealType = meal.MealType,
            IngredientIds = new List<int> { ingredientA.Id, 99999 },
        };

        var result = await _mealService.UpdateAsync(request);

        Assert.NotNull(result);
        Assert.Single(result!.Ingredients);
        Assert.Equal(ingredientA.Id, result.Ingredients[0].Id);
    }

    [Fact]
    public async Task GetAllMeals_ReturnsMealsWithMappedImagePath()
    {
        _dbContext.Meals.AddRange(
            new Meal
            {
                Name = "Toast",
                MealType = MealType.Breakfast,
                ImagePath = "/img/toast.png",
            },
            new Meal
            {
                Name = "Soup",
                MealType = MealType.Lunch,
                ImagePath = "/img/soup.png",
            }
        );
        await _dbContext.SaveChangesAsync();

        var result = await _mealService.GetAllAsync();

        Assert.Equal(2, result.Count);
        Assert.Contains(result, m => m.ImagePath == "/img/toast.png");
        Assert.Contains(result, m => m.ImagePath == "/img/soup.png");
    }

    [Fact]
    public async Task GetByIdMeal_WhenExists_ReturnsMealResponse()
    {
        var meal = new Meal
        {
            Name = "Salad",
            MealType = MealType.Lunch,
            ImagePath = "/img/salad.png",
        };
        _dbContext.Meals.Add(meal);
        await _dbContext.SaveChangesAsync();

        var result = await _mealService.GetByIdAsync(meal.Id);

        Assert.NotNull(result);
        Assert.Equal(meal.Id, result!.MealId);
        Assert.Equal("Salad", result.Name);
        Assert.Equal("/img/salad.png", result.ImagePath);
    }

    [Fact]
    public async Task GetByIdMeal_WhenMissing_ReturnsNull()
    {
        var result = await _mealService.GetByIdAsync(99999);

        Assert.Null(result);
    }

    [Fact]
    public async Task DeleteMeal_WhenExists_RemovesMealAndReturnsTrue()
    {
        var meal = new Meal { Name = "DeleteMe", MealType = MealType.Dinner };
        _dbContext.Meals.Add(meal);
        await _dbContext.SaveChangesAsync();

        var deleted = await _mealService.DeleteAsync(meal.Id);

        Assert.True(deleted);
        Assert.Null(await _dbContext.Meals.FindAsync(meal.Id));
    }

    [Fact]
    public async Task DeleteMeal_WhenMissing_ReturnsFalse()
    {
        var deleted = await _mealService.DeleteAsync(99999);

        Assert.False(deleted);
    }
}
