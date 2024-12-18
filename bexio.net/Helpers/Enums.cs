namespace bexio.net.Helpers;

internal static class Enums
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
    
    /// <summary>
    /// Possible values:<br/>
    /// "=" "equal" "!=" "not_equal"
    /// ">" "greater_than" ">=" "greater_equal"
    /// "&lt;" "less_than" "&lt;=" "less_equal"
    /// "like" "not_like"
    /// "is_null" "not_null"
    /// "in" "not_in"
    /// </summary>
    public enum SearchCriteria
    {
        like,
        not_like,
        is_null,
        not_null,
        equal,
        not_equal,
        greater_than,
        greater_equal,
        less_than,
        less_equal
    }
}