using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyAPI.Migrations
{
    public partial class xcmoinew : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Thoiluong",
                table: "xuatchieu",
                newName: "Thoigian");

            migrationBuilder.RenameColumn(
                name: "NgayGio",
                table: "xuatchieu",
                newName: "NgayChieu");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Thoigian",
                table: "xuatchieu",
                newName: "Thoiluong");

            migrationBuilder.RenameColumn(
                name: "NgayChieu",
                table: "xuatchieu",
                newName: "NgayGio");
        }
    }
}
