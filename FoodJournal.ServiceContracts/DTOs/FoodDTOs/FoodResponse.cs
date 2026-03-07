using System.ComponentModel.DataAnnotations;
using FoodJournal.Entities;

namespace FoodJournal.ServiceContracts.DTOs.FoodDTOs;

public class FoodResponse
{
    [Display(Name = "Food ID")] public int FoodId { get; set; }

    public string Name { get; set; } = string.Empty;

    public string? Category { get; set; }

    public int? Calories { get; set; }

    public double? Protein { get; set; }

    public double? Fat { get; set; }

    public double? Carbs { get; set; }

    public List<Meal> Meals { get; set; } = new();
}