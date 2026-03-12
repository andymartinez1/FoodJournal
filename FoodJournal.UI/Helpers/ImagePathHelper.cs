namespace FoodJournal.Helpers;

public static class ImagePathHelper
{
    public static string GetMealImagePath(string name)
    {
        var meal = name.ToLower().Replace(" ", "-");

        return $"img/{meal}.png";
    }
}