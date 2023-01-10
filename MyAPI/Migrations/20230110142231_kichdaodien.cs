using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyAPI.Migrations
{
    public partial class kichdaodien : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DaoDien",
                table: "kich");

            migrationBuilder.AlterColumn<string>(
                name: "Thoiluong",
                table: "xuatchieu",
                type: "longtext",
                nullable: true,
                collation: "utf8mb4_0900_ai_ci",
                oldClrType: typeof(TimeSpan),
                oldType: "time(6)",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "kich_daodien",
                columns: table => new
                {
                    MaDaodien = table.Column<string>(type: "varchar(255)", nullable: false, collation: "utf8mb4_0900_ai_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    MaKich = table.Column<string>(type: "longtext", nullable: true, collation: "utf8mb4_0900_ai_ci")
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.MaDaodien);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_0900_ai_ci");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "kich_daodien");

            migrationBuilder.AlterColumn<TimeSpan>(
                name: "Thoiluong",
                table: "xuatchieu",
                type: "time(6)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .OldAnnotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("Relational:Collation", "utf8mb4_0900_ai_ci");

            migrationBuilder.AddColumn<string>(
                name: "DaoDien",
                table: "kich",
                type: "varchar(255)",
                maxLength: 255,
                nullable: true,
                collation: "utf8mb4_0900_ai_ci")
                .Annotation("MySql:CharSet", "utf8mb4");
        }
    }
}
