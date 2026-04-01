namespace FoodJournal.Entities;

public abstract class BaseEntity
{
    public int Id { get; set; }

    public int? Calories { get; set; }

    public double? Protein { get; set; }

    public double? Fat { get; set; }

    public double? Carbs { get; set; }
}