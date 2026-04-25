namespace FoodJournal.ServiceContracts.DTOs.ReportDTOs;

public class MealConsumptionReportResponse
{
    public int MealId { get; set; }

    public string MealName { get; set; } = string.Empty;

    public DateOnly FromDate { get; set; }

    public DateOnly ToDate { get; set; }

    public int TimesConsumed { get; set; }
}