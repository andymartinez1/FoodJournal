namespace FoodJournal.Entities;

public class FoodMeal
{
    public int FoodId { get; set; }

    public Food Food { get; set; }

    public int MealId { get; set; }

    public Meal Meal { get; set; }
}