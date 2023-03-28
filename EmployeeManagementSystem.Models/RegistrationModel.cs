using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace EmployeeManagementSystem.Models
{
    /// <summary>
    /// This Entity table is Used for the Registration
    /// VIEW MODEL
    /// </summary>
    public class RegistrationModel 
    {
        [Required]
        [Display(Name = "Name")]
        public string Name { get; set; }
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }
        [Required]
        [Display(Name = "Employee ID")]
        public string Username { get; set; }
        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string PasswordConfirm { get; set; }
        public string PhoneNumber { get; set; }
        public int Salary { get; set; }

        public string? Role { get; set; }

        public List<SelectListItem> IdentityRoles { get; set; }
        [Display(Name = "Role")]
        public string IdentityRoleId { get; set; }


    }
}
