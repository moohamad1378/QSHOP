using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    public partial class add_catalogType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4",
                column: "ConcurrencyStamp",
                value: "148b3c6b-b3fa-4a66-8788-3071d418d036");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5",
                column: "ConcurrencyStamp",
                value: "017c556b-c119-4d7b-ae69-3538c99295e6");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6",
                column: "ConcurrencyStamp",
                value: "69e9cfc7-e8c2-4605-8e52-589e4473f059");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8e445865-a24d-4543-a6c6-9443d048cdb9",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "d3003c15-0dc2-412b-95b4-1492c99b91aa", "AQAAAAEAACcQAAAAENmKTFXm+uL34Ty1x4ooGcr7ZH8/mQY2yJlMeIp5HwPPV7npIqwu5FxGvzq+xaNSzg==", "525f2fad-fbab-4354-be1b-7f518cc68040" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4",
                column: "ConcurrencyStamp",
                value: "4278733c-2ad7-423b-b38e-82ea4da6b208");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5",
                column: "ConcurrencyStamp",
                value: "d9f22697-07b1-4207-8755-567dacb59d0a");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6",
                column: "ConcurrencyStamp",
                value: "a02f8178-de9c-4d36-a3a7-66aaacf2551a");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8e445865-a24d-4543-a6c6-9443d048cdb9",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "37fd2616-6990-496c-9d0e-3d1b4c1a8f0c", "AQAAAAEAACcQAAAAECFeBZzmT2MRveD4xoQBAzKTFpJFG6RbD62G8CxEiSvDMJINdHdCDqfA91vTHm9FRg==", "0f37d355-0e3b-4b7d-857f-dffc3b715a74" });
        }
    }
}
