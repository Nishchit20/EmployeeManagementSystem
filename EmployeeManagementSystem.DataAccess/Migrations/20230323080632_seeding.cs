using Microsoft.EntityFrameworkCore.Migrations;

namespace EmployeeManagementSystem.Migrations
{
    public partial class seeding : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b74ddd14-6340-4840-95c2-db12554843e1");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "b74ddd14-6340-4840-95c2-db12554843e1");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "2c5e174e-3b0e-446f-86af-483d56fd7210", "63a21078-e22c-44cb-a6b9-1732151e762c", "admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "Name", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "ProfilePicture", "Role", "Salary", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "8e445865-a24d-4543-a6c6-9443d048cdb9", 0, "d853afa4-7b9e-44aa-a2a8-e8cb41409bf4", "adminevry22@gmail.com", true, false, null, "Admin", "ADMINEVRY22@GMAIL.COM", "EIA00001", "AQAAAAEAACcQAAAAEAqr7f5EP9FHLKDt1ApYqHxDEz/i/1zUhsa6bvA0GIZdfxGYjo0PvIhD22fS0rkSNw==", "9741364080", false, null, "AdminUser", null, "135bd84a-593f-44c1-b5ac-9dca9df1328e", false, "EIA00001" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "UserId", "RoleId" },
                values: new object[] { "8e445865-a24d-4543-a6c6-9443d048cdb9", "2c5e174e-3b0e-446f-86af-483d56fd7210" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "UserId", "RoleId" },
                keyValues: new object[] { "8e445865-a24d-4543-a6c6-9443d048cdb9", "2c5e174e-3b0e-446f-86af-483d56fd7210" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2c5e174e-3b0e-446f-86af-483d56fd7210");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8e445865-a24d-4543-a6c6-9443d048cdb9");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "b74ddd14-6340-4840-95c2-db12554843e1", "63a21078-e22c-44cb-a6b9-1732151e762c", "owner", "OWNER" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "Name", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "ProfilePicture", "Role", "Salary", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "b74ddd14-6340-4840-95c2-db12554843e1", 0, "f5bebc3a-ac39-4217-8034-3b2c588ad945", "adminevry22@gmail.com", true, false, null, "Admin", "ADMINEVRY22@GMAIL.COM", "EIA00001", "AQAAAAEAACcQAAAAEB7KCkRiHKOZ8NlyiB6vmyXpiLNZYbe2Ai3jrJYK33Fo0y/QfiToeajPT5kQ4OYwmQ==", "9741364080", false, null, "AdminUser", null, "7a9105c1-6316-491c-8257-3bd401d3f065", false, "EIA00001" });
        }
    }
}
