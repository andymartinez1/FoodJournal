using FoodJournal.Data;
using FoodJournal.Entities;
using FoodJournal.ServiceContracts.DTOs.ReportDTOs;
using FoodJournal.Services;
using Microsoft.EntityFrameworkCore;

namespace FoodJournal.UnitTests;

public class ReportServiceTests
{
    private readonly ApplicationDbContext _dbContext;
    private readonly ReportService _reportService;

    public ReportServiceTests()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        _dbContext = new ApplicationDbContext(options);
        _reportService = new ReportService(_dbContext);
    }

    [Fact]
    public async Task LogFoodConsumption_WhenFoodExists_AddsEntry()
    {
        var food = new Food { Name = "Apple", Category = "Fruit" };
        _dbContext.FoodItems.Add(food);
        await _dbContext.SaveChangesAsync();

        await _reportService.LogFoodConsumptionAsync(food.Id, new DateOnly(2026, 4, 20));

        var count = await _dbContext.FoodConsumptionEntries.CountAsync(entry =>
            entry.FoodId == food.Id
        );
        Assert.Equal(1, count);
    }

    [Fact]
    public async Task GetFoodConsumptionReport_WhenEntriesInRange_ReturnsCorrectCount()
    {
        var food = new Food { Name = "Apple", Category = "Fruit" };
        _dbContext.FoodItems.Add(food);
        await _dbContext.SaveChangesAsync();

        _dbContext.FoodConsumptionEntries.AddRange(
            new FoodConsumptionEntry { FoodId = food.Id, ConsumedOn = new DateOnly(2026, 4, 10) },
            new FoodConsumptionEntry { FoodId = food.Id, ConsumedOn = new DateOnly(2026, 4, 15) },
            new FoodConsumptionEntry { FoodId = food.Id, ConsumedOn = new DateOnly(2026, 4, 25) }
        );
        await _dbContext.SaveChangesAsync();

        var request = new FoodConsumptionReportRequest
        {
            FoodId = food.Id,
            FromDate = new DateOnly(2026, 4, 10),
            ToDate = new DateOnly(2026, 4, 20),
        };

        var report = await _reportService.GetFoodConsumptionReportAsync(request);

        Assert.Equal(food.Id, report.FoodId);
        Assert.Equal("Apple", report.FoodName);
        Assert.Equal(2, report.TimesConsumed);
    }

    [Fact]
    public async Task GetFoodConsumptionReport_WhenBoundaryDatesMatch_IncludesBoundaryEntries()
    {
        var food = new Food { Name = "Rice", Category = "Grain" };
        _dbContext.FoodItems.Add(food);
        await _dbContext.SaveChangesAsync();

        _dbContext.FoodConsumptionEntries.AddRange(
            new FoodConsumptionEntry { FoodId = food.Id, ConsumedOn = new DateOnly(2026, 4, 1) },
            new FoodConsumptionEntry { FoodId = food.Id, ConsumedOn = new DateOnly(2026, 4, 30) }
        );
        await _dbContext.SaveChangesAsync();

        var request = new FoodConsumptionReportRequest
        {
            FoodId = food.Id,
            FromDate = new DateOnly(2026, 4, 1),
            ToDate = new DateOnly(2026, 4, 30),
        };

        var report = await _reportService.GetFoodConsumptionReportAsync(request);

        Assert.Equal(2, report.TimesConsumed);
    }

    [Fact]
    public async Task GetFoodConsumptionReport_WhenNoEntriesInRange_ReturnsZero()
    {
        var food = new Food { Name = "Bread", Category = "Grain" };
        _dbContext.FoodItems.Add(food);
        await _dbContext.SaveChangesAsync();

        var request = new FoodConsumptionReportRequest
        {
            FoodId = food.Id,
            FromDate = new DateOnly(2026, 4, 1),
            ToDate = new DateOnly(2026, 4, 30),
        };

        var report = await _reportService.GetFoodConsumptionReportAsync(request);

        Assert.Equal(0, report.TimesConsumed);
    }

    [Fact]
    public async Task LogMealConsumption_WhenMealExists_AddsEntry()
    {
        var meal = new Meal { Name = "Breakfast Bowl", MealType = MealType.Breakfast };
        _dbContext.Meals.Add(meal);
        await _dbContext.SaveChangesAsync();

        await _reportService.LogMealConsumptionAsync(meal.Id, new DateOnly(2026, 4, 20));

        var count = await _dbContext.MealConsumptionEntries.CountAsync(entry =>
            entry.MealId == meal.Id
        );
        Assert.Equal(1, count);
    }

    [Fact]
    public async Task GetMealConsumptionReport_WhenEntriesInRange_ReturnsCorrectCount()
    {
        var meal = new Meal { Name = "Protein Lunch", MealType = MealType.Lunch };
        _dbContext.Meals.Add(meal);
        await _dbContext.SaveChangesAsync();

        _dbContext.MealConsumptionEntries.AddRange(
            new MealConsumptionEntry { MealId = meal.Id, ConsumedOn = new DateOnly(2026, 4, 10) },
            new MealConsumptionEntry { MealId = meal.Id, ConsumedOn = new DateOnly(2026, 4, 15) },
            new MealConsumptionEntry { MealId = meal.Id, ConsumedOn = new DateOnly(2026, 4, 25) }
        );
        await _dbContext.SaveChangesAsync();

        var request = new MealConsumptionReportRequest
        {
            MealId = meal.Id,
            FromDate = new DateOnly(2026, 4, 10),
            ToDate = new DateOnly(2026, 4, 20),
        };

        var report = await _reportService.GetMealConsumptionReportAsync(request);

        Assert.Equal(meal.Id, report.MealId);
        Assert.Equal("Protein Lunch", report.MealName);
        Assert.Equal(2, report.TimesConsumed);
    }

    [Fact]
    public async Task GetMealConsumptionReport_WhenBoundaryDatesMatch_IncludesBoundaryEntries()
    {
        var meal = new Meal { Name = "Evening Meal", MealType = MealType.Dinner };
        _dbContext.Meals.Add(meal);
        await _dbContext.SaveChangesAsync();

        _dbContext.MealConsumptionEntries.AddRange(
            new MealConsumptionEntry { MealId = meal.Id, ConsumedOn = new DateOnly(2026, 4, 1) },
            new MealConsumptionEntry { MealId = meal.Id, ConsumedOn = new DateOnly(2026, 4, 30) }
        );
        await _dbContext.SaveChangesAsync();

        var request = new MealConsumptionReportRequest
        {
            MealId = meal.Id,
            FromDate = new DateOnly(2026, 4, 1),
            ToDate = new DateOnly(2026, 4, 30),
        };

        var report = await _reportService.GetMealConsumptionReportAsync(request);

        Assert.Equal(2, report.TimesConsumed);
    }

    [Fact]
    public async Task GetMealConsumptionReport_WhenNoEntriesInRange_ReturnsZero()
    {
        var meal = new Meal { Name = "Snack Plate", MealType = MealType.Snack };
        _dbContext.Meals.Add(meal);
        await _dbContext.SaveChangesAsync();

        var request = new MealConsumptionReportRequest
        {
            MealId = meal.Id,
            FromDate = new DateOnly(2026, 4, 1),
            ToDate = new DateOnly(2026, 4, 30),
        };

        var report = await _reportService.GetMealConsumptionReportAsync(request);

        Assert.Equal(0, report.TimesConsumed);
    }
}
