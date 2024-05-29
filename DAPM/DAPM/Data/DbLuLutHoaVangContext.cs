using System;
using System.Collections.Generic;
using DAPM.Models;
using Microsoft.EntityFrameworkCore;

namespace DAPM.Data;

public partial class DbLuLutHoaVangContext : DbContext
{
    public DbLuLutHoaVangContext()
    {
    }

    public DbLuLutHoaVangContext(DbContextOptions<DbLuLutHoaVangContext> options)
        : base(options)
    {
    }

    public virtual DbSet<TbBaiDang> TbBaiDangs { get; set; }

    public virtual DbSet<TbChiTietHangCuuTro> TbChiTietHangCuuTros { get; set; }

    public virtual DbSet<TbChiTietHangUngHo> TbChiTietHangUngHos { get; set; }

    public virtual DbSet<TbChitietMucDoThietHai> TbChitietMucDoThietHais { get; set; }

    public virtual DbSet<TbDanhMuc> TbDanhMucs { get; set; }

    public virtual DbSet<TbDonDangKyUngHo> TbDonDangKyUngHos { get; set; }

    public virtual DbSet<TbDotCuuTro> TbDotCuuTros { get; set; }

    public virtual DbSet<TbDotLu> TbDotLus { get; set; }

    public virtual DbSet<TbFile> TbFiles { get; set; }

    public virtual DbSet<TbHangHoa> TbHangHoas { get; set; }

    public virtual DbSet<TbHinhAnh> TbHinhAnhs { get; set; }

    public virtual DbSet<TbMucDoThietHai> TbMucDoThietHais { get; set; }

    public virtual DbSet<TbTaiKhoan> TbTaiKhoans { get; set; }

