using FoodJournal.Data;
using FoodJournal.Entities;
using FoodJournal.ServiceContracts.DTOs.FoodDTOs;
using FoodJournal.ServiceContracts.Enums;
using FoodJournal.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;

namespace FoodJournal.UnitTests;

public class FoodServiceTests
{
    private readonly ApplicationDbContext _dbContext;
    private readonly FoodService _foodService;

    public FoodServiceTests()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        _dbContext = new ApplicationDbContext(options);
        var mockLogger = new Mock<ILogger<FoodService>>();
        _foodService = new FoodService(_dbContext, mockLogger.Object);
    }

    [Fact]
    public async Task AddFood_WhenRequestIsValid_ReturnsNotNullResult()
    {
        // Arrange
        var request = new AddFoodRequest
        {
            Name = "Apple",
            Category = FoodCategory.Fruit,
            Calories = 52,
            Protein = 0.3,
            Fat = 0.2,
            Carbs = 14,
        };

        // Act
        var result = await _foodService.AddAsync(request);

        // Assert
        Assert.NotNull(result);
    }

    [Fact]
    public async Task AddFood_WhenRequestIsValid_MapsNameFromRequest()
    {
        var request = new AddFoodRequest
        {
            Name = "Apple",
            Category = FoodCategory.Fruit,
            Calories = 52,
            Protein = 0.3,
            Fat = 0.2,
            Carbs = 14,
        };

        var result = await _foodService.AddAsync(request);

        Assert.Equal(request.Name, result.Name);
    }

    [Fact]
    public async Task AddFood_WhenRequestIsValid_MapsCategoryFromRequest()
    {
        var request = new AddFoodRequest
        {
            Name = "Apple",
            Category = FoodCategory.Fruit,
            Calories = 52,
            Protein = 0.3,
            Fat = 0.2,
            Carbs = 14,
        };

        var result = await _foodService.AddAsync(request);

        Assert.Equal(request.Category.ToString(), result.Category);
    }

    [Fact]
    public async Task AddFood_WhenMealIdsProvided_AssignsExistingMealsOnly()
    {
        // Arrange
        var breakfast = new Meal { Name = "Breakfast", MealType = MealType.Breakfast };
        var dinner = new Meal { Name = "Dinner", MealType = MealType.Dinner };
        _dbContext.Meals.AddRange(breakfast, dinner);
        await _dbContext.SaveChangesAsync();

        var request = new AddFoodRequest
        {
            Name = "Rice",
            Category = FoodCategory.Grain,
            MealIds = new List<int> { breakfast.Id, 9999 },
        };

        // Act
        var result = await _foodService.AddAsync(request);

        // Assert
        Assert.NotNull(result);
        Assert.Single(result.Meals);
        Assert.Equal(breakfast.Id, result.Meals[0].Id);
    }

    [Fact]
    public async Task AddFood_WhenDuplicateNameExists_ReturnsExistingFood()
    {
        // Arrange
        var existing = new Food { Name = "Banana", Category = FoodCategory.Fruit.ToString() };
        _dbContext.FoodItems.Add(existing);
        await _dbContext.SaveChangesAsync();

        var request = new AddFoodRequest { Name = "Banana", Category = FoodCategory.Fruit };

        // Act
        var result = await _foodService.AddAsync(request);

        // Assert
        Assert.Equal(existing.Id, result.FoodId);
        Assert.Equal("Banana", result.Name);
        Assert.Equal(1, await _dbContext.FoodItems.CountAsync());
    }

    [Fact]
    public async Task GetByIdFood_WhenExists_ReturnsFoodResponse()
    {
        var food = new Food { Name = "Yogurt", Category = FoodCategory.Dairy.ToString() };
        _dbContext.FoodItems.Add(food);
        await _dbContext.SaveChangesAsync();

        var result = await _foodService.GetByIdAsync(food.Id);

        Assert.NotNull(result);
        Assert.Equal(food.Id, result!.FoodId);
        Assert.Equal("Yogurt", result.Name);
    }

    [Fact]
    public async Task GetByIdFood_WhenMissing_ReturnsNull()
    {
        var result = await _foodService.GetByIdAsync(99999);

        Assert.Null(result);
    }

    [Fact]
    public async Task GetAllFood_WhenFoodsExist_ReturnsCorrectCount()
    {
        _dbContext.FoodItems.AddRange(
            new Food { Name = "Bread", Category = FoodCategory.Grain.ToString() },
            new Food { Name = "Milk", Category = FoodCategory.Dairy.ToString() }
        );
        await _dbContext.SaveChangesAsync();

        var result = await _foodService.GetAllAsync();

        Assert.Equal(2, result.Count);
    }

    [Fact]
    public async Task GetAllFood_WhenFoodsExist_ContainsBread()
    {
        _dbContext.FoodItems.AddRange(
            new Food { Name = "Bread", Category = FoodCategory.Grain.ToString() },
            new Food { Name = "Milk", Category = FoodCategory.Dairy.ToString() }
        );
        await _dbContext.SaveChangesAsync();

        var result = await _foodService.GetAllAsync();

        Assert.Contains(result, f => f.Name == "Bread");
    }

    [Fact]
    public async Task GetAllFood_WhenFoodsExist_ContainsMilk()
    {
        _dbContext.FoodItems.AddRange(
            new Food { Name = "Bread", Category = FoodCategory.Grain.ToString() },
            new Food { Name = "Milk", Category = FoodCategory.Dairy.ToString() }
        );
        await _dbContext.SaveChangesAsync();

        var result = await _foodService.GetAllAsync();

        Assert.Contains(result, f => f.Name == "Milk");
    }

    [Fact]
    public async Task UpdateFood_WhenRequestValid_UpdatesReturnedEntity()
    {
        var breakfast = new Meal { Name = "Breakfast", MealType = MealType.Breakfast };
        var lunch = new Meal { Name = "Lunch", MealType = MealType.Lunch };
        var food = new Food { Name = "Oats", Category = FoodCategory.Grain.ToString() };

        _dbContext.Meals.AddRange(breakfast, lunch);
        _dbContext.FoodItems.Add(food);
        await _dbContext.SaveChangesAsync();

        var request = new UpdateFoodRequest
        {
            FoodId = food.Id,
            Name = "Oats Updated",
            Category = FoodCategory.Grain.ToString(),
            Calories = 389,
            Protein = 16.9,
            Fat = 6.9,
            Carbs = 66.3,
            MealIds = new List<int> { breakfast.Id, 99999 },
            Meals = new List<Meal>(),
        };

        var result = await _foodService.UpdateAsync(request);

        Assert.NotNull(result);
        Assert.Equal("Oats Updated", result!.Name);
    }

    [Fact]
    public async Task UpdateFood_WhenRequestValid_AssignsExistingMealsOnReturnedEntity()
    {
        var breakfast = new Meal { Name = "Breakfast", MealType = MealType.Breakfast };
        var lunch = new Meal { Name = "Lunch", MealType = MealType.Lunch };
        var food = new Food { Name = "Oats", Category = FoodCategory.Grain.ToString() };

        _dbContext.Meals.AddRange(breakfast, lunch);
        _dbContext.FoodItems.Add(food);
        await _dbContext.SaveChangesAsync();

        var request = new UpdateFoodRequest
        {
            FoodId = food.Id,
            Name = "Oats Updated",
            Category = FoodCategory.Grain.ToString(),
            Calories = 389,
            Protein = 16.9,
            Fat = 6.9,
            Carbs = 66.3,
            MealIds = new List<int> { breakfast.Id, 99999 },
            Meals = new List<Meal>(),
        };

        var result = await _foodService.UpdateAsync(request);

        Assert.NotNull(result);
        Assert.Single(result!.Meals);
        Assert.Equal(breakfast.Id, result.Meals[0].Id);
    }

    [Fact]
    public async Task UpdateFood_WhenRequestValid_UpdatesPersistedEntityName()
    {
        var breakfast = new Meal { Name = "Breakfast", MealType = MealType.Breakfast };
        var lunch = new Meal { Name = "Lunch", MealType = MealType.Lunch };
        var food = new Food { Name = "Oats", Category = FoodCategory.Grain.ToString() };

        _dbContext.Meals.AddRange(breakfast, lunch);
        _dbContext.FoodItems.Add(food);
        await _dbContext.SaveChangesAsync();

        var request = new UpdateFoodRequest
        {
            FoodId = food.Id,
            Name = "Oats Updated",
            Category = FoodCategory.Grain.ToString(),
            Calories = 389,
            Protein = 16.9,
            Fat = 6.9,
            Carbs = 66.3,
            MealIds = new List<int> { breakfast.Id, 99999 },
            Meals = new List<Meal>(),
        };

        await _foodService.UpdateAsync(request);

        var persisted = await _dbContext
            .FoodItems.Include(f => f.Meals)
            .FirstAsync(f => f.Id == food.Id);
        Assert.Equal("Oats Updated", persisted.Name);
    }

    [Fact]
    public async Task UpdateFood_WhenRequestValid_AssignsExistingMealsOnPersistedEntity()
    {
        var breakfast = new Meal { Name = "Breakfast", MealType = MealType.Breakfast };
        var lunch = new Meal { Name = "Lunch", MealType = MealType.Lunch };
        var food = new Food { Name = "Oats", Category = FoodCategory.Grain.ToString() };

        _dbContext.Meals.AddRange(breakfast, lunch);
        _dbContext.FoodItems.Add(food);
        await _dbContext.SaveChangesAsync();

        var request = new UpdateFoodRequest
        {
            FoodId = food.Id,
            Name = "Oats Updated",
            Category = FoodCategory.Grain.ToString(),
            Calories = 389,
            Protein = 16.9,
            Fat = 6.9,
            Carbs = 66.3,
            MealIds = new List<int> { breakfast.Id, 99999 },
            Meals = new List<Meal>(),
        };

        await _foodService.UpdateAsync(request);

        var persisted = await _dbContext
            .FoodItems.Include(f => f.Meals)
            .FirstAsync(f => f.Id == food.Id);
        Assert.Single(persisted.Meals);
    }

    [Fact]
    public async Task DeleteFood_WhenExists_RemovesFoodAndReturnsTrue()
    {
        var food = new Food { Name = "ToDelete", Category = FoodCategory.Processed.ToString() };
        _dbContext.FoodItems.Add(food);
        await _dbContext.SaveChangesAsync();

        var deleted = await _foodService.DeleteAsync(food.Id);

        Assert.True(deleted);
        Assert.Null(await _dbContext.FoodItems.FindAsync(food.Id));
    }

    [Fact]
    public async Task DeleteFood_WhenMissing_ReturnsFalse()
    {
        var deleted = await _foodService.DeleteAsync(99999);

        Assert.False(deleted);
    }
}
