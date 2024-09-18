using System.ComponentModel.DataAnnotations;

namespace ProficiencyTracker.Models 
{
    public class StudentProgress 
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public Student Student { get; set; } = null!;
        public int ProficiencyItemId { get; set; }
        public ProficiencyItem ProficiencyItem { get; set; } = null!;
        public bool IsComplete { get; set; } = false;
    }
}