using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    public partial class add_admin : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "4", "f2ac7301-5973-4af4-8108-e8229d76ade2", "Admin", null },
                    { "5", "530baf75-cb98-481c-be74-7b7fddcd17da", "Operator", null },
                    { "6", "5525e5b2-22ff-4498-99ab-0ec8b7d7171b", "Customer", null }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "abcd1-abcd2-abcd3-abcd4", 0, "aaab4477-7be5-4e63-b629-0935ffd9c84d", "Qasemiyan.mostafa@gmail.com", false, false, null, "QASEMIYAN.MOSTAFA@GMAIL.COM", "ADMIN", "Admin", null, false, "315876fc-32c3-4a07-9c3a-855d41525235", false, "Admin" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "4", "abcd1-abcd2-abcd3-abcd4" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "4", "abcd1-abcd2-abcd3-abcd4" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "abcd1-abcd2-abcd3-abcd4");
        }
    }
}
