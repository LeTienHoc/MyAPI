using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyAPI.Migrations
{
    public partial class lc_km : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SoLuong",
                table: "ve");

            migrationBuilder.DropColumn(
                name: "DienVien",
                table: "kich");

            migrationBuilder.DropColumn(
                name: "GiaGhe",
                table: "ghe");

            migrationBuilder.DropColumn(
                name: "TenGhe",
                table: "ghe");

            migrationBuilder.DropColumn(
                name: "TinhTrangGhe",
                table: "ghe");

            migrationBuilder.RenameColumn(
                name: "MaKich",
                table: "khuyenmai",
                newName: "MaNhaKich");

            migrationBuilder.AddColumn<ulong>(
                name: "TinhTrang",
                table: "ve",
                type: "bit(1)",
                nullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "NgayKT",
                table: "lichchieu",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "NgayBD",
                table: "lichchieu",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MaNhaKich",
                table: "lichchieu",
                type: "longtext",
                nullable: false,
                collation: "utf8mb4_0900_ai_ci")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "lichchieu_khuyenmai",
                columns: table => new
                {
                    MaLichChieu = table.Column<string>(type: "longtext", nullable: false, collation: "utf8mb4_0900_ai_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    MaKM = table.Column<string>(type: "longtext", nullable: false, collation: "utf8mb4_0900_ai_ci")
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_0900_ai_ci");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "lichchieu_khuyenmai");

            migrationBuilder.DropColumn(
                name: "TinhTrang",
                table: "ve");

            migrationBuilder.DropColumn(
                name: "MaNhaKich",
                table: "lichchieu");

            migrationBuilder.RenameColumn(
                name: "MaNhaKich",
                table: "khuyenmai",
                newName: "MaKich");

            migrationBuilder.AddColumn<int>(
                name: "SoLuong",
                table: "ve",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "NgayKT",
                table: "lichchieu",
                type: "datetime(6)",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)");

            migrationBuilder.AlterColumn<DateTime>(
                name: "NgayBD",
                table: "lichchieu",
                type: "datetime(6)",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)");

            migrationBuilder.AddColumn<string>(
                name: "DienVien",
                table: "kich",
                type: "varchar(255)",
                maxLength: 255,
                nullable: true,
                collation: "utf8mb4_0900_ai_ci")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<float>(
                name: "GiaGhe",
                table: "ghe",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TenGhe",
                table: "ghe",
                type: "varchar(255)",
                maxLength: 255,
                nullable: true,
                collation: "utf8mb4_0900_ai_ci")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<ulong>(
                name: "TinhTrangGhe",
                table: "ghe",
                type: "bit(1)",
                nullable: true);
        }
    }
}
