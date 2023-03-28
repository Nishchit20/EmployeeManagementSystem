using System.ComponentModel.DataAnnotations;

namespace EmployeeManagementSystem.Models
{
    /// <summary>
    /// This Entity table is Used for Login 
    /// VIEW MODEL
    /// </summary>
    public class LoginModel
    {
        [Required]
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
