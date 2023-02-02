using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyAPI.Migrations
{
    public partial class mankghe : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "NhaKich",
                table: "ghe",
                newName: "MaNhaKich");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "MaNhaKich",
                table: "ghe",
                newName: "NhaKich");
        }
    }
}
