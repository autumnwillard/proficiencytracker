using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace ProficiencyTracker.Models
{
    public class User : IdentityUser
    {
        public User()
        {
            Students = new List<Student>();
        }
        
        [Required]
        public List<Student> Students { get; set; }
    }
}