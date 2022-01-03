using System;
using System.ComponentModel.DataAnnotations;

namespace bexio.net.Models.Projects
{
    public class Milestone : IHasPrimaryKey
    {
        public int Id { get; set; } = default;

        [MaxLength(255)]
        public string Name { get; set; } = "";

        public DateTime? EndDate { get; set; }

        [MaxLength(10000)]
        public string? Comment { get; set; }

        public int? PrParentMilestoneId { get; set; }
    }
}
