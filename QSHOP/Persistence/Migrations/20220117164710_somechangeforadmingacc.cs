using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    public partial class somechangeforadmingacc : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "4", "abcd1-abcd2-abcd3-abcd4" });

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "abcd1-abcd2-abcd3-abcd4");

            migrationBuilder.CreateTable(
                name: "CatalogBrands",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Brand = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CatalogBrands", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CatalogTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ParentCatalogTypeId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CatalogTypes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CatalogTypes_CatalogTypes_ParentCatalogTypeId",
                        column: x => x.ParentCatalogTypeId,
                        principalTable: "CatalogTypes",
                        principalColumn: "Id");
                });

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

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "8e445865-a24d-4543-a6c6-9443d048cdb9", 0, "37fd2616-6990-496c-9d0e-3d1b4c1a8f0c", "Qasemiyan.mostafa@gmail.com", false, false, null, "QASEMIYAN.MOSTAFA@GMAIL.COM", "ADMIN", "AQAAAAEAACcQAAAAECFeBZzmT2MRveD4xoQBAzKTFpJFG6RbD62G8CxEiSvDMJINdHdCDqfA91vTHm9FRg==", null, false, "0f37d355-0e3b-4b7d-857f-dffc3b715a74", false, "Admin" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "4", "8e445865-a24d-4543-a6c6-9443d048cdb9" });

            migrationBuilder.CreateIndex(
                name: "IX_CatalogTypes_ParentCatalogTypeId",
                table: "CatalogTypes",
                column: "ParentCatalogTypeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CatalogBrands");

            migrationBuilder.DropTable(
                name: "CatalogTypes");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "4", "8e445865-a24d-4543-a6c6-9443d048cdb9" });

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8e445865-a24d-4543-a6c6-9443d048cdb9");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4",
                column: "ConcurrencyStamp",
                value: "f2ac7301-5973-4af4-8108-e8229d76ade2");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5",
                column: "ConcurrencyStamp",
                value: "530baf75-cb98-481c-be74-7b7fddcd17da");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6",
                column: "ConcurrencyStamp",
                value: "5525e5b2-22ff-4498-99ab-0ec8b7d7171b");

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "abcd1-abcd2-abcd3-abcd4", 0, "aaab4477-7be5-4e63-b629-0935ffd9c84d", "Qasemiyan.mostafa@gmail.com", false, false, null, "QASEMIYAN.MOSTAFA@GMAIL.COM", "ADMIN", "Admin", null, false, "315876fc-32c3-4a07-9c3a-855d41525235", false, "Admin" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "4", "abcd1-abcd2-abcd3-abcd4" });
        }
    }
}
