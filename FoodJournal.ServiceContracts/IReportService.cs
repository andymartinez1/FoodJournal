using FoodJournal.ServiceContracts.DTOs.ReportDTOs;

namespace FoodJournal.ServiceContracts;

public interface IReportService
{
    Task LogFoodConsumptionAsync(int foodId, DateOnly consumedOn);
    Task LogMealConsumptionAsync(int mealId, DateOnly consumedOn);

    Task<FoodConsumptionReportResponse> GetFoodConsumptionReportAsync(
        FoodConsumptionReportRequest request
    );

    Task<MealConsumptionReportResponse> GetMealConsumptionReportAsync(
        MealConsumptionReportRequest request
    );
}
