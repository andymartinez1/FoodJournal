using FoodJournal.ServiceContracts.DTOs.MealDTOs;

namespace FoodJournal.ServiceContracts;

public interface IMealService
{
    public Task<MealResponse> AddMealAsync(AddMealRequest? foodRequest);

    public Task<MealResponse?> GetMealByIdAsync(int? foodId);

    public Task<List<MealResponse>> GetAllMealsAsync();

    public Task<MealResponse?> UpdateMealAsync(UpdateMealRequest? mealRequest);

    public Task<bool> DeleteMealAsync(int? mealId);
}