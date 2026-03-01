using FoodJournal.Data;
using FoodJournal.Entities;
using FoodJournal.ServiceContracts;
using FoodJournal.ServiceContracts.DTOs.Food;
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

    public async Task<FoodResponse> AddFoodAsync(AddFoodRequest? foodRequest)
    {
        ArgumentNullException.ThrowIfNull(foodRequest);

        if (await _context.FoodItems.AnyAsync(f => f.Name == foodRequest.Name))
        {
            _logger.LogWarning("Attempted to add food that already exists: {FoodName}", foodRequest.Name);
            var existingFood = await _context.FoodItems.FirstOrDefaultAsync(f => f.Name == foodRequest.Name);
            if (existingFood is not null)
                return existingFood.ToFoodResponse();
        }

        var foodItem = new Food
        {
            Name = foodRequest.Name,
            Category = foodRequest.Category.ToString(),
            Calories = foodRequest.Calories,
            Protein = foodRequest.Protein,
            Fat = foodRequest.Fat,
            Carbs = foodRequest.Carbs
        };

        if (foodRequest.MealIds?.Any() == true)
        {
            var meals = await _context.Meals
                .Where(m => foodRequest.MealIds.Contains(m.MealId))
                .ToListAsync();

            var missing = foodRequest.MealIds.Except(meals.Select(m => m.MealId)).ToList();
            if (missing.Any())
                _logger.LogWarning("Some MealIds were not found while creating food: {Missing}", missing);

            foodItem.Meals = meals;
        }

        await _context.AddAsync(foodItem);
        _logger.LogInformation("Food successfully added with ID: {foodId}.", foodItem.FoodId);
        await _context.SaveChangesAsync();

        return foodItem.ToFoodResponse();
    }

    public async Task<FoodResponse?> GetFoodByIdAsync(int? foodId)
    {
        if (foodId is null)
            throw new ArgumentNullException(nameof(foodId));

        var food = await _context.FoodItems
            .Include(f => f.Meals)
            .AsNoTracking()
            .FirstOrDefaultAsync(f => f.FoodId == foodId);

        _logger.LogInformation("Food with ID: {FoodId} retrieved.", food?.FoodId);

        if (food is null)
            return null;

        return food.ToFoodResponse();
    }

    public async Task<List<FoodResponse>> GetAllFoodAsync()
    {
        var foodList = await _context.FoodItems
            .AsNoTracking()
            .Select(f => f.ToFoodResponse())
            .ToListAsync();
        return foodList;
    }

    public async Task<FoodResponse?> UpdateFoodAsync(UpdateFoodRequest? foodRequest)
    {
        if (foodRequest is null)
            throw new ArgumentNullException(nameof(foodRequest));

        var foodToUpdate = await _context.FoodItems.FindAsync(foodRequest.FoodId);

        if (foodToUpdate is null)
            throw new KeyNotFoundException($"Food with id {foodRequest.FoodId} not found.");

        foodToUpdate.Name = foodRequest.Name;
        foodToUpdate.Category = foodRequest.Category;
        foodToUpdate.Calories = foodRequest.Calories;
        foodToUpdate.Protein = foodRequest.Protein;
        foodToUpdate.Fat = foodRequest.Fat;
        foodToUpdate.Carbs = foodRequest.Carbs;

        if (foodRequest.MealIds?.Any() == true)
        {
            var meals = await _context.Meals
                .Where(m => foodRequest.MealIds.Contains(m.MealId))
                .ToListAsync();

            var missing = foodRequest.MealIds.Except(meals.Select(m => m.MealId)).ToList();
            if (missing.Any())
                _logger.LogWarning("Some MealIds were not found while updating food: {Missing}", missing);

            foodToUpdate.Meals = meals;
        }
        else
        {
            foodToUpdate.Meals = new List<Meal>();
        }

        _context.FoodItems.Update(foodToUpdate);
        _logger.LogInformation("Food with ID: {foodId} updated.", foodToUpdate.FoodId);
        await _context.SaveChangesAsync();

        return foodToUpdate.ToFoodResponse();
    }

    public async Task<bool> DeleteFoodAsync(int? foodId)
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

    public async Task<List<Meal>> GetAllMealsAsync()
    {
        return await _context.Meals.AsNoTracking().ToListAsync();
    }
}