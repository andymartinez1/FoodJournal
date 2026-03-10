namespace FoodJournal.Helpers;

public static class ImagePathHelper
{
    public static string GetImagePath(string name)
    {
        var path = name.ToLower().Replace(" ", "-");

        return $"{path}.png";
    }
}