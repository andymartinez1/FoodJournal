namespace FoodJournal.ServiceContracts.DTOs.ReportDTOs;

public class MealConsumptionReportRequest
{
    public int MealId { get; set; }

    public DateOnly FromDate { get; set; }

    public DateOnly ToDate { get; set; }
}
