using bexio.net.Models.Contacts;
using bexio.net.Models.Projects;

namespace bexio.net.Models;

public class ApiModels
{
    public Project Project { get; set; } = null!;
    public SearchQuery SearchQuery { get; set; } = null!;
    public Contact Contact { get; set; } = null!;
}