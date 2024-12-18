namespace bexio.net.Helpers;

public static class Enums
{
    // Based on Bexio API documentation values
    public enum ValidPrInvoiceType
    {
        TypeHourlyRateService	= 1,
        TypeHourlyRateEmployee = 2,
        TypeHourlyRateProject	= 3, 
        TypeFix	= 4
    }
    
    // Based on Bexio API documentation values
    public enum ValidPrBudgetTypeId
    {
        TypeBudgetedCosts	= 1,
        TypeBudgetedHours = 2,
        TypeServiceBudget	= 3, 
        TypeServiceEmployees	= 4
    }
}