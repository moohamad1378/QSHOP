using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    public partial class aaaa : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4",
                column: "ConcurrencyStamp",
                value: "a7490f91-1d3f-4424-81a1-3e955762e22f");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5",
                column: "ConcurrencyStamp",
                value: "c143aa52-189f-4148-9cf0-cd55508dad0b");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6",
                column: "ConcurrencyStamp",
                value: "794d281a-b8bd-4620-9388-9c7a207d0fad");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8e445865-a24d-4543-a6c6-9443d048cdb9",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "11130f35-031d-4b63-9bf7-301702bd368b", "AQAAAAEAACcQAAAAEAM1hQTzofpZw+G0H8spdjYw37kI6kvsK7rqiV3f19T7wPuZWFElEVeIRSUwXmvK+A==", "49339e4e-b9f5-443a-b20c-efb2deddd25e" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4",
                column: "ConcurrencyStamp",
                value: "a367622d-5a5f-40fb-887f-02abb8d9d169");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5",
                column: "ConcurrencyStamp",
                value: "2f368eac-7772-450a-8f19-c91bc44e091f");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6",
                column: "ConcurrencyStamp",
                value: "2384cade-9d46-46b0-9882-83089eba9c9e");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8e445865-a24d-4543-a6c6-9443d048cdb9",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "fa588a8d-088f-4768-9b17-0278abcfeeba", "AQAAAAEAACcQAAAAEMTRbvUgR/WeeCs0OqoxBgxagnlCyp0n4KRi5FjFhsGkyBhKx+FmjFnopn+bSEF8mQ==", "b796e060-2879-4c86-bbe6-c3e7382f7f11" });
        }
    }
}
