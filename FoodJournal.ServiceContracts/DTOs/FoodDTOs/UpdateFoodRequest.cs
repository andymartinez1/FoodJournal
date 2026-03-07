using System.ComponentModel.DataAnnotations;
using FoodJournal.Entities;

namespace FoodJournal.ServiceContracts.DTOs.FoodDTOs;

public class UpdateFoodRequest
{
    [Required(ErrorMessage = "ID cannot be empty.")]
    public int FoodId { get; set; }

    [Required(ErrorMessage = "Name cannot be empty.")]
    public string Name { get; set; } = string.Empty;

    public string? Category { get; set; }

    public int? Calories { get; set; }

    public double? Protein { get; set; }

    public double? Fat { get; set; }

    public double? Carbs { get; set; }

    public List<int> MealIds { get; set; } = new();

    public List<Meal> Meals { get; set; }
}