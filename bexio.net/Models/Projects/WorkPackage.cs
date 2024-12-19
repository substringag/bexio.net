using System.ComponentModel.DataAnnotations;

namespace bexio.net.Models.Projects;

public class WorkPackage : IHasPrimaryKey
{
    public int Id { get; set; } = default;

    [MaxLength(255)]
    public string Name { get; set; } = "";

    public decimal? SpentTimeInHours { get; set; }

    public decimal? EstimatedTimeInHours { get; set; }

    [MaxLength(10000)]
    public string? Comment { get; set; }

    public int? PrMilestoneId { get; set; }
}