using FoodJournal.ServiceContracts.DTOs.ReportDTOs;

namespace FoodJournal.ServiceContracts;

public interface IReportService
{
    Task LogFoodConsumptionAsync(int foodId, DateOnly consumedOn);

    Task<FoodConsumptionReportResponse> GetFoodConsumptionReportAsync(
        FoodConsumptionReportRequest request
    );
}
