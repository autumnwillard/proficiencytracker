using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography.X509Certificates;

namespace ProficiencyTracker.Models
{
    public class User : IdentityUser
    {
        public User()
        {
            Students = new List<Student>();
        }

        public virtual ICollection<Student> Students { get; set; }
    }
}