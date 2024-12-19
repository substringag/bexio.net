using System.ComponentModel.DataAnnotations;

namespace bexio.net.Models.Projects;

public record Milestone
{
    public int? Id { get; set; }

    [MaxLength(255)]
    public string Name { get; set; } = "";

    public DateTime? EndDate { get; set; }

    [MaxLength(10000)]
    public string? Comment { get; set; }

    public int? PrParentMilestoneId { get; set; }
}