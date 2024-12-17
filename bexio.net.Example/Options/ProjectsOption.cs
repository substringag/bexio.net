using bexio.net.Models;
using bexio.net.Models.Projects;

namespace bexio.net.Example.Options;

public static class ProjectsOption
{
    /**
     * https://docs.bexio.com/#tag/Projects/operation/v2ListProjects
     */
    public static async Task ShowProjectsAndMilestones(ApiBexio.BexioApi bexioApi)
    {
        Console.WriteLine("Projects:");
        foreach (Project project in (await bexioApi.Project.GetProjectsAsync())!)
        {
            Console.WriteLine($"{project.Id}: {project.Uuid} {project.Name}");
            Console.WriteLine("Project milestones:");
            foreach (Milestone milestone in (await bexioApi.Project.GetProjectMilestonesAsync((int)project.Id!))!)
            {
                Console.WriteLine($"{milestone.Id}: {milestone.Name}");
            }
        }
    }
    
    /**
     * https://docs.bexio.com/#tag/Projects/operation/v2CreateProject
     */
    public static async Task CreateProject(ApiBexio.BexioApi bexioApi)
    {
        Console.WriteLine("Create a project:");
        
        var apiModels = new ApiModels
        {
            Project = new Project
            {
                Name = "TEST PROJECT WABIA3",
                StartDate = DateTime.Now,
                EndDate = null,
                Comment = string.Empty,
                PrStateId = 1,
                PrProjectTypeId = 2,
                ContactId = 54,
                PrInvoiceTypeId = 3,
                PrInvoiceTypeAmount = 230.00m,
                UserId = 13
            }
        };
        
        Project? response = await bexioApi.Project.CreateProjectAsync(apiModels.Project);
        
        if (response != null)
        {
            Console.WriteLine("Project Created Successfully:");
            Console.WriteLine($"ID: {response.Id}");
            Console.WriteLine($"Name: {response.Name}");
            Console.WriteLine($"Start Date: {response.StartDate}");
            Console.WriteLine($"End Date: {response.EndDate?.ToString("yyyy-MM-dd") ?? "N/A"}"); // Handle nullable EndDate
            Console.WriteLine($"Comment: {response.Comment ?? "No comment"}");
            Console.WriteLine($"State ID: {response.PrStateId}");
            Console.WriteLine($"Project Type ID: {response.PrProjectTypeId}");
            Console.WriteLine($"Contact ID: {response.ContactId}");
            Console.WriteLine($"Invoice Type ID: {response.PrInvoiceTypeId}");
            Console.WriteLine($"Invoice Type Amount: {response.PrInvoiceTypeAmount}");
            Console.WriteLine($"User ID: {response.UserId}");
        }
        else
        {
            Console.WriteLine("Project creation failed. Response is null.");
        }
    }
}