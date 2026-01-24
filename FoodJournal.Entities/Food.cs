namespace FoodJournal.Entities;

public class Food
{
    public int FoodId { get; set; }

    public string Name { get; set; } = string.Empty;

    public string? Category { get; set; }

    public int? Calories { get; set; }

    public double? Protein { get; set; }

    public double? Fat { get; set; }

    public double? Carbs { get; set; }

    public List<Meal> Meals { get; set; } = [];
}

public enum FoodCategory
{
    Fruit,
    Vegetable,
    Meat,
    Dairy,
    Grain,
    Seafood,
    Processed,
    Nuts,
    Protein,
}
