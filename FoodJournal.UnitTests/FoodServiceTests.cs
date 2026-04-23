using FoodJournal.Data;
using FoodJournal.ServiceContracts.DTOs.FoodDTOs;
using FoodJournal.ServiceContracts.Enums;
using FoodJournal.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;

namespace FoodJournal.UnitTests;

public class FoodServiceTests
{
    private readonly FoodService _foodService;
    private readonly ApplicationDbContext _dbContext;

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
    public async Task AddFood_IfNotNull_ReturnsFoodResponse()
    {
        // Arrange
        var request = new AddFoodRequest
        {
            Name = "Apple",
            Category = FoodCategory.Fruit,
            Calories = 52,
            Protein = 0.3,
            Fat = 0.2,
            Carbs = 14
        };

        // Act
        var result = await _foodService.AddAsync(request);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(request.Name, result.Name);
        Assert.Equal(request.Category.ToString(), result.Category);
    }
}