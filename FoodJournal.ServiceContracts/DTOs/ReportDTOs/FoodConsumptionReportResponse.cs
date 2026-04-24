namespace FoodJournal.ServiceContracts.DTOs.ReportDTOs;

public class FoodConsumptionReportResponse
{
    public int FoodId { get; set; }

    public string FoodName { get; set; } = string.Empty;

    public DateOnly FromDate { get; set; }

    public DateOnly ToDate { get; set; }

    public int TimesConsumed { get; set; }
}