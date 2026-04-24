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

        var count = await _dbContext.FoodConsumptionEntries.CountAsync(entry => entry.FoodId == food.Id);
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
}