using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyAPI.Migrations
{
    public partial class emailnk : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                table: "taikhoan");

            migrationBuilder.DropColumn(
                name: "SDT",
                table: "taikhoan");

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "nhakich",
                type: "longtext",
                nullable: false,
                collation: "utf8mb4_0900_ai_ci")
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                table: "nhakich");

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "taikhoan",
                type: "varchar(225)",
                maxLength: 225,
                nullable: false,
                defaultValue: "",
                collation: "utf8mb4_0900_ai_ci")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "SDT",
                table: "taikhoan",
                type: "varchar(10)",
                maxLength: 10,
                nullable: false,
                defaultValue: "",
                collation: "utf8mb4_0900_ai_ci")
                .Annotation("MySql:CharSet", "utf8mb4");
        }
    }
}
