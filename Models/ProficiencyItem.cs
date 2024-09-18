using System.ComponentModel.DataAnnotations;

namespace ProficiencyTracker.Models 
{
    public class ProficiencyItem 
    {
        public int Id { get; set; }
        [Required]
        public string Description { get; set; }
        public int GradeLevelId { get; set; }
        public GradeLevel GradeLevel { get; set; } = null!;
    }
}