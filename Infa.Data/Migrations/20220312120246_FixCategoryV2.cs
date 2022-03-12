using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infa.Data.Migrations
{
    public partial class FixCategoryV2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: "19b499d6-8920-43c0-a6d7-bbe48c42f7b3",
                column: "ConcurrencyStamp",
                value: "a922df75-5f63-4030-946d-442a7b671755");

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: "ac0e1032-58c2-449a-8b47-7f6838386bc0",
                column: "ConcurrencyStamp",
                value: "124fabeb-71ef-4de9-bddb-ae78dec64f90");

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: "ea345380-5af2-4d0c-a0e2-65f3eeabb898",
                column: "ConcurrencyStamp",
                value: "39e1e415-a512-4811-ab6a-aeb49863887e");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: "19b499d6-8920-43c0-a6d7-bbe48c42f7b3",
                column: "ConcurrencyStamp",
                value: "49d0d8f3-cc96-48a9-bc52-8ceb2ce7e198");

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: "ac0e1032-58c2-449a-8b47-7f6838386bc0",
                column: "ConcurrencyStamp",
                value: "de3b05a8-2ee8-4ffb-8cc2-a5d3be5ee3a3");

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: "ea345380-5af2-4d0c-a0e2-65f3eeabb898",
                column: "ConcurrencyStamp",
                value: "b6778655-315c-4bc4-be6b-50d9f022f9ec");
        }
    }
}
