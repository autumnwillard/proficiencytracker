using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;
using ProficiencyTracker.Attributes;

namespace ProficiencyTracker.Models 
{
    public class Student 
    {
        public int Id { get; set; }
        [Required]
        public required string Name { get; set; }
        
        public string? Notes { get; set; }
        public string UserId { get; set; }
        public virtual User? User { get; set; } = null!;

        public string? PhotoPath { get; set; }
        [NotMapped]
        [Display(Name = "Profile Picture")]
        [MaxFileSize(5 * 1024 * 1024)] // 5 MB
        [AllowedExtensions(new string[] { ".jpg", ".png", ".gif" })]
        public IFormFile? PhotoFile { get; set; }
        public virtual ICollection<StudentProgress> Progress { get; set; } = new List<StudentProgress>();

        public Student()
        {

        }

        public Student(string name, string userId, string? notes = null, string? photoPath = null) 
        {
            Name = name;
            UserId = userId;
            Notes = notes;
            PhotoPath = photoPath;
        }
    }
}