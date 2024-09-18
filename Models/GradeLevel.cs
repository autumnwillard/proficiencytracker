using System.ComponentModel.DataAnnotations;

namespace ProficiencyTracker.Models 
{
    public class GradeLevel 
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public int SubjectId { get; set; }
        public Subject Subject { get; set; }
        public List<ProficiencyItem> ProficiencyItems { get; set; } = new List<ProficiencyItem>();
    }
}