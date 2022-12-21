using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyAPI.Migrations
{
    public partial class newxc : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "KetThuc",
                table: "xuatchieu");

            migrationBuilder.AddColumn<int>(
                name: "Thoiluong",
                table: "xuatchieu",
                type: "int",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Thoiluong",
                table: "xuatchieu");

            migrationBuilder.AddColumn<DateTime>(
                name: "KetThuc",
                table: "xuatchieu",
                type: "datetime(6)",
                nullable: true);
        }
    }
}
