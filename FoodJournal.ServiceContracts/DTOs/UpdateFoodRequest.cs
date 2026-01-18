using System.ComponentModel.DataAnnotations;
using FoodJournal.Entities;

namespace FoodJournal.ServiceContracts.DTOs;

public class UpdateFoodRequest
{
    [Required(ErrorMessage = "ID cannot be empty.")]
    public int FoodId { get; set; }

    [Required(ErrorMessage = "Name cannot be empty.")]
    public string Name { get; set; } = string.Empty;

    public FoodCategory Category { get; set; }

    public int? Calories { get; set; }

    public double? Protein { get; set; }

    public double? Fat { get; set; }

    public double? Carbs { get; set; }

    public List<Meal> Meals { get; set; } = [];

    public Food ToFoodEntity()
    {
        return new Food
        {
            Name = Name,
            Category = Category.ToString(),
            Calories = Calories,
            Protein = Protein,
            Fat = Fat,
            Carbs = Carbs,
            Meals = Meals,
        };
    }
}
