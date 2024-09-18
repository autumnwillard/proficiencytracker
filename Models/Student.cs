
using System.ComponentModel.DataAnnotations;

namespace ProficiencyTracker.Models 
{
    public class Student 
    {
        public Student(string name, int userId, string? notes = null) 
        {
            Name = name;
            UserId = userId;
            Notes = notes;
        }
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        
        public string? Notes { get; set; }
        public int UserId { get; set; }
        public User User { get; set; } = null!;

    }
}