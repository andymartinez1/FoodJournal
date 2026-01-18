using System.ComponentModel.DataAnnotations;
using FoodJournal.Entities;

namespace FoodJournal.ServiceContracts.DTOs;

public class FoodResponse
{
    [Display(Name = "Food ID")]
    public int FoodId { get; set; }

    public string Name { get; set; } = string.Empty;

    public string? Category { get; set; }

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
        return new FoodResponse
        {
            FoodId = food.FoodId,
            Name = food.Name,
            Category = food.Category,
            Calories = food.Calories,
            Protein = food.Protein,
            Fat = food.Fat,
            Carbs = food.Carbs,
            Meals = food.Meals,
        };
    }
}
