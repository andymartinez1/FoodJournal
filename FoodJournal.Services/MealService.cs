using FoodJournal.Data;
using FoodJournal.Entities;
using FoodJournal.ServiceContracts;
using FoodJournal.ServiceContracts.DTOs.MealDTOs;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace FoodJournal.Services;

public class MealService : IMealService
{
    private readonly ApplicationDbContext _context;
    private readonly ILogger<MealService> _logger;

    public MealService(ApplicationDbContext context, ILogger<MealService> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<MealResponse> AddMealAsync(AddMealRequest? mealRequest)
    {
        ArgumentNullException.ThrowIfNull(mealRequest);

        _logger.LogWarning("Attempted to add a meal that already exists: {MealName}", mealRequest.Name);
        var existingMeal = await _context.Meals.FirstOrDefaultAsync(m => m.Name == mealRequest.Name);
        if (existingMeal is not null)
            return MapToMealResponse(existingMeal);

        var meal = MapToMealEntity(mealRequest);

        await _context.Meals.AddAsync(meal);

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException ex)
        {
            _logger.LogWarning(ex, "Concurrency conflict while adding category.");
            return MapToMealResponse(meal);
        }
        catch (DbUpdateException ex)
        {
            _logger.LogError(ex, "Database update failed while adding category.");
            return MapToMealResponse(meal);
        }

        _logger.LogInformation("Meal with ID: {id} added.", meal.MealId);
        return MapToMealResponse(meal);
    }

    public async Task<MealResponse?> GetMealByIdAsync(int? mealId)
    {
        ArgumentNullException.ThrowIfNull(mealId);

        var meal = await _context.Meals
            .Include(m => m.Ingredients)
            .AsNoTracking()
            .FirstOrDefaultAsync(m => m.MealId == mealId);

        if (meal is null)
            return null;

        _logger.LogInformation("Meal with ID: {MealId} retrieved.", meal?.MealId);
        return MapToMealResponse(meal);
    }

    public async Task<List<MealResponse>> GetAllMealsAsync()
    {
        var meals = await _context.Meals
            .Include(m => m.Ingredients)
            .AsNoTracking()
            .ToListAsync();

        return meals.Select(MapToMealResponse).ToList();
    }

    public async Task<MealResponse?> UpdateMealAsync(UpdateMealRequest? mealRequest)
    {
        ArgumentNullException.ThrowIfNull(mealRequest);

        var mealToUpdate = await _context.Meals.FindAsync(mealRequest.MealId);

        if (mealToUpdate is null)
            throw new KeyNotFoundException($"Meal with id {mealRequest.MealId} not found.");

        mealToUpdate.Name = mealRequest.Name;
        mealToUpdate.Description = mealRequest.Description;
        mealToUpdate.MealType = mealRequest.MealType;
        mealToUpdate.Calories = mealRequest.Calories;
        mealToUpdate.Protein = mealRequest.Protein;
        mealToUpdate.Fat = mealRequest.Fat;
        mealToUpdate.Carbs = mealRequest.Carbs;
        mealToUpdate.IsFavorite = mealRequest.IsFavorite;
        mealToUpdate.TimesEaten = mealRequest.TimesEaten;
        mealToUpdate.LastDayEaten = mealRequest.LastDayEaten;

        if (mealRequest.IngredientIds?.Any() == true)
        {
            var ingredients = await _context.FoodItems
                .Where(f => mealRequest.IngredientIds.Contains(f.FoodId))
                .ToListAsync();

            var missing = mealRequest.IngredientIds.Except(ingredients.Select(f => f.FoodId)).ToList();
            if (missing.Any())
                _logger.LogWarning("Some Ingredient IDs were not found while updating meal: {Missing}", missing);

            mealToUpdate.Ingredients = ingredients;
        }
        else
        {
            mealToUpdate.Ingredients = new List<Food>();
        }

        _context.Meals.Update(mealToUpdate);

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException ex)
        {
            _logger.LogWarning(ex, "Concurrency conflict while adding category.");
            return MapToMealResponse(mealToUpdate);
        }
        catch (DbUpdateException ex)
        {
            _logger.LogError(ex, "Database update failed while adding category.");
            return MapToMealResponse(mealToUpdate);
        }

        _logger.LogInformation("Meal with ID: {mealId} updated.", mealToUpdate.MealId);
        return MapToMealResponse(mealToUpdate);
    }

    public async Task<bool> DeleteMealAsync(int? mealId)
    {
        ArgumentNullException.ThrowIfNull(mealId);

        var meal = await _context.Meals.FindAsync(mealId);

        if (meal is null)
            return false;

        _context.Remove(meal);

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

        _logger.LogInformation("Food with ID: {MealId} deleted.", meal.MealId);
        return true;
    }

    private static MealResponse MapToMealResponse(Meal meal)
    {
        return new MealResponse
        {
            MealId = meal.MealId,
            Name = meal.Name,
            Description = meal.Description,
            MealType = meal.MealType,
            Calories = meal.Calories,
            Protein = meal.Protein,
            Fat = meal.Fat,
            Carbs = meal.Carbs,
            IsFavorite = meal.IsFavorite,
            TimesEaten = meal.TimesEaten,
            LastDayEaten = meal.LastDayEaten,
            Ingredients = meal.Ingredients
        };
    }

    private static Meal MapToMealEntity(AddMealRequest request)
    {
        return new Meal
        {
            Name = request.Name,
            Description = request.Description,
            MealType = request.MealType,
            Calories = request.Calories,
            Protein = request.Protein,
            Fat = request.Fat,
            Carbs = request.Carbs,
            IsFavorite = request.IsFavorite,
            TimesEaten = request.TimesEaten,
            LastDayEaten = request.LastDayEaten,
            Ingredients = request.Ingredients
        };
    }
}