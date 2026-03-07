using FoodJournal.Entities;
using FoodJournal.ServiceContracts.DTOs.FoodDTOs;

namespace FoodJournal.ServiceContracts;

public interface IFoodService
{
    public Task<FoodResponse> AddFoodAsync(AddFoodRequest? foodRequest);

    public Task<FoodResponse?> GetFoodByIdAsync(int? foodId);

    public Task<List<FoodResponse>> GetAllFoodAsync();

    public Task<FoodResponse?> UpdateFoodAsync(UpdateFoodRequest? foodRequest);

    public Task<bool> DeleteFoodAsync(int? foodId);

    public Task<List<Meal>> GetAllMealsAsync();
}