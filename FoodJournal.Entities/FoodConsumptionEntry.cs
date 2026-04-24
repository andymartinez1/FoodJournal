namespace FoodJournal.Entities;

public class FoodConsumptionEntry
{
    public int Id { get; set; }

    public int FoodId { get; set; }

    public Food Food { get; set; } = null!;

    public DateOnly ConsumedOn { get; set; }
}
