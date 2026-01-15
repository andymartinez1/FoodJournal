namespace FoodJournal.Entities;

public class Meal
{
    public int MealId { get; set; }

    public string Name { get; set; } = string.Empty;

    public string? Description { get; set; }

    public MealType MealType { get; set; }

    public int? Calories { get; set; }

    public double? Protein { get; set; }

    public double? Fat { get; set; }

    public double? Carbs { get; set; }

    public List<Food> Ingredients { get; set; }
}

public enum MealType
{
    Breakfast,
    Lunch,
    Dinner,
    Snack,
}
