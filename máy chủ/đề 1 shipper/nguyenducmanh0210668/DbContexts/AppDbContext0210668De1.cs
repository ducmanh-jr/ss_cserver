using Microsoft.EntityFrameworkCore;
using nguyenducmanh0210668.Entities;

namespace nguyenducmanh0210668.DbContexts
{
    public class AppDbContext0210668De1 : DbContext
    {
        public AppDbContext0210668De1(DbContextOptions<AppDbContext0210668De1> options) : base(options) { }

        // Khai báo 3 bảng sẽ được tạo trong Database
        public DbSet<Shipper0210668De1> Shippers { get; set; }
        public DbSet<SanPham0210668De1> SanPhams { get; set; }
        public DbSet<ChiTietGiaoHang0210668De1> ChiTietGiaoHangs { get; set; }

        // Viết các quy tắc thiết kế database
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Tạo khóa chính kép cho bảng trung gian (gồm cả 2 id)
            modelBuilder.Entity<ChiTietGiaoHang0210668De1>()
                .HasKey(c => new { c.ShipperId, c.SanPhamId });

            // Ràng buộc "không được trùng" (Unique) theo đúng yêu cầu đề bài
            modelBuilder.Entity<Shipper0210668De1>().HasIndex(s => s.MaShipper).IsUnique();
            modelBuilder.Entity<Shipper0210668De1>().HasIndex(s => s.TenShipper).IsUnique();
            modelBuilder.Entity<Shipper0210668De1>().HasIndex(s => s.CCCD).IsUnique();
            
            modelBuilder.Entity<SanPham0210668De1>().HasIndex(s => s.MaSanPham).IsUnique();
            modelBuilder.Entity<SanPham0210668De1>().HasIndex(s => s.TenSanPham).IsUnique();
        }
    }
}
