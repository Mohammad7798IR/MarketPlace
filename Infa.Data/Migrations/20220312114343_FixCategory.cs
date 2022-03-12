using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infa.Data.Migrations
{
    public partial class FixCategory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ParentId",
                table: "Category",
                type: "nvarchar(450)",
                nullable: true);

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

            migrationBuilder.CreateIndex(
                name: "IX_Category_ParentId",
                table: "Category",
                column: "ParentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Category_Category_ParentId",
                table: "Category",
                column: "ParentId",
                principalTable: "Category",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Category_Category_ParentId",
                table: "Category");

            migrationBuilder.DropIndex(
                name: "IX_Category_ParentId",
                table: "Category");

            migrationBuilder.DropColumn(
                name: "ParentId",
                table: "Category");

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: "19b499d6-8920-43c0-a6d7-bbe48c42f7b3",
                column: "ConcurrencyStamp",
                value: "e391b312-3c54-4fec-8d0b-696677daa32e");

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: "ac0e1032-58c2-449a-8b47-7f6838386bc0",
                column: "ConcurrencyStamp",
                value: "34445278-6f8c-45f2-9507-18c00961a199");

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: "ea345380-5af2-4d0c-a0e2-65f3eeabb898",
                column: "ConcurrencyStamp",
                value: "3cf75e8b-2ca5-444b-8c42-28fe5dd7cf0b");
        }
    }
}
