namespace FoodJournal.Entities;

public class MealConsumptionEntry
{
    public int Id { get; set; }

    public int MealId { get; set; }

    public Meal Meal { get; set; } = null!;

    public DateOnly ConsumedOn { get; set; }
}