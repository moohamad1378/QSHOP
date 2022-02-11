using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    public partial class add_name_sliders : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "NameTage1",
                table: "Sliders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "NameTage2",
                table: "Sliders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "NameTage3",
                table: "Sliders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4",
                column: "ConcurrencyStamp",
                value: "dfe6ac1c-2441-4e22-8f2a-311487cbbe1b");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5",
                column: "ConcurrencyStamp",
                value: "ff728fd2-039a-46c3-ab7a-36b084de4ca6");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6",
                column: "ConcurrencyStamp",
                value: "23df5763-976f-405f-bcad-7077ff858f2b");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8e445865-a24d-4543-a6c6-9443d048cdb9",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "b460fd9d-f10a-4974-86d1-e4519e7538e2", "AQAAAAEAACcQAAAAEFaCN0xdVXeI7+HouNcbV95FW586+vNug5YC1un5dyEkM/oQ3OXZlV5ONRUhDYwj3g==", "6ac02095-152c-4ab9-8cda-e1f170870c73" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NameTage1",
                table: "Sliders");

            migrationBuilder.DropColumn(
                name: "NameTage2",
                table: "Sliders");

            migrationBuilder.DropColumn(
                name: "NameTage3",
                table: "Sliders");

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
    }
}
