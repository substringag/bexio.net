using System.ComponentModel.DataAnnotations;
using bexio.net.Models;
using bexio.net.Models.Projects;

namespace bexio.net.Example.Options;

internal static class ProjectsOption
{
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
    
    public static async Task CreateProject(ApiBexio.BexioApi bexioApi)
    {
        Console.WriteLine("Create a project:");
        
        var apiModels = new ApiModels
        {
            Project = new Project
            {
                Name = "TEST PROJECT WABIA",
                StartDate = DateTime.Now,
                EndDate = null,
                Comment = string.Empty,
                PrStateId = 1,
                PrProjectTypeId = 2,
                ContactId = 54,
                PrInvoiceTypeId = 8,
                PrInvoiceTypeAmount = 230.00m,
                UserId = 13
            }
        };
        
        var validationResults = new List<ValidationResult>();
        var context = new ValidationContext(apiModels.Project);
        bool isValid = Validator.TryValidateObject(apiModels.Project, context, validationResults, true);
        
        if (!isValid)
        {
            foreach (ValidationResult validationResult in validationResults)
            {
                Console.WriteLine(validationResult.ErrorMessage);
            }
        }
        else
        {
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
    
    public static async Task SearchProjects(ApiBexio.BexioApi bexioApi) {
        Console.WriteLine("Search projects:");

        var apiModels = new ApiModels
        {
            SearchQuery = new SearchQuery
            {
                Field = "name",
                Value = "WABIA",
                Criteria = "like"
            }
        };

        var validationResults = new List<ValidationResult>();
        var context = new ValidationContext(apiModels.SearchQuery);
        bool isValid = Validator.TryValidateObject(apiModels.SearchQuery, context, validationResults, true);
        
        if (!isValid)
        {
            foreach (ValidationResult validationResult in validationResults)
            {
                Console.WriteLine(validationResult.ErrorMessage);
            }
        }
        else
        {
            List<Project>? response = await bexioApi.Project.SearchProjectsAsync([apiModels.SearchQuery]);
            
            if (response != null)
            {
                Console.WriteLine("Projects Found:");
                foreach (Project project in response)
                {
                    Console.WriteLine($"{project.Id}: {project.Uuid} {project.Name}");
                }
            }
            else
            {
                Console.WriteLine("No projects found. Response is null.");
            }
        }
    }

    public static async Task GetProject(ApiBexio.BexioApi bexioApi)
    {
        Console.WriteLine("Get a project:");
        
        Project? response = await bexioApi.Project.GetProjectAsync(16);

        if (response != null)
        {
            Console.WriteLine("Project Found:");
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
            Console.WriteLine("No project found. Response is null.");
        }
    }

    public static async Task GetProjectStatuses(ApiBexio.BexioApi bexioApi)
    {
        Console.WriteLine("Get a project statuses:");
        
        List<SimpleDictionaryEntry>? response = await bexioApi.Project.GetProjectStatusesAsync();
        
        if (response != null)
        {
            Console.WriteLine("Project Statuses:");
            foreach (SimpleDictionaryEntry entry in response)
            {
                Console.WriteLine($"{entry.Id}: {entry.Name}");
            }
        }
        else
        {
            Console.WriteLine("No project statuses found. Response is null.");
        }
    }

    public static async Task GetProjectTypes(ApiBexio.BexioApi bexioApi)
    {
        Console.WriteLine("Get a project types:");
        
        List<SimpleDictionaryEntry>? response = await bexioApi.Project.GetProjectTypesAsync();
        
        if (response != null)
        {
            Console.WriteLine("Project Types:");
            foreach (SimpleDictionaryEntry entry in response)
            {
                Console.WriteLine($"{entry.Id}: {entry.Name}");
            }
        }
        else
        {
            Console.WriteLine("No project types found. Response is null.");
        }
    }
}