using System.ComponentModel.DataAnnotations;
using FoodJournal.Entities;
using FoodJournal.ServiceContracts.Enums;

namespace FoodJournal.ServiceContracts.DTOs;

public class AddFoodRequest
{
    [Required(ErrorMessage = "Name cannot be empty.")]
    public string Name { get; set; } = string.Empty;

    public FoodCategory Category { get; set; }

    public int? Calories { get; set; }

    [Display(Name = "Protein(g)")] public double? Protein { get; set; }

    [Display(Name = "Fat(g)")] public double? Fat { get; set; }

    [Display(Name = "Carbs(g)")] public double? Carbs { get; set; }

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
            Meals = Meals
        };
    }
}