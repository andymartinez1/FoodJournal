using FoodJournal.ServiceContracts.DTOs.MealDTOs;

namespace FoodJournal.ServiceContracts;

public interface IMealService : ICrudService<AddMealRequest, UpdateMealRequest, MealResponse, int>
{
}