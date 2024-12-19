using bexio.net.Models.Projects;

namespace bexio.net.Example.Options;

internal static class BusinessActivitiesOption
{
    public static async Task ShowBusinessActivities(ApiBexio.BexioApi bexioApi)
    {
        Console.WriteLine("Business Activities:");

        foreach (BusinessActivity ba in (await bexioApi.Project.GetBusinessActivitiesAsync())!)
        {
            Console.WriteLine($"{ba.Id}: {ba.Name} \t {ba.DefaultPricePerHour}");
        }
    }
}