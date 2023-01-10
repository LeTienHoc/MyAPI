using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace MyAPI.Data
{
    public partial class MyDbContext : DbContext
    {
        public MyDbContext()
        {
        }

        public MyDbContext(DbContextOptions<MyDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Banner> Banners { get; set; } = null!;
        public virtual DbSet<Daodien> Daodiens { get; set; } = null!;
        public virtual DbSet<Dienvien> Dienviens { get; set; } = null!;
        public virtual DbSet<Ghe> Ghes { get; set; } = null!;
        public virtual DbSet<Khachhang> Khachhangs { get; set; } = null!;
        public virtual DbSet<Kich> Kiches { get; set; } = null!;
        public virtual DbSet<KichDienvien> KichDienviens { get; set; } = null!;
        public virtual DbSet<KichDaodien> KichDaodiens { get; set; } = null!;
        public virtual DbSet<Lichchieu> Lichchieus { get; set; } = null!;
        public virtual DbSet<Nhakich> Nhakiches { get; set; } = null!;
        public virtual DbSet<Taikhoan> Taikhoans { get; set; } = null!;
        public virtual DbSet<Ve> Ves { get; set; } = null!;
        public virtual DbSet<Xuatchieu> Xuatchieus { get; set; } = null!;
        public virtual DbSet<RefreshToken> RefreshTokens { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseMySql("server=localhost;user id=root;password=123456;database=vekich", Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.29-mysql"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseCollation("utf8mb4_0900_ai_ci")
                .HasCharSet("utf8mb4");

            modelBuilder.Entity<Banner>(entity =>
            {
                entity.HasKey(e => e.MaBanner)
                    .HasName("PRIMARY");

                entity.ToTable("banner");

                entity.Property(e => e.Image).HasMaxLength(255);

                entity.Property(e => e.TenBanner).HasMaxLength(255);
            });

           

            modelBuilder.Entity<Daodien>(entity =>
            {
                entity.HasKey(e => e.MaDaoDien)
                    .HasName("PRIMARY");

                entity.ToTable("daodien");

                entity.Property(e => e.TenDaoDien).HasMaxLength(255);
            });

            modelBuilder.Entity<Dienvien>(entity =>
            {
                entity.HasKey(e => e.MaDienVien)
                    .HasName("PRIMARY");

                entity.ToTable("dienvien");

                entity.Property(e => e.TenDienVien).HasMaxLength(255);
            });

            modelBuilder.Entity<Ghe>(entity =>
            {
                entity.HasKey(e => e.MaGhe)
                    .HasName("PRIMARY");

                entity.ToTable("ghe");

                entity.Property(e => e.Hang).HasMaxLength(1);

            });

            modelBuilder.Entity<Khachhang>(entity =>
            {
                entity.HasKey(e => e.MaKh)
                    .HasName("PRIMARY");

                entity.ToTable("khachhang");

                entity.Property(e => e.Avarta).HasMaxLength(255);

                entity.Property(e => e.DiaChi).HasMaxLength(255);

                entity.Property(e => e.Email).HasMaxLength(255);

                entity.Property(e => e.MatKhau).HasMaxLength(255);

                entity.Property(e => e.Sdt)
                    .HasMaxLength(10)
                    .HasColumnName("SDT");

                entity.Property(e => e.TenKh)
                    .HasMaxLength(255)
                    .HasColumnName("TenKH");

                entity.Property(e => e.TenTaiKhoan).HasMaxLength(255);
            });

            

            modelBuilder.Entity<Kich>(entity =>
            {
                entity.HasKey(e => e.MaKich)
                    .HasName("PRIMARY");

                entity.ToTable("kich");


                entity.Property(e => e.Image).HasMaxLength(255);

                entity.Property(e => e.MoTa).HasMaxLength(255);

                entity.Property(e => e.NgayBd).HasColumnName("NgayBD");

                entity.Property(e => e.NgayKt).HasColumnName("NgayKT");

                entity.Property(e => e.TenKich).HasMaxLength(255);

                entity.Property(e => e.TheLoai).HasMaxLength(255);

                entity.Property(e => e.TrangThai).HasColumnType("bit(1)");
            });

            modelBuilder.Entity<KichDienvien>(entity =>
            {
                entity.HasKey(e=>e.MaKich).HasName("PRIMARY");
                entity.HasKey(e => e.MaDienVien).HasName("PRIMARY");

                entity.ToTable("kich_dienvien");
            });
            modelBuilder.Entity<KichDaodien>(entity =>
            {
                entity.HasKey(e => e.MaKich).HasName("PRIMARY");
                entity.HasKey(e => e.MaDaodien).HasName("PRIMARY");

                entity.ToTable("kich_daodien");
            });

            modelBuilder.Entity<Lichchieu>(entity =>
            {
                entity.HasKey(e => e.MaLichChieu)
                    .HasName("PRIMARY");

                entity.ToTable("lichchieu");

                entity.Property(e => e.NgayBd).HasColumnName("NgayBD");

                entity.Property(e => e.NgayKt).HasColumnName("NgayKT");
            });

            modelBuilder.Entity<Nhakich>(entity =>
            {
                entity.HasKey(e => e.MaNhaKich)
                    .HasName("PRIMARY");

                entity.ToTable("nhakich");

                entity.Property(e => e.DiaChi).HasMaxLength(255);

                entity.Property(e => e.SoDienThoai).HasMaxLength(10);

                entity.Property(e => e.TenNhaKich).HasMaxLength(255);
            });

            modelBuilder.Entity<Taikhoan>(entity =>
            {
                entity.HasKey(e => e.MaTk)
                    .HasName("PRIMARY");

                entity.ToTable("taikhoan");

                entity.Property(e => e.Email).HasMaxLength(225);

                entity.Property(e => e.LoaiTaiKhoan).HasMaxLength(225);

                entity.Property(e => e.MatKhau).HasMaxLength(255);

                entity.Property(e => e.Sdt)
                    .HasMaxLength(10)
                    .HasColumnName("SDT");

                entity.Property(e => e.TenTaiKhoan).HasMaxLength(255);
            });

            modelBuilder.Entity<Ve>(entity =>
            {
                entity.HasKey(e => e.MaVe)
                    .HasName("PRIMARY");

                entity.ToTable("ve");

                entity.Property(e => e.MaXc).HasColumnName("MaXC");
            });
            modelBuilder.Entity<RefreshToken>(entity =>
            {
                entity.HasKey(e => e.TokenID)
                    .HasName("PRIMARY");

                entity.ToTable("refreshtoken");

                entity.Property(e => e.MaTk).HasColumnName("MaTK");
            });

            modelBuilder.Entity<Xuatchieu>(entity =>
            {
                entity.HasKey(e => e.MaXc)
                    .HasName("PRIMARY");

                entity.ToTable("xuatchieu");

                entity.Property(e => e.MaXc).HasColumnName("MaXC");

            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
