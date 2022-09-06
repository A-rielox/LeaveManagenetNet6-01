using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LeaveManagenet.Web.Data.Migrations
{
    public partial class @as : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "RequestComments",
                table: "LeaveRequests",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "RequestComments",
                table: "LeaveRequests",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "cac43a6e-f7bb-4448-baaf-1add431ccbbf",
                column: "ConcurrencyStamp",
                value: "50bdb610-136e-49ec-ba5f-0713d0d8306d");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "cac43a7e-f7cb-4148-baaf-1acb431eabbf",
                column: "ConcurrencyStamp",
                value: "32a7cdb3-ca11-4046-8119-5f3ab0533fd0");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "3f4631bd-f907-4409-b416-ba356312e659",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "0c2cd308-6099-4700-ae86-7cc3a1f8e32a", "AQAAAAEAACcQAAAAEBIGGJzPA1CtsR0x62UTjN69gs53cvavPebWrbzD2Hc3eOpqlSohhJp2GMUdAjL1tA==", "2298cc06-e969-4089-b2d5-04c144aea11b" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "408aa945-3d84-4421-8342-7269ec64d949",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "2c9c4c5b-e65d-4543-8456-cb4b153f978e", "AQAAAAEAACcQAAAAEBXkINTi9yfAwTE++BhbmKsEuodz0Lfty90qAsthgxGZ0Mhn8mNWg1PMlJ564eo0zA==", "796062b1-2f29-4dcf-9b95-112ee83442e4" });
        }
    }
}
