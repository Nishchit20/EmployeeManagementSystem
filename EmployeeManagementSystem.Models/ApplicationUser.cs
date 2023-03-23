using Microsoft.AspNetCore.Identity;

namespace EmployeeManagementSystem.Models
{
    public class ApplicationUser : IdentityUser 
    {
        
        public string  Name { get; set; }
        public string ? ProfilePicture { get; set; }
        public  int? Salary { get; set; }
        public string ? Role { get; set; }
    }
}
