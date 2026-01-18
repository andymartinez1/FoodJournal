using FoodJournal.Data;
using FoodJournal.ServiceContracts;
using FoodJournal.ServiceContracts.DTOs;
using Microsoft.EntityFrameworkCore;

namespace FoodJournal.Services;

public class FoodService : IFoodService
{
    private readonly ApplicationDbContext _context;

    public FoodService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<FoodResponse> AddFood(AddFoodRequest? foodRequest)
    {
        if (foodRequest == null)
            throw new ArgumentNullException(nameof(foodRequest));

        if (_context.FoodItems.Where(f => f.Name == foodRequest.Name).Any())
            throw new ArgumentException("Food already exists in database.");

        var foodItem = foodRequest.ToFoodEntity();
        await _context.AddAsync(foodItem);
        await _context.SaveChangesAsync();

        return foodItem.ToFoodResponse();
    }

    public async Task<FoodResponse?> GetFoodById(int? foodId)
    {
        if (foodId is null)
            throw new ArgumentNullException(nameof(foodId));

        var food = await _context.FoodItems.FindAsync(foodId);

        if (food == null)
            return null;

        return food.ToFoodResponse();
    }

    public async Task<List<FoodResponse>> GetAllFood()
    {
        return await _context.FoodItems.Select(f => f.ToFoodResponse()).ToListAsync();
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
        await _context.SaveChangesAsync();

        return true;
    }
}
