using FoodJournal.Entities;
using FoodJournal.ServiceContracts.DTOs.FoodDTOs;

namespace FoodJournal.ServiceContracts;

public interface IFoodService : ICrudService<AddFoodRequest, UpdateFoodRequest, FoodResponse, int>
{
    public Task<List<Meal>> GetAllMealsAsync();
}