using bexio.net.Models;
using bexio.net.Models.Projects.Timesheet;

namespace bexio.net.Example.Options;

internal static class TimesheetsOption
{
    public static async Task GetTimesheets(ApiBexio.BexioApi bexioApi)
    {
        Console.WriteLine("Timesheets:");
        
        foreach (TimesheetFetched timesheet in (await bexioApi.Project.GetTimesheetsAsync())!)
        {
            Console.WriteLine($"Id: {timesheet.Id}");
            Console.WriteLine($"Date: {timesheet.Date}");
            Console.WriteLine($"Duration: {timesheet.Duration}");
            Console.WriteLine($"Running: {timesheet.Running}");
            Console.WriteLine($"TravelTime: {timesheet.TravelTime}");
            Console.WriteLine($"TravelCharge: {timesheet.TravelCharge}");
            Console.WriteLine($"TravelDistance: {timesheet.TravelDistance}");
            Console.WriteLine();
        }
    }

    public static async Task GetTimesheet(ApiBexio.BexioApi bexioApi)
    {
        Console.WriteLine("Timesheet:");
        
        TimesheetFetched? timesheet = await bexioApi.Project.GetTimesheetAsync(514);
        
        if (timesheet is not null)
        {
            Console.WriteLine($"Id: {timesheet.Id}");
            Console.WriteLine($"Date: {timesheet.Date}");
            Console.WriteLine($"Duration: {timesheet.Duration}");
            Console.WriteLine($"Running: {timesheet.Running}");
            Console.WriteLine($"TravelTime: {timesheet.TravelTime}");
            Console.WriteLine($"TravelCharge: {timesheet.TravelCharge}");
            Console.WriteLine($"TravelDistance: {timesheet.TravelDistance}");
        } 
        else
        {
            Console.WriteLine("Timesheet not found");
        }
    }

    public static async Task GetTimesheetStatus(ApiBexio.BexioApi bexioApi)
    {
        Console.WriteLine("Timesheet Status:");
        
        foreach (SimpleDictionaryEntry status in (await bexioApi.Project.GetTimesheetStatusAsync())!)
        {
            Console.WriteLine($"Id: {status.Id}");
            Console.WriteLine($"Name: {status.Name}");
            Console.WriteLine();
        }
    }
}