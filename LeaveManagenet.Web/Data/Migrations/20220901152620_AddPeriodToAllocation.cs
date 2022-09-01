using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LeaveManagenet.Web.Data.Migrations
{
    public partial class AddPeriodToAllocation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Period",
                table: "LeaveAllocations",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "cac43a6e-f7bb-4448-baaf-1add431ccbbf",
                column: "ConcurrencyStamp",
                value: "0d0795f3-83d4-4c52-ba63-81b3e8b193a6");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "cac43a7e-f7cb-4148-baaf-1acb431eabbf",
                column: "ConcurrencyStamp",
                value: "650ca3a1-cdcd-417c-a320-383b9f445488");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "3f4631bd-f907-4409-b416-ba356312e659",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "3cad8389-7ff0-4552-b3a5-3229f84e8548", "AQAAAAEAACcQAAAAEMZzH7K51SxT7Q/XFqDIHDpcIk76aWEkvfWvkY6CDmCLEWeafSL9feQYidqRRdGbNw==", "8701c5a8-f027-4042-9f15-f8eb3b56c999" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "408aa945-3d84-4421-8342-7269ec64d949",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "926729ec-bbf5-47a0-b63a-1638e5c063a5", "AQAAAAEAACcQAAAAELThmwAI2sllZ4k2NUMOH1jnDCnr0sUHdlMvNUAD7xZfzu3PnT90KcUzcQx60mPeGQ==", "a49cdac4-7631-4d40-824c-83216bf382cb" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Period",
                table: "LeaveAllocations");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "cac43a6e-f7bb-4448-baaf-1add431ccbbf",
                column: "ConcurrencyStamp",
                value: "725781d3-75f4-488f-842b-03c734f5cc32");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "cac43a7e-f7cb-4148-baaf-1acb431eabbf",
                column: "ConcurrencyStamp",
                value: "63c42de5-f2c5-45bf-913c-0dd50567220b");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "3f4631bd-f907-4409-b416-ba356312e659",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "9d0ee793-41d4-42ed-9f29-7af5ddfc6505", "AQAAAAEAACcQAAAAEE+GMD40zx6KOx0eLS/s0SeGLaeWY3P9MyDwbgq6aZ1bIJJsicAFjWGv6E+0TK9C8A==", "723effb1-dd08-4c9b-a80b-d422e81df79f" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "408aa945-3d84-4421-8342-7269ec64d949",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "73611c2d-6dfc-46fd-963a-61f79c1d0441", "AQAAAAEAACcQAAAAEF+xOVbqpdbufN9oaNlQhvmVG8iBWEhGs1/uolxHPh99QhEnSB/7AgaX13E17//0cw==", "10dd20af-7875-4677-a761-59f6b93bc97d" });
        }
    }
}
