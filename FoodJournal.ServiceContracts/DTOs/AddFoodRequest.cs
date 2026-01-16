using FoodJournal.Entities;

namespace FoodJournal.ServiceContracts.DTOs;

public class AddFoodRequest
{
    public string Name { get; set; } = string.Empty;

    public FoodCategory Category { get; set; }

    public int? Calories { get; set; }

    public double? Protein { get; set; }

    public double? Fat { get; set; }

    public double? Carbs { get; set; }

    public Food ToFoodEntity()
    {
        return new Food()
        {
            Name = Name,
            Category = Category,
            Calories = Calories,
            Protein = Protein,
            Fat = Fat,
            Carbs = Carbs,
        };
    }
}
