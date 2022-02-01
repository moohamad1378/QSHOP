using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    public partial class add_slug : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Slug",
                table: "CatalogItems",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4",
                column: "ConcurrencyStamp",
                value: "2a69b3da-1d2d-4923-b631-e11cd2b0576b");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5",
                column: "ConcurrencyStamp",
                value: "1ea94fcf-ccb1-4488-90b4-90000014a6bc");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6",
                column: "ConcurrencyStamp",
                value: "8f636743-ae00-4e4b-bd0b-db42f1f4ce0f");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8e445865-a24d-4543-a6c6-9443d048cdb9",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "d314b851-425b-4d84-8e25-a40248d9fbf8", "AQAAAAEAACcQAAAAEGkBvdGuD9gNH98XQaP2TbAS8Wvh8xRbsBkp7fMPRpn8GCijpiiozv9sLZuHZnyBlw==", "54880564-9940-403f-8543-01fb144a44ac" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Slug",
                table: "CatalogItems");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4",
                column: "ConcurrencyStamp",
                value: "0096cfc3-5374-4852-b613-72e8b849c01d");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5",
                column: "ConcurrencyStamp",
                value: "aca4be99-44a4-4635-802f-17528653dbd4");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6",
                column: "ConcurrencyStamp",
                value: "f72dfa89-3bbc-40a2-acb0-0cdad339b53a");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8e445865-a24d-4543-a6c6-9443d048cdb9",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "1ed92728-1125-4944-aeae-0e5aa2cf6418", "AQAAAAEAACcQAAAAEFXp3gBziXBDIsid1EaCIydlsa59RFeXxRImYYKEYLoYK0wmTFbGvaVQJxKpWnN7Ww==", "2198e869-39f6-4f06-bec3-be2bb3c77bda" });
        }
    }
}
