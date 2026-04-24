namespace FoodJournal.ServiceContracts.DTOs.ReportDTOs;

public class FoodConsumptionReportRequest
{
    public int FoodId { get; set; }

    public DateOnly FromDate { get; set; }

    public DateOnly ToDate { get; set; }
}