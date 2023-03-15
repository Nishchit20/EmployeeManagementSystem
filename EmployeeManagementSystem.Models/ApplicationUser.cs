﻿using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagementSystem.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string Name { get; set; }
        public string ? ProfilePicture { get; set; }
        public  int? Salary { get; set; }
    }
}
