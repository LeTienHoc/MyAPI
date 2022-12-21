﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MyAPI.Data;

#nullable disable

namespace MyAPI.Migrations
{
    [DbContext(typeof(MyDbContext))]
    [Migration("20221219153217_xuatchieu")]
    partial class xuatchieu
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseCollation("utf8mb4_0900_ai_ci")
                .HasAnnotation("ProductVersion", "6.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            MySqlModelBuilderExtensions.HasCharSet(modelBuilder, "utf8mb4");

            modelBuilder.Entity("MyAPI.Data.Banner", b =>
                {
                    b.Property<string>("MaBanner")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Image")
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)");

                    b.Property<string>("TenBanner")
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)");

                    b.HasKey("MaBanner")
                        .HasName("PRIMARY");

                    b.ToTable("banner", (string)null);
                });

            modelBuilder.Entity("MyAPI.Data.Daodien", b =>
                {
                    b.Property<string>("MaDaoDien")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("MaNhaKich")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("TenDaoDien")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)");

                    b.HasKey("MaDaoDien")
                        .HasName("PRIMARY");

                    b.ToTable("daodien", (string)null);
                });

            modelBuilder.Entity("MyAPI.Data.Dienvien", b =>
                {
                    b.Property<string>("MaDienVien")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("MaNhaKich")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("TenDienVien")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)");

                    b.HasKey("MaDienVien")
                        .HasName("PRIMARY");

                    b.ToTable("dienvien", (string)null);
                });

            modelBuilder.Entity("MyAPI.Data.Ghe", b =>
                {
                    b.Property<string>("MaGhe")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Hang")
                        .IsRequired()
                        .HasMaxLength(1)
                        .HasColumnType("varchar(1)");

                    b.Property<string>("NhaKich")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int?>("Seat")
                        .IsRequired()
                        .HasColumnType("int");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.HasKey("MaGhe")
                        .HasName("PRIMARY");

                    b.ToTable("ghe", (string)null);
                });

            modelBuilder.Entity("MyAPI.Data.Khachhang", b =>
                {
                    b.Property<string>("MaKh")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Avarta")
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)");

                    b.Property<string>("ConfirmMatKhau")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("DiaChi")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)");

                    b.Property<string>("MatKhau")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)");

                    b.Property<DateTime?>("NgaySinh")
                        .IsRequired()
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Sdt")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("varchar(10)")
                        .HasColumnName("SDT");

                    b.Property<string>("TenKh")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)")
                        .HasColumnName("TenKH");

                    b.Property<string>("TenTaiKhoan")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)");

                    b.HasKey("MaKh")
                        .HasName("PRIMARY");

                    b.ToTable("khachhang", (string)null);
                });

            modelBuilder.Entity("MyAPI.Data.Kich", b =>
                {
                    b.Property<string>("MaKich")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("DaoDien")
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Image")
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)");

                    b.Property<string>("MaNhaKich")
                        .HasColumnType("longtext");

                    b.Property<string>("MoTa")
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)");

                    b.Property<DateTime?>("NgayBd")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("NgayBD");

                    b.Property<DateTime?>("NgayKt")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("NgayKT");

                    b.Property<string>("TenKich")
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)");

                    b.Property<string>("TheLoai")
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)");

                    b.Property<ulong?>("TrangThai")
                        .HasColumnType("bit(1)");

                    b.HasKey("MaKich")
                        .HasName("PRIMARY");

                    b.ToTable("kich", (string)null);
                });

            modelBuilder.Entity("MyAPI.Data.KichDienvien", b =>
                {
                    b.Property<string>("MaDienVien")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("MaKich")
                        .HasColumnType("longtext");

                    b.HasKey("MaDienVien")
                        .HasName("PRIMARY");

                    b.ToTable("kich_dienvien", (string)null);
                });

            modelBuilder.Entity("MyAPI.Data.Lichchieu", b =>
                {
                    b.Property<string>("MaLichChieu")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("MaNhaKich")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateTime?>("NgayBd")
                        .IsRequired()
                        .HasColumnType("datetime(6)")
                        .HasColumnName("NgayBD");

                    b.Property<DateTime?>("NgayKt")
                        .IsRequired()
                        .HasColumnType("datetime(6)")
                        .HasColumnName("NgayKT");

                    b.HasKey("MaLichChieu")
                        .HasName("PRIMARY");

                    b.ToTable("lichchieu", (string)null);
                });

            modelBuilder.Entity("MyAPI.Data.Nhakich", b =>
                {
                    b.Property<string>("MaNhaKich")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("DiaChi")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)");

                    b.Property<string>("SoDienThoai")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("varchar(10)");

                    b.Property<string>("TenNhaKich")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)");

                    b.HasKey("MaNhaKich")
                        .HasName("PRIMARY");

                    b.ToTable("nhakich", (string)null);
                });

            modelBuilder.Entity("MyAPI.Data.RefreshToken", b =>
                {
                    b.Property<Guid>("TokenID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("ExpiredAt")
                        .HasColumnType("datetime(6)");

                    b.Property<bool>("IsRevoked")
                        .HasColumnType("tinyint(1)");

                    b.Property<bool>("IsUsed")
                        .HasColumnType("tinyint(1)");

                    b.Property<DateTime>("IssuedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("JwtID")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("MaTk")
                        .IsRequired()
                        .HasColumnType("longtext")
                        .HasColumnName("MaTK");

                    b.Property<string>("Token")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("TokenID")
                        .HasName("PRIMARY");

                    b.ToTable("refreshtoken", (string)null);
                });

            modelBuilder.Entity("MyAPI.Data.Taikhoan", b =>
                {
                    b.Property<string>("MaTk")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("ConfirmMatkhau")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(225)
                        .HasColumnType("varchar(225)");

                    b.Property<string>("LoaiTaiKhoan")
                        .IsRequired()
                        .HasMaxLength(225)
                        .HasColumnType("varchar(225)");

                    b.Property<string>("MatKhau")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Sdt")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("varchar(10)")
                        .HasColumnName("SDT");

                    b.Property<string>("TenTaiKhoan")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)");

                    b.HasKey("MaTk")
                        .HasName("PRIMARY");

                    b.ToTable("taikhoan", (string)null);
                });

            modelBuilder.Entity("MyAPI.Data.Ve", b =>
                {
                    b.Property<string>("MaVe")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("MaGhe")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("MaKh")
                        .HasColumnType("longtext");

                    b.Property<string>("MaTk")
                        .HasColumnType("longtext");

                    b.Property<string>("MaXc")
                        .IsRequired()
                        .HasColumnType("longtext")
                        .HasColumnName("MaXC");

                    b.Property<DateTime?>("NgayDatVe")
                        .IsRequired()
                        .HasColumnType("datetime(6)");

                    b.Property<int>("TinhTrang")
                        .HasColumnType("int");

                    b.Property<float?>("TongGia")
                        .IsRequired()
                        .HasColumnType("float");

                    b.HasKey("MaVe")
                        .HasName("PRIMARY");

                    b.ToTable("ve", (string)null);
                });

            modelBuilder.Entity("MyAPI.Data.Xuatchieu", b =>
                {
                    b.Property<string>("MaXc")
                        .HasColumnType("varchar(255)")
                        .HasColumnName("MaXC");

                    b.Property<DateTime?>("KetThuc")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("MaKich")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("MaLichChieu")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateTime?>("NgayGio")
                        .IsRequired()
                        .HasColumnType("datetime(6)");

                    b.HasKey("MaXc")
                        .HasName("PRIMARY");

                    b.ToTable("xuatchieu", (string)null);
                });
#pragma warning restore 612, 618
        }
    }
}
