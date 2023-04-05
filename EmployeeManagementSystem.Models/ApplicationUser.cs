using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace EmployeeManagementSystem.Models
{
    /// <summary>
    /// This table will have the additional properties which we need to use in Identity table
    /// </summary>
    public class ApplicationUser : IdentityUser 
    {
        
        [Required]
        public string  Name { get; set; }
        public string ? ProfilePicture { get; set; }
        public  int? Salary { get; set; }
        public string ? Role { get; set; }
    }
}
