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

    public async Task<FoodResponse> AddAsync(AddFoodRequest? request)
    {
        ArgumentNullException.ThrowIfNull(request);

        if (await _context.FoodItems.AnyAsync(f => f.Name == request.Name))
        {
            _logger.LogWarning("Attempted to add food that already exists: {FoodName}", request.Name);
            var existingFood = await _context.FoodItems.FirstOrDefaultAsync(f => f.Name == request.Name);
            if (existingFood is not null)
                return MapToFoodResponse(existingFood);
        }

        var foodItem = MapToFoodEntity(request);

        if (request.MealIds?.Any() == true)
        {
            var meals = await _context.Meals
                .Where(m => request.MealIds.Contains(m.Id))
                .ToListAsync();

            var missing = request.MealIds.Except(meals.Select(m => m.Id)).ToList();
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

        _logger.LogInformation("Food with ID: {id} added.", foodItem.Id);
        return MapToFoodResponse(foodItem);
    }

    public async Task<List<Meal>> GetAllMealsAsync()
    {
        return await _context.Meals.AsNoTracking().ToListAsync();
    }

    public async Task<FoodResponse?> GetByIdAsync(int id)
    {
        ArgumentNullException.ThrowIfNull(id);

        var food = await _context.FoodItems
            .Include(f => f.Meals)
            .AsNoTracking()
            .FirstOrDefaultAsync(f => f.Id == id);

        if (food is null)
            return null;

        _logger.LogInformation("Food with ID: {FoodId} retrieved.", food?.Id);
        return MapToFoodResponse(food);
    }

    public async Task<List<FoodResponse>> GetAllAsync()
    {
        var foodList = await _context.FoodItems
            .AsNoTracking()
            .ToListAsync();

        return foodList.Select(MapToFoodResponse).ToList();
    }

    public async Task<FoodResponse?> UpdateAsync(UpdateFoodRequest? request)
    {
        ArgumentNullException.ThrowIfNull(request);

        var foodToUpdate = await _context.FoodItems.Include(f => f.Meals)
            .FirstOrDefaultAsync(f => f.Id == request.FoodId);

        if (foodToUpdate is null)
            throw new KeyNotFoundException($"Food with id {request.FoodId} not found.");


        foodToUpdate.Name = request.Name;
        foodToUpdate.Category = request.Category;
        foodToUpdate.Calories = request.Calories;
        foodToUpdate.Protein = request.Protein;
        foodToUpdate.Fat = request.Fat;
        foodToUpdate.Carbs = request.Carbs;
        foodToUpdate.Meals = request.Meals;

        if (request.MealIds?.Any() == true)
        {
            var meals = await _context.Meals
                .Where(m => request.MealIds.Contains(m.Id))
                .ToListAsync();

            var missing = request.MealIds.Except(meals.Select(m => m.Id)).ToList();
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

        _logger.LogInformation("Food with ID: {foodId} updated.", foodToUpdate.Id);
        return MapToFoodResponse(foodToUpdate);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        ArgumentNullException.ThrowIfNull(id);

        var food = await _context.FoodItems.FindAsync(id);

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

        _logger.LogInformation("Food with ID: {foodId} deleted.", food.Id);
        return true;
    }

    private static FoodResponse MapToFoodResponse(Food food)
    {
        return new FoodResponse
        {
            FoodId = food.Id,
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