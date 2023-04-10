using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagementSystem.Models.Domain
{
    /// <summary>
    /// DBContext file 
    /// </summary>
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }



        /// <summary>
        /// Data seeding entity framework core has been performed to add the admin detail
        /// </summary>
        /// <param name="builder"></param>
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            this.SeedingUser(builder);
        }

        private void SeedingUser(ModelBuilder builder)
        {

            /// <summary>
            /// Values added to Identity Roles Table
            /// </summary>
            builder.Entity<IdentityRole>().HasData(new IdentityRole { Name = "admin", NormalizedName = "ADMIN", Id = "2c5e174e-3b0e-446f-86af-483d56fd7210", ConcurrencyStamp = "63a21078-e22c-44cb-a6b9-1732151e762c" });

            /// <summary>
            /// Values added to the ApplicationUser Table which is inheriting the properties of IdentityUser also
            /// </summary>
            var hasher = new PasswordHasher<ApplicationUser>();
            ApplicationUser adminUser = new ApplicationUser()
            {
                Id = "8e445865-a24d-4543-a6c6-9443d048cdb9",
                UserName = "EIA00001",
                NormalizedUserName = "EIA00001",
                Email = "adminevry22@gmail.com",
                NormalizedEmail = "ADMINEVRY22@GMAIL.COM",
                PasswordHash = hasher.HashPassword(null, "EvryAdmin@20"),
                LockoutEnabled = false,
                PhoneNumber = "9741364080",
                Name = "Admin",
                Role = "AdminUser",
                EmailConfirmed = true,
            };


            builder.Entity<ApplicationUser>().HasData(adminUser);

            /// <summary>
            /// Values added to the IdentityUserTable
            /// </summary>
            builder.Entity<IdentityUserRole<string>>().HasData(
            new IdentityUserRole<string>
            {
                RoleId = "2c5e174e-3b0e-446f-86af-483d56fd7210",
                UserId = "8e445865-a24d-4543-a6c6-9443d048cdb9"
            }
        );
        }
        }
}
