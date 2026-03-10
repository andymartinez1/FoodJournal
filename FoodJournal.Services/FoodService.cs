using FoodJournal.Data;
using FoodJournal.Entities;
using FoodJournal.ServiceContracts;
using FoodJournal.ServiceContracts.DTOs.FoodDTOs;
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
                return MapToFoodResponse(existingFood);
        }

        var foodItem = MapToFoodEntity(foodRequest);

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

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException ex)
        {
            _logger.LogWarning(ex, "Concurrency conflict while adding category.");
            return MapToFoodResponse(foodItem);
        }
        catch (DbUpdateException ex)
        {
            _logger.LogError(ex, "Database update failed while adding category.");
            return MapToFoodResponse(foodItem);
        }

        _logger.LogInformation("Food with ID: {id} added.", foodItem.FoodId);
        return MapToFoodResponse(foodItem);
    }

    public async Task<FoodResponse?> GetFoodByIdAsync(int? foodId)
    {
        ArgumentNullException.ThrowIfNull(foodId);

        var food = await _context.FoodItems
            .Include(f => f.Meals)
            .AsNoTracking()
            .FirstOrDefaultAsync(f => f.FoodId == foodId);

        if (food is null)
            return null;

        _logger.LogInformation("Food with ID: {FoodId} retrieved.", food?.FoodId);
        return MapToFoodResponse(food);
    }

    public async Task<List<FoodResponse>> GetAllFoodAsync()
    {
        var foodList = await _context.FoodItems
            .AsNoTracking()
            .ToListAsync();

        return foodList.Select(MapToFoodResponse).ToList();
    }

    public async Task<FoodResponse?> UpdateFoodAsync(UpdateFoodRequest? foodRequest)
    {
        ArgumentNullException.ThrowIfNull(foodRequest);

        var foodToUpdate = await _context.FoodItems.Include(f => f.Meals)
            .FirstOrDefaultAsync(f => f.FoodId == foodRequest.FoodId);

        if (foodToUpdate is null)
            throw new KeyNotFoundException($"Food with id {foodRequest.FoodId} not found.");


        foodToUpdate.Name = foodRequest.Name;
        foodToUpdate.Category = foodRequest.Category;
        foodToUpdate.Calories = foodRequest.Calories;
        foodToUpdate.Protein = foodRequest.Protein;
        foodToUpdate.Fat = foodRequest.Fat;
        foodToUpdate.Carbs = foodRequest.Carbs;
        foodToUpdate.Meals = foodRequest.Meals;

        if (foodRequest.MealIds?.Any() == true)
        {
            var meals = await _context.Meals
                .Where(m => foodRequest.MealIds.Contains(m.MealId))
                .ToListAsync();

            var missing = foodRequest.MealIds.Except(meals.Select(m => m.MealId)).ToList();
            if (missing.Any())
                _logger.LogWarning("Some Meal IDs were not found while updating food: {Missing}", missing);

            foodToUpdate.Meals = meals;
        }
        else
        {
            foodToUpdate.Meals = new List<Meal>();
        }

        _context.FoodItems.Update(foodToUpdate);

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException ex)
        {
            _logger.LogWarning(ex, "Concurrency conflict while adding category.");
            return MapToFoodResponse(foodToUpdate);
        }
        catch (DbUpdateException ex)
        {
            _logger.LogError(ex, "Database update failed while adding category.");
            return MapToFoodResponse(foodToUpdate);
        }

        _logger.LogInformation("Food with ID: {foodId} updated.", foodToUpdate.FoodId);
        return MapToFoodResponse(foodToUpdate);
    }

    public async Task<bool> DeleteFoodAsync(int? foodId)
    {
        ArgumentNullException.ThrowIfNull(foodId);

        var food = await _context.FoodItems.FindAsync(foodId);

        if (food is null)
            return false;

        _context.Remove(food);

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException ex)
        {
            _logger.LogWarning(ex, "Concurrency conflict while adding category.");
            return false;
        }
        catch (DbUpdateException ex)
        {
            _logger.LogError(ex, "Database update failed while adding category.");
            return false;
        }

        _logger.LogInformation("Food with ID: {foodId} deleted.", food.FoodId);
        return true;
    }

    public async Task<List<Meal>> GetAllMealsAsync()
    {
        return await _context.Meals.AsNoTracking().ToListAsync();
    }

    private static FoodResponse MapToFoodResponse(Food food)
    {
        return new FoodResponse
        {
            FoodId = food.FoodId,
            Name = food.Name,
            Category = food.Category,
            Calories = food.Calories,
            Protein = food.Protein,
            Fat = food.Fat,
            Carbs = food.Carbs,
            Meals = food.Meals
        };
    }

    private static Food MapToFoodEntity(AddFoodRequest request)
    {
        return new Food
        {
            Name = request.Name,
            Category = request.Category.ToString(),
            Calories = request.Calories,
            Protein = request.Protein,
            Fat = request.Fat,
            Carbs = request.Carbs,
            Meals = request.Meals
        };
    }
}