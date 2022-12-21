using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyAPI.Migrations
{
    public partial class newxuatchieumoi : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<TimeSpan>(
                name: "Thoiluong",
                table: "xuatchieu",
                type: "time(6)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Thoiluong",
                table: "xuatchieu",
                type: "int",
                nullable: true,
                oldClrType: typeof(TimeSpan),
                oldType: "time(6)",
                oldNullable: true);
        }
    }
}
