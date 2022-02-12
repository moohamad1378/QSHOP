using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    public partial class add_our_News : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsVizhe",
                table: "CatalogItems",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<long>(
                name: "Visit",
                table: "CatalogItems",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4",
                column: "ConcurrencyStamp",
                value: "e2183502-cdfe-49d6-829f-526711169a7b");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5",
                column: "ConcurrencyStamp",
                value: "d186c48d-686e-4e88-929c-5c49c9a29da7");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6",
                column: "ConcurrencyStamp",
                value: "62a35239-ad83-4bac-aa79-4ee68bbce74e");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8e445865-a24d-4543-a6c6-9443d048cdb9",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "f1f7df87-6ddf-4500-9379-7fa280a5dde4", "AQAAAAEAACcQAAAAEI8tNkS9y6MV65up9WF6OWJ0KvndyjLQkwFiLsTW5DPLAh/VdLBrrhfiOyWc/vWhlQ==", "3ddd14d6-7ed8-4ed2-9c60-cee065d0fc29" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsVizhe",
                table: "CatalogItems");

            migrationBuilder.DropColumn(
                name: "Visit",
                table: "CatalogItems");

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
    }
}
