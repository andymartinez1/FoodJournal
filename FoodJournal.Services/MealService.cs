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

    public async Task<MealResponse> AddAsync(AddMealRequest? request)
    {
        ArgumentNullException.ThrowIfNull(request);

        _logger.LogWarning("Attempted to add a meal that already exists: {MealName}", request.Name);
        var existingMeal = await _context.Meals.FirstOrDefaultAsync(m => m.Name == request.Name);
        if (existingMeal is not null)
            return MapToMealResponse(existingMeal);

        var meal = MapToMealEntity(request);

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

        _logger.LogInformation("Meal with ID: {id} added.", meal.Id);
        return MapToMealResponse(meal);
    }

    public async Task<MealResponse?> GetByIdAsync(int id)
    {
        ArgumentNullException.ThrowIfNull(id);

        var meal = await _context
            .Meals.Include(m => m.Ingredients)
            .AsNoTracking()
            .FirstOrDefaultAsync(m => m.Id == id);

        if (meal is null)
            return null;

        _logger.LogInformation("Meal with ID: {MealId} retrieved.", meal?.Id);
        return MapToMealResponse(meal);
    }

    public async Task<List<MealResponse>> GetAllAsync()
    {
        var meals = await _context.Meals.Include(m => m.Ingredients).AsNoTracking().ToListAsync();

        return meals.Select(MapToMealResponse).ToList();
    }

    public async Task<MealResponse?> UpdateAsync(UpdateMealRequest? request)
    {
        ArgumentNullException.ThrowIfNull(request);

        var mealToUpdate = await _context
            .Meals.Include(m => m.Ingredients)
            .FirstOrDefaultAsync(m => m.Id == request.MealId);

        if (mealToUpdate is null)
            throw new KeyNotFoundException($"Meal with id {request.MealId} not found.");

        mealToUpdate.Name = request.Name;
        mealToUpdate.Description = request.Description;
        mealToUpdate.MealType = request.MealType;
        mealToUpdate.Calories = request.Calories;
        mealToUpdate.Protein = request.Protein;
        mealToUpdate.Fat = request.Fat;
        mealToUpdate.Carbs = request.Carbs;
        mealToUpdate.IsFavorite = request.IsFavorite;
        mealToUpdate.TimesEaten = request.TimesEaten;
        mealToUpdate.LastDayEaten = request.LastDayEaten;
        mealToUpdate.ImagePath = request.ImagePath;

        if (request.IngredientIds?.Any() == true)
        {
            var ingredients = await _context
                .FoodItems.Where(f => request.IngredientIds.Contains(f.Id))
                .ToListAsync();

            var missing = request.IngredientIds.Except(ingredients.Select(f => f.Id)).ToList();
            if (missing.Any())
                _logger.LogWarning(
                    "Some Ingredient IDs were not found while updating meal: {Missing}",
                    missing
                );

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

        _logger.LogInformation("Meal with ID: {mealId} updated.", mealToUpdate.Id);
        return MapToMealResponse(mealToUpdate);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        ArgumentNullException.ThrowIfNull(id);

        var meal = await _context.Meals.FindAsync(id);

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

        _logger.LogInformation("Food with ID: {MealId} deleted.", meal.Id);
        return true;
    }

    private static MealResponse MapToMealResponse(Meal meal)
    {
        return new MealResponse
        {
            MealId = meal.Id,
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
            ImagePath = meal.ImagePath,
            Ingredients = meal.Ingredients,
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
            ImagePath = request.ImagePath,
            Ingredients = request.Ingredients,
        };
    }
}
