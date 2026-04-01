namespace FoodJournal.Entities;

public class Meal : BaseEntity
{
    public string Name { get; set; } = string.Empty;

    public string? Description { get; set; }

    public MealType MealType { get; set; }

    public bool IsFavorite { get; set; }

    public int TimesEaten { get; set; }

    public DateOnly LastDayEaten { get; set; }

    public string? ImagePath { get; set; }

    public List<Food> Ingredients { get; set; } = new();
}

public enum MealType
{
    Breakfast,
    Lunch,
    Dinner,
    Snack
}