using System.ComponentModel.DataAnnotations;

namespace ProficiencyTracker.Models 
{
    public class Subject 
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public List<GradeLevel> GradeLevels { get; set; } = new List<GradeLevel>();
    }
}