using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    public partial class add_name_slider : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4",
                column: "ConcurrencyStamp",
                value: "e29ec1c6-b848-4326-8daa-8e399eeda3e6");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5",
                column: "ConcurrencyStamp",
                value: "6aa37676-f8db-40f2-aab4-5e1217080d48");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6",
                column: "ConcurrencyStamp",
                value: "96b22e88-e10b-469d-9de7-19af4be001a6");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8e445865-a24d-4543-a6c6-9443d048cdb9",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "283ca935-b427-484d-ad37-f5109cd2ba17", "AQAAAAEAACcQAAAAEDlQp0zYtlCGNWEzIZR0+RmxlMMHmaW325mj4uc1KpV7OSw6FfA9Hk4IT+LKD+d0TA==", "b1270cbc-d45d-423e-9c8c-5b60df507d28" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4",
                column: "ConcurrencyStamp",
                value: "33f995ee-27f4-4a91-aeb9-6cfbf1050a2f");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5",
                column: "ConcurrencyStamp",
                value: "0bcf29d8-9097-49e0-9776-d5430034f6e2");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6",
                column: "ConcurrencyStamp",
                value: "8121dae0-3da0-4669-9030-e67dfe85cbbb");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8e445865-a24d-4543-a6c6-9443d048cdb9",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "398f415a-60b2-41f8-b027-3afa8a0f21b1", "AQAAAAEAACcQAAAAEOh8VBk4PsbHyp7W9i0eVeCc3mQiz+vwfuDRnAy+xhyAI0qEXoNI62fhVJ2IFV4P+w==", "db83f08c-487f-4206-948f-a79f32e8b33f" });
        }
    }
}
