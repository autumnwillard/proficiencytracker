using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;
using ProficiencyTracker.Attributes;

namespace ProficiencyTracker.Models 
{
    public class Student 
    {
        public Student(string name, int userId, string? notes = null, string? photoPath = null) 
        {
            Name = name;
            UserId = userId;
            Notes = notes;
            PhotoPath = photoPath;
            Progress = new List<StudentProgress>();
        }
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        
        public string? Notes { get; set; }
        public int UserId { get; set; }
        public User User { get; set; } = null!;

        string? PhotoPath { get; set; }
        [NotMapped]
        [Display(Name = "Profile Picture")]
        [MaxFileSize(5 * 1024 * 1024)] // 5 MB
        [AllowedExtensions(new string[] { ".jpg", ".png", ".gif" })]
        public IFormFile? PhotoFile { get; set; }
        public List<StudentProgress> Progress { get; set; } = new List<StudentProgress>();
    }
}