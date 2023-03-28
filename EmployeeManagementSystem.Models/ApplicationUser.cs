using Microsoft.AspNetCore.Identity;

namespace EmployeeManagementSystem.Models
{
    /// <summary>
    /// This table will have the additionala properties which we need to use in Identity table
    /// </summary>
    public class ApplicationUser : IdentityUser 
    {
        
        public string  Name { get; set; }
        public string ? ProfilePicture { get; set; }
        public  int? Salary { get; set; }
        public string ? Role { get; set; }
    }
}