    public virtual DbSet<TbThongBao> TbThongBaos { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TbBaiDang>(entity =>
        {
            entity.HasKey(e => e.IdBaiDang).HasName("PK__tbBaiDan__145A6AE1E9C00E8F");

            entity.ToTable("tbBaiDang");

            entity.Property(e => e.IdBaiDang).HasColumnName("idBaiDang");
            entity.Property(e => e.IdDotLu).HasColumnName("idDotLu");
            entity.Property(e => e.IdTaiKhoan).HasColumnName("idTaiKhoan");
            entity.Property(e => e.NgayDang)
                .HasColumnType("datetime")
                .HasColumnName("ngayDang");
            entity.Property(e => e.NoiDung).HasColumnName("noiDung");
            entity.Property(e => e.TieuDe)
                .HasMaxLength(100)
                .HasColumnName("tieuDe");

            entity.HasOne(d => d.IdDotLuNavigation).WithMany(p => p.TbBaiDangs)
                .HasForeignKey(d => d.IdDotLu)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__tbBaiDang__idDot__4316F928");

            entity.HasOne(d => d.IdTaiKhoanNavigation).WithMany(p => p.TbBaiDangs)
                .HasForeignKey(d => d.IdTaiKhoan)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__tbBaiDang__idTai__440B1D61");
        });

        modelBuilder.Entity<TbChiTietHangCuuTro>(entity =>
        {
            entity.HasKey(e => new { e.IdHangHoa, e.IdTaiKhoan, e.IdDotCuuTro }).HasName("PK__tbChiTie__FAA0A39FCA225C80");

            entity.ToTable("tbChiTietHangCuuTro", tb => tb.HasTrigger("update_product_quantity"));

            entity.Property(e => e.IdHangHoa).HasColumnName("idHangHoa");
            entity.Property(e => e.IdTaiKhoan).HasColumnName("idTaiKhoan");
            entity.Property(e => e.IdDotCuuTro).HasColumnName("idDotCuuTro");
            entity.Property(e => e.SoLuong).HasColumnName("soLuong");

            entity.HasOne(d => d.IdDotCuuTroNavigation).WithMany(p => p.TbChiTietHangCuuTros)
                .HasForeignKey(d => d.IdDotCuuTro)
                .HasConstraintName("FK__tbChiTiet__idDot__5AEE82B9");

            entity.HasOne(d => d.IdHangHoaNavigation).WithMany(p => p.TbChiTietHangCuuTros)
                .HasForeignKey(d => d.IdHangHoa)
                .HasConstraintName("FK__tbChiTiet__idHan__59063A47");

            entity.HasOne(d => d.IdTaiKhoanNavigation).WithMany(p => p.TbChiTietHangCuuTros)
                .HasForeignKey(d => d.IdTaiKhoan)
                .HasConstraintName("FK__tbChiTiet__idTai__59FA5E80");
        });

        modelBuilder.Entity<TbChiTietHangUngHo>(entity =>
        {
            entity.HasKey(e => new { e.IdHangHoa, e.IdDonDk }).HasName("PK__tbChiTie__8FB24F36522C89B8");

            entity.ToTable("tbChiTietHangUngHo", tb =>
                {
                    tb.HasTrigger("CapNhatSoLuongHangUngHo");
                    tb.HasTrigger("trg_UpdateTrangThaiDonDangKy");
                });

            entity.Property(e => e.IdHangHoa).HasColumnName("idHangHoa");
            entity.Property(e => e.IdDonDk).HasColumnName("idDonDK");
            entity.Property(e => e.SoLuong).HasColumnName("soLuong");

            entity.HasOne(d => d.IdDonDkNavigation).WithMany(p => p.TbChiTietHangUngHos)
                .HasForeignKey(d => d.IdDonDk)
                .HasConstraintName("FK__tbChiTiet__idDon__5629CD9C");

            entity.HasOne(d => d.IdHangHoaNavigation).WithMany(p => p.TbChiTietHangUngHos)
                .HasForeignKey(d => d.IdHangHoa)
                .HasConstraintName("FK__tbChiTiet__idHan__5535A963");
        });

        modelBuilder.Entity<TbChitietMucDoThietHai>(entity =>
        {
            entity.HasKey(e => new { e.IdMucDo, e.IdTaiKhoan, e.IdDotLu }).HasName("PK__tbChitie__AAEBEF46A5A8CCE6");

            entity.ToTable("tbChitietMucDoThietHai");

            entity.Property(e => e.IdMucDo).HasColumnName("idMucDo");
            entity.Property(e => e.IdTaiKhoan).HasColumnName("idTaiKhoan");
            entity.Property(e => e.IdDotLu).HasColumnName("idDotLu");
            entity.Property(e => e.Mota).HasColumnName("mota");

            entity.HasOne(d => d.IdDotLuNavigation).WithMany(p => p.TbChitietMucDoThietHais)
                .HasForeignKey(d => d.IdDotLu)
                .HasConstraintName("FK__tbChitiet__idDot__619B8048");

            entity.HasOne(d => d.IdMucDoNavigation).WithMany(p => p.TbChitietMucDoThietHais)
                .HasForeignKey(d => d.IdMucDo)
                .HasConstraintName("FK__tbChitiet__idMuc__5FB337D6");

            entity.HasOne(d => d.IdTaiKhoanNavigation).WithMany(p => p.TbChitietMucDoThietHais)
                .HasForeignKey(d => d.IdTaiKhoan)
                .HasConstraintName("FK__tbChitiet__idTai__60A75C0F");
        });

        modelBuilder.Entity<TbDanhMuc>(entity =>
        {
            entity.HasKey(e => e.IdDanhMuc).HasName("PK__tbDanhMu__927F1878021EC87D");

            entity.ToTable("tbDanhMuc");

            entity.Property(e => e.IdDanhMuc).HasColumnName("idDanhMuc");
            entity.Property(e => e.MoTa)
                .HasColumnType("ntext")
                .HasColumnName("moTa");
            entity.Property(e => e.TenDanhMuc)
                .HasMaxLength(100)
                .HasColumnName("tenDanhMuc");
        });

        modelBuilder.Entity<TbDonDangKyUngHo>(entity =>
        {
            entity.HasKey(e => e.IdDonDk).HasName("PK__tbDonDan__45F500CBD044AE1F");

            entity.ToTable("tbDonDangKyUngHo");

            entity.Property(e => e.IdDonDk).HasColumnName("idDonDK");
            entity.Property(e => e.IdDotLu).HasColumnName("idDotLu");
            entity.Property(e => e.IdTaiKhoan).HasColumnName("idTaiKhoan");
            entity.Property(e => e.NgayDk)
                .HasColumnType("datetime")
                .HasColumnName("ngayDK");
            entity.Property(e => e.TenHangUngHo).HasColumnName("tenHangUngHo");
            entity.Property(e => e.TrangThai)
                .HasMaxLength(100)
                .HasColumnName("trangThai");

            entity.HasOne(d => d.IdDotLuNavigation).WithMany(p => p.TbDonDangKyUngHos)
                .HasForeignKey(d => d.IdDotLu)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__tbDonDang__idDot__4BAC3F29");

            entity.HasOne(d => d.IdTaiKhoanNavigation).WithMany(p => p.TbDonDangKyUngHos)
                .HasForeignKey(d => d.IdTaiKhoan)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__tbDonDang__idTai__4AB81AF0");
        });

        modelBuilder.Entity<TbDotCuuTro>(entity =>
        {
            entity.HasKey(e => e.IdDotCuuTro).HasName("PK__tbDotCuu__B795416852F5C197");

            entity.ToTable("tbDotCuuTro");

            entity.Property(e => e.IdDotCuuTro).HasColumnName("idDotCuuTro");
            entity.Property(e => e.IdDotLu).HasColumnName("idDotLu");
            entity.Property(e => e.NgayBatDau)
                .HasColumnType("datetime")
                .HasColumnName("ngayBatDau");
            entity.Property(e => e.NgayKetThuc)
                .HasColumnType("datetime")
                .HasColumnName("ngayKetThuc");
            entity.Property(e => e.TenDotCuuTro)
                .HasMaxLength(50)
                .HasColumnName("tenDotCuuTro");

            entity.HasOne(d => d.IdDotLuNavigation).WithMany(p => p.TbDotCuuTros)
                .HasForeignKey(d => d.IdDotLu)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__tbDotCuuT__idDot__5165187F");
        });

        modelBuilder.Entity<TbDotLu>(entity =>
        {
            entity.HasKey(e => e.IdDotLu).HasName("PK__tbDotLu__D7AF9906A52C8567");

            entity.ToTable("tbDotLu");

            entity.HasIndex(e => e.TenDotLu, "UQ__tbDotLu__8BBC9DB6D3CF8442").IsUnique();

            entity.Property(e => e.IdDotLu).HasColumnName("idDotLu");
            entity.Property(e => e.NgayBatDau)
                .HasColumnType("datetime")
                .HasColumnName("ngayBatDau");
            entity.Property(e => e.NgayKetThuc)
                .HasColumnType("datetime")
                .HasColumnName("ngayKetThuc");
            entity.Property(e => e.TenDotLu)
                .HasMaxLength(100)
                .HasColumnName("tenDotLu");
        });

        modelBuilder.Entity<TbFile>(entity =>
        {
            entity.HasKey(e => e.IdFile).HasName("PK__tbFile__775AFE722C88670C");

            entity.ToTable("tbFile");

            entity.Property(e => e.IdFile).HasColumnName("idFile");
            entity.Property(e => e.IdThongBao).HasColumnName("idThongBao");
            entity.Property(e => e.UrlFile)
                .IsUnicode(false)
                .HasColumnName("urlFile");

            entity.HasOne(d => d.IdThongBaoNavigation).WithMany(p => p.TbFiles)
                .HasForeignKey(d => d.IdThongBao)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__tbFile__idThongB__6754599E");
        });

        modelBuilder.Entity<TbHangHoa>(entity =>
        {
            entity.HasKey(e => e.IdHangHoa).HasName("PK__tbHangHo__2BED1F3AF585627C");

            entity.ToTable("tbHangHoa");

            entity.Property(e => e.IdHangHoa).HasColumnName("idHangHoa");
            entity.Property(e => e.DonViTinh)
                .HasMaxLength(50)
                .HasColumnName("donViTinh");
            entity.Property(e => e.IdDanhMuc).HasColumnName("idDanhMuc");
            entity.Property(e => e.MoTa).HasColumnName("moTa");
            entity.Property(e => e.SoLuong).HasColumnName("soLuong");
            entity.Property(e => e.TenHangHoa)
                .HasMaxLength(100)
                .HasColumnName("tenHangHoa");

            entity.HasOne(d => d.IdDanhMucNavigation).WithMany(p => p.TbHangHoas)
                .HasForeignKey(d => d.IdDanhMuc)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__tbHangHoa__idDan__4E88ABD4");
        });

        modelBuilder.Entity<TbHinhAnh>(entity =>
        {
            entity.HasKey(e => e.IdHinhAnh).HasName("PK__tbHinhAn__4187C930D645FDCD");

            entity.ToTable("tbHinhAnh");

            entity.Property(e => e.IdHinhAnh).HasColumnName("idHinhAnh");
            entity.Property(e => e.IdBaiDang).HasColumnName("idBaiDang");
            entity.Property(e => e.UrlHinhAnh)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("urlHinhAnh");

            entity.HasOne(d => d.IdBaiDangNavigation).WithMany(p => p.TbHinhAnhs)
                .HasForeignKey(d => d.IdBaiDang)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__tbHinhAnh__idBai__46E78A0C");
        });

        modelBuilder.Entity<TbMucDoThietHai>(entity =>
        {
            entity.HasKey(e => e.IdMucDo).HasName("PK__tbMucDoT__15C6693B0D72DCC6");

            entity.ToTable("tbMucDoThietHai");

            entity.Property(e => e.IdMucDo).HasColumnName("idMucDo");
            entity.Property(e => e.MoTa).HasColumnName("moTa");
            entity.Property(e => e.MucThietHai)
                .HasMaxLength(50)
                .HasColumnName("mucThietHai");
        });

        modelBuilder.Entity<TbTaiKhoan>(entity =>
        {
            entity.HasKey(e => e.IdTaiKhoan).HasName("PK__tbTaiKho__8FA29E4AEE13257A");

            entity.ToTable("tbTaiKhoan");

            entity.HasIndex(e => e.TenDangNhap, "UQ__tbTaiKho__59267D4A2BBBF7E7").IsUnique();

            entity.HasIndex(e => e.Email, "UQ__tbTaiKho__AB6E6164B2F6AEC7").IsUnique();

            entity.HasIndex(e => e.Sdt, "UQ__tbTaiKho__DDDFB4838539B9CB").IsUnique();

            entity.Property(e => e.IdTaiKhoan).HasColumnName("idTaiKhoan");
            entity.Property(e => e.DiaChi)
                .HasMaxLength(50)
                .HasColumnName("diaChi");
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.HoVaTen)
                .HasMaxLength(100)
                .HasColumnName("hoVaTen");
            entity.Property(e => e.MatKhau)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("matKhau");
            entity.Property(e => e.Quyen)
                .HasMaxLength(50)
                .HasColumnName("quyen");
            entity.Property(e => e.Sdt)
                .HasMaxLength(11)
                .IsUnicode(false)
                .HasColumnName("sdt");
            entity.Property(e => e.TenDangNhap)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("tenDangNhap");
            entity.Property(e => e.TenPhuong)
                .HasMaxLength(1)
                .HasColumnName("tenPhuong");
        });

        modelBuilder.Entity<TbThongBao>(entity =>
        {
            entity.HasKey(e => e.IdThongBao).HasName("PK__tbThongB__8B4D322B80B275AE");

            entity.ToTable("tbThongBao");

            entity.Property(e => e.IdThongBao).HasColumnName("idThongBao");
            entity.Property(e => e.IdTaiKhoan).HasColumnName("idTaiKhoan");
            entity.Property(e => e.NgayDang)
                .HasColumnType("datetime")
                .HasColumnName("ngayDang");
            entity.Property(e => e.NoiDung).HasColumnName("noiDung");

            entity.HasOne(d => d.IdTaiKhoanNavigation).WithMany(p => p.TbThongBaos)
                .HasForeignKey(d => d.IdTaiKhoan)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__tbThongBa__idTai__6477ECF3");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
