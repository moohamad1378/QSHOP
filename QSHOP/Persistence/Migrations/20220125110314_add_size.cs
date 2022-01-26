using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    public partial class add_size : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Size",
                table: "CatalogItems",
                type: "int",
                nullable: false,
                defaultValue: 0);

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Size",
                table: "CatalogItems");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4",
                column: "ConcurrencyStamp",
                value: "8c94c16e-6d2a-4fe3-8d9f-08ece568f70f");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5",
                column: "ConcurrencyStamp",
                value: "870a167a-574b-461c-b832-44f5a33cf89c");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6",
                column: "ConcurrencyStamp",
                value: "fff412e3-4e98-4d80-ab25-1c59bba8c07d");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8e445865-a24d-4543-a6c6-9443d048cdb9",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "783413a1-8601-4938-986c-3274c44b4c8f", "AQAAAAEAACcQAAAAEAKGVi71wEqnMgJ64OOnLr3/eL3ALxG4ftdsfG+gQRQ18TyY7GsTXP8KQ9Kb70ybvw==", "b94105c1-f4da-44bc-829a-e23ce893a9ec" });
        }
    }
}
