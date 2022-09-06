using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LeaveManagenet.Web.Data.Migrations
{
    public partial class asdlkfjaslkdjf : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "cac43a6e-f7bb-4448-baaf-1add431ccbbf",
                column: "ConcurrencyStamp",
                value: "c853f580-868f-41c8-aca2-97fc47ff03cd");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "cac43a7e-f7cb-4148-baaf-1acb431eabbf",
                column: "ConcurrencyStamp",
                value: "2e10dc4c-bf9f-48c9-a836-161ace5ff36e");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "3f4631bd-f907-4409-b416-ba356312e659",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "b0ff8faf-c1f8-4428-8bb0-d72c28bbd259", "AQAAAAEAACcQAAAAEIvx0XQc3HcBwbvrFRbDMgAeOkjplEExWJ+IIsdeHV4qvpHsOe0nXBiQz2oLbX/IsA==", "efd6c706-eed5-4de2-bbf5-6590b7139b2a" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "408aa945-3d84-4421-8342-7269ec64d949",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "6ce3b105-63e6-453f-82d8-b5347df33688", "AQAAAAEAACcQAAAAEI2ColiwYTFpwkwIVS4umYXdJygRp7dn0BvckopbxhFqih5yIfdZNjULXq27Rt9bqg==", "fafc7967-0785-4c5d-8e32-bc51e7042102" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "cac43a6e-f7bb-4448-baaf-1add431ccbbf",
                column: "ConcurrencyStamp",
                value: "141a90cb-fdcc-42e1-8fe6-ac0b53cb8562");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "cac43a7e-f7cb-4148-baaf-1acb431eabbf",
                column: "ConcurrencyStamp",
                value: "83d61891-439a-4ffe-aa31-d29abbc5cca1");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "3f4631bd-f907-4409-b416-ba356312e659",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "eaf49299-fb42-4014-afca-efe01a30ae30", "AQAAAAEAACcQAAAAEClcaSEEPjG0lK0F0MegvdH2NroInIPf+y/ggHEMhmqRxxx4LQsEUbGk+RE1yoUS9w==", "9be49ed2-78a5-4a4d-a518-dd8d03dd3a13" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "408aa945-3d84-4421-8342-7269ec64d949",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "ded0128e-5513-4800-bc1f-38e79d1c89ec", "AQAAAAEAACcQAAAAENazELQqCRKmkhKVCq0v8BfDotVnErGdeuy3JPB0ubIhloMNtv1THlS9C06r/5wT0w==", "8f9d7552-c99d-4e79-b822-173291824892" });
        }
    }
}
