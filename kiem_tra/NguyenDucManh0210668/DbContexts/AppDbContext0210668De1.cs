using Microsoft.EntityFrameworkCore;
using NguyenDucManh0210668.Entities;

namespace NguyenDucManh0210668.DbContexts;

public class AppDbContext0210668De1 : DbContext
{
    public AppDbContext0210668De1(DbContextOptions<AppDbContext0210668De1> options) : base(options)
    {
    }

    public DbSet<NhanVien0210668De1> NhanViens => Set<NhanVien0210668De1>();
    public DbSet<DuAn0210668De1> DuAns => Set<DuAn0210668De1>();
    public DbSet<PhanCong0210668De1> PhanCongs => Set<PhanCong0210668De1>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<NhanVien0210668De1>(entity =>
        {
            entity.ToTable("NhanViens");
            entity.HasKey(nhanVien => nhanVien.Id);
            entity.Property(nhanVien => nhanVien.Id).ValueGeneratedOnAdd();
            entity.Property(nhanVien => nhanVien.TenNhanVien).HasMaxLength(150).IsRequired();
            entity.Property(nhanVien => nhanVien.MaNhanVien).HasMaxLength(50).IsRequired();
            entity.Property(nhanVien => nhanVien.Email).HasMaxLength(150).IsRequired();
            entity.HasIndex(nhanVien => nhanVien.MaNhanVien).IsUnique();
            entity.HasIndex(nhanVien => nhanVien.Email).IsUnique();
        });

        modelBuilder.Entity<DuAn0210668De1>(entity =>
        {
            entity.ToTable("DuAns");
            entity.HasKey(duAn => duAn.Id);
            entity.Property(duAn => duAn.Id).ValueGeneratedOnAdd();
            entity.Property(duAn => duAn.TenDuAn).HasMaxLength(150).IsRequired();
            entity.Property(duAn => duAn.MaDuAn).HasMaxLength(50).IsRequired();
            entity.HasIndex(duAn => duAn.TenDuAn).IsUnique();
            entity.HasIndex(duAn => duAn.MaDuAn).IsUnique();
        });

        modelBuilder.Entity<PhanCong0210668De1>(entity =>
        {
            entity.ToTable("PhanCongs");
            entity.HasKey(phanCong => phanCong.Id);
            entity.Property(phanCong => phanCong.Id).ValueGeneratedOnAdd();
            entity.Property(phanCong => phanCong.SoGioLamViec).IsRequired();
            entity.HasIndex(phanCong => new { phanCong.NhanVienId, phanCong.DuAnId }).IsUnique();

            entity.HasOne(phanCong => phanCong.NhanVien)
                .WithMany(nhanVien => nhanVien.PhanCongs)
                .HasForeignKey(phanCong => phanCong.NhanVienId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(phanCong => phanCong.DuAn)
                .WithMany(duAn => duAn.PhanCongs)
                .HasForeignKey(phanCong => phanCong.DuAnId)
                .OnDelete(DeleteBehavior.Cascade);
        });
    }
}
