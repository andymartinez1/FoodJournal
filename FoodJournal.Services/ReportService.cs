using FoodJournal.Data;
using FoodJournal.Entities;
using FoodJournal.ServiceContracts;
using FoodJournal.ServiceContracts.DTOs.ReportDTOs;
using Microsoft.EntityFrameworkCore;

namespace FoodJournal.Services;

public class ReportService : IReportService
{
    private readonly ApplicationDbContext _dbContext;

    public ReportService(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task LogFoodConsumptionAsync(int foodId, DateOnly consumedOn)
    {
        var foodExists = await _dbContext.FoodItems.AnyAsync(food => food.Id == foodId);
        if (!foodExists)
        {
            throw new InvalidOperationException("Food not found.");
        }

        var entry = new FoodConsumptionEntry { FoodId = foodId, ConsumedOn = consumedOn };

        _dbContext.FoodConsumptionEntries.Add(entry);
        await _dbContext.SaveChangesAsync();
    }

    public async Task LogMealConsumptionAsync(int mealId, DateOnly consumedOn)
    {
        var mealExists = await _dbContext.Meals.AnyAsync(meal => meal.Id == mealId);
        if (!mealExists)
        {
            throw new InvalidOperationException("Meal not found.");
        }

        var entry = new MealConsumptionEntry { MealId = mealId, ConsumedOn = consumedOn };

        _dbContext.MealConsumptionEntries.Add(entry);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<FoodConsumptionReportResponse> GetFoodConsumptionReportAsync(
        FoodConsumptionReportRequest request
    )
    {
        if (request.FromDate > request.ToDate)
        {
            throw new ArgumentException("FromDate cannot be later than ToDate.");
        }

        var food = await _dbContext.FoodItems.FirstOrDefaultAsync(item =>
            item.Id == request.FoodId
        );
        if (food is null)
        {
            throw new InvalidOperationException("Food not found.");
        }

        var timesConsumed = await _dbContext.FoodConsumptionEntries.CountAsync(entry =>
            entry.FoodId == request.FoodId
            && entry.ConsumedOn >= request.FromDate
            && entry.ConsumedOn <= request.ToDate
        );

        return new FoodConsumptionReportResponse
        {
            FoodId = request.FoodId,
            FoodName = food.Name,
            FromDate = request.FromDate,
            ToDate = request.ToDate,
            TimesConsumed = timesConsumed,
        };
    }

    public async Task<MealConsumptionReportResponse> GetMealConsumptionReportAsync(
        MealConsumptionReportRequest request
    )
    {
        if (request.FromDate > request.ToDate)
        {
            throw new ArgumentException("FromDate cannot be later than ToDate.");
        }

        var meal = await _dbContext.Meals.FirstOrDefaultAsync(item => item.Id == request.MealId);
        if (meal is null)
        {
            throw new InvalidOperationException("Meal not found.");
        }

        var timesConsumed = await _dbContext.MealConsumptionEntries.CountAsync(entry =>
            entry.MealId == request.MealId
            && entry.ConsumedOn >= request.FromDate
            && entry.ConsumedOn <= request.ToDate
        );

        return new MealConsumptionReportResponse
        {
            MealId = request.MealId,
            MealName = meal.Name,
            FromDate = request.FromDate,
            ToDate = request.ToDate,
            TimesConsumed = timesConsumed,
        };
    }
}
