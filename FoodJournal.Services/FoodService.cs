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

        var foodItem = foodRequest.ToFoodEntity();
        await _context.AddAsync(foodItem);

        return foodItem.ToFoodResponse();
    }

    public async Task<FoodResponse?> GetFoodById(int foodId)
    {
        throw new NotImplementedException();
    }

    public async Task<List<FoodResponse>> GetAllFood()
    {
        return await _context.FoodItems.Select(f => f.ToFoodResponse()).ToListAsync();
    }

    public Task<FoodResponse?> UpdateFood(AddFoodRequest? foodRequest)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteFood(int foodId)
    {
        throw new NotImplementedException();
    }
}
