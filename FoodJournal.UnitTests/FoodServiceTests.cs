using FoodJournal.Data;
using FoodJournal.Services;
using Microsoft.Extensions.Logging;
using Moq;

namespace FoodJournal.UnitTests;

public class FoodServiceTests
{
    private FoodService _foodService;
    private Mock<ApplicationDbContext> _mockDbContext;
    private Mock<ILogger<FoodService>> _mockLogger;

    public FoodServiceTests()
    {
        var _mockDbContext = new Mock<ApplicationDbContext>();
        var _mockLogger = new Mock<ILogger<FoodService>>();
        _foodService = new FoodService(_mockDbContext.Object, _mockLogger.Object);
    }

    [Fact]
    public void AddFood_IfNotNull_ReturnsFoodResponse()
    {
        // Arrange

        // Act

        // Assert
    }
}