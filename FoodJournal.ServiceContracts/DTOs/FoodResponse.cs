using FoodJournal.Entities;

namespace FoodJournal.ServiceContracts.DTOs;

public class FoodResponse
{
    public int FoodId { get; set; }

    public string Name { get; set; } = string.Empty;

    public FoodCategory Category { get; set; }

    public int? Calories { get; set; }

    public double? Protein { get; set; }

    public double? Fat { get; set; }

    public double? Carbs { get; set; }

    public List<Meal> Meals { get; set; } = [];
}

public static class FoodExtensions
{
    public static FoodResponse ToFoodResponse(this Food food)
    {
        return new FoodResponse()
        {
            FoodId = food.FoodId,
            Name = food.Name,
            Category = food.Category,
            Calories = food.Calories,
            Protein = food.Protein,
            Fat = food.Fat,
            Carbs = food.Carbs,
        };
    }
}
