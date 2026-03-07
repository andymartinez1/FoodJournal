using FoodJournal.Entities;

namespace FoodJournal.ServiceContracts.DTOs.MealDTOs;

public class UpdateMealRequest
{
    public int MealId { get; set; }

    public string Name { get; set; } = string.Empty;

    public string? Description { get; set; }

    public MealType MealType { get; set; }

    public int? Calories { get; set; }

    public double? Protein { get; set; }

    public double? Fat { get; set; }

    public double? Carbs { get; set; }

    public bool IsFavorite { get; set; }

    public int TimesEaten { get; set; }

    public DateOnly LastDayEaten { get; set; }

    public List<int> IngredientIds { get; set; } = new();

    public List<Food> Ingredients { get; set; } = new();
}