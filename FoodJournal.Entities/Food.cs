namespace FoodJournal.Entities;

public class Food : BaseEntity
{
    public string Name { get; set; } = string.Empty;

    public string? Category { get; set; }

    public List<Meal> Meals { get; set; } = new();
}