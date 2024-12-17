using bexio.net.Models.Projects;

namespace bexio.net.Example.Options;

public static class ProjectsOption
{
    public static async Task ShowProjects(ApiBexio.BexioApi bexioApi)
    {
        Console.WriteLine("Projects:");
        foreach (Project project in (await bexioApi.Project.GetProjectsAsync())!)
        {
            Console.WriteLine($"{project.Id}: {project.Uuid} {project.Name}");
            Console.WriteLine("Project milestones:");
            foreach (Milestone milestone in (await bexioApi.Project.GetProjectMilestonesAsync(project.Id))!)
            {
                Console.WriteLine($"{milestone.Id}: {milestone.Name}");
            }
        }
    }
}