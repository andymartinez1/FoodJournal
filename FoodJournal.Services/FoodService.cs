using FoodJournal.Data;
using FoodJournal.Entities;
using FoodJournal.ServiceContracts;
using FoodJournal.ServiceContracts.DTOs;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace FoodJournal.Services;

public class FoodService : IFoodService
{
    private readonly ApplicationDbContext _context;
    private readonly ILogger<FoodService> _logger;

    public FoodService(ApplicationDbContext context, ILogger<FoodService> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<FoodResponse> AddFood(AddFoodRequest? foodRequest)
    {
        if (foodRequest is null)
            throw new ArgumentNullException(nameof(foodRequest));

        if (_context.FoodItems.Where(f => f.Name == foodRequest.Name).Any())
            throw new ArgumentException("Food already exists in database.");

        var foodItem = foodRequest.ToFoodEntity();
        await _context.AddAsync(foodItem);
        _logger.LogInformation("Food successfully added with ID: {foodId}.", foodItem.FoodId);
        await _context.SaveChangesAsync();

        return foodItem.ToFoodResponse();
    }

    public async Task<FoodResponse?> GetFoodById(int? foodId)
    {
        if (foodId is null)
            throw new ArgumentNullException(nameof(foodId));

        var food = await _context.FoodItems.FindAsync(foodId);
        _logger.LogInformation("Food with ID: {FoodId} retrieved.", food.FoodId);

        if (food is null)
            return null;

        return food.ToFoodResponse();
    }

    public async Task<List<FoodResponse>> GetAllFood()
    {
        var foodList = await _context.FoodItems.Select(f => f.ToFoodResponse()).ToListAsync();
        return foodList;
    }

    public async Task<FoodResponse?> UpdateFood(UpdateFoodRequest? foodRequest)
    {
        if (foodRequest is null)
            throw new ArgumentNullException(nameof(foodRequest));

        var foodToUpdate = await _context.FoodItems.FindAsync(foodRequest.FoodId);

        foodToUpdate.Name = foodRequest.Name;
        foodToUpdate.Category = foodToUpdate.Category;
        foodToUpdate.Calories = foodRequest.Calories;
        foodToUpdate.Protein = foodRequest.Protein;
        foodToUpdate.Fat = foodRequest.Fat;
        foodToUpdate.Carbs = foodRequest.Carbs;
        foodToUpdate.Meals = foodRequest.Meals;

        _context.FoodItems.Update(foodToUpdate);
        _logger.LogInformation("Food with ID: {foodId} updated.", foodToUpdate.FoodId);
        await _context.SaveChangesAsync();

        return foodToUpdate.ToFoodResponse();
    }

    public async Task<bool> DeleteFood(int? foodId)
    {
        if (foodId is null)
            throw new ArgumentNullException(nameof(foodId));

        var food = await _context.FoodItems.FindAsync(foodId);

        if (food is null)
            return false;

        _context.Remove(food);
        _logger.LogInformation("Food with ID: {foodId} deleted.", food.FoodId);
        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<Food> ConvertToFoodEntity(FoodResponse foodResponse)
    {
        return new Food
        {
            FoodId = foodResponse.FoodId,
            Name = foodResponse.Name,
            Category = foodResponse.Category,
            Calories = foodResponse.Calories,
            Protein = foodResponse.Protein,
            Fat = foodResponse.Fat,
            Carbs = foodResponse.Carbs,
            Meals = foodResponse.Meals
        };
    }
}