using FoodJournal.Entities;
using FoodJournal.ServiceContracts.DTOs;

namespace FoodJournal.ServiceContracts;

public interface IFoodService
{
    public Task<FoodResponse> AddFood(AddFoodRequest? foodRequest);

    public Task<FoodResponse?> GetFoodById(int? foodId);

    public Task<List<FoodResponse>> GetAllFood();

    public Task<FoodResponse?> UpdateFood(UpdateFoodRequest? foodRequest);

    public Task<bool> DeleteFood(int? foodId);

    public Task<Food> ConvertToFoodEntity(FoodResponse foodResponse);
}